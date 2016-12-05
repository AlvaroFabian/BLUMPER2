using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RankinMundial : MonoBehaviour
{

    const string URL = "http://dreamlo.com/lb/";//URL de enlace con DREAMIO
    const string PublicKey = "580849ac8af6030d4c35c5cc";//la llave publica se utiliza para leer datos del servidor
    const string PrivateKey = "GG0SQJ4Dmk6EEV3RXI5a1QqrFgt5SjbEyaEITNYoGWKw";//La llave privada se utiliza para escribir datos al servidor
    public Ranking[] RankingList;//Referencia a la estructura publica
    public Text leaderBoard;
    public Text puntuacion;
    public int lugar;
    void Awake()
    {
       // DontDestroyOnLoad(gameObject);
      
        NotificationCenter.DefaultCenter().AddObserver(this, "NuevoRecord");
        Leer();
    }

    public void AgregarPuntaje(string username, int score)
    {

        StartCoroutine(CargarPuntaje(username, score));
    }

    IEnumerator CargarPuntaje(string username, int score)
    {
        WWW www = new WWW(URL + PrivateKey + "/add/" + WWW.EscapeURL(username) + "/" + score);//Cadena para añadir los datos al servidor
        yield return www;

        if (string.IsNullOrEmpty(www.error)) 
       print("carga completa");
        else
        {

            leaderBoard.text = "Error al enviar datos";
        }
    }
    public void Leer()
    {
        StartCoroutine("LeerPuntaje");
    }

        IEnumerator LeerPuntaje()
       {
        WWW www = new WWW(URL + PublicKey + "/pipe/");//Cadena para añadir los datos al servidor
        yield return www;

        if (string.IsNullOrEmpty(www.error))
            FormatoPuntajes(www.text);//enviamos la cadena de texto recibida del servidor al metodo FormatoPuntajes
           // print("lectura correcta");
            //print(www.text);
                
        else
        {

           // print("error en la lectura" + www.error);
            leaderBoard.text = "No se detecto conexion a red";
        }
    }
    void FormatoPuntajes(string textStream)//textStream recibe los datos del server
    {
        leaderBoard.text = ("");
        string[] datos = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);//Separamos los datos cada que hay una nueva linea(\n) y los movemos al array @Datos
        //Ocupando un espacio en el array por nombre del jugador
        RankingList = new Ranking[datos.Length];//Creamos un array de Nombre y puntajes que tenga la extension igual al maximo de elementos que contiene @datos
       // print(datos);

        for(int i = 0; i < datos.Length; i++)
        {
            string[] InfoDatos = datos[i].Split(new char[] { '|' });//Separa los datos por simbolo y los mueve uno a 1 a infodatos
            string username = InfoDatos[0];//definimos que el dato 0 del array es el nombre del jugador
            int score = int.Parse(InfoDatos[1]);//definimos que el dato 1 del array es el puntaje
            lugar = int.Parse(InfoDatos[5]);//obtenemos la hubicacion en el ranking mundial
            RankingList[i] = new Ranking(username, score);//movemos los valores de username y score a el array RankingList[i]
           // print(RankingList[i].username + ":" + RankingList[i].score+" posicion: "+ (lugar+1));//cada ciclo imprimimos el username y el score de cada jugador, la variable (i) define el orden ascendente en el que se imprimen los datos
            leaderBoard.text += ((lugar+1) +"." + RankingList[i].username+"\n");
          
        }
       Actualizar();
    }



    void Actualizar()
    {
        //leaderBoard.text = ("Ranking Mundial\n");
        for (int i = 0; i < RankingList.Length; i++)

        {
            //leaderBoard.text += RankingList[i].username + " " + RankingList[i].score + "\n";
            puntuacion.text += (RankingList[i].score + "\n") ;
        }


    }
}
public struct Ranking
{
    public string username;
    public int score;


    public Ranking(string _username, int _score)//Se crea este constructor para poder tomar los dos argumentos de forma publica
    {
        username = _username;
        score = _score;
    }
void NuevoRecord(Notification PuntajeRecord)
    {
        Debug.Log(PuntajeRecord);
    }
}