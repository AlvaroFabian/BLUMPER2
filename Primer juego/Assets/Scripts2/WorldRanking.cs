using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/*Las clases  son básicamente una plantilla que sirve para crear un objeto. Si imaginásemos las clases en el mundo en el que vivimos
podríamos decir que la clase “persona” es una plantilla sobre cómo debe ser un ser humano. Todos y cada uno de nosotros, los seres humanos
somos objetos de la clase “persona“, ya que todos somos personas. La clase “persona” contiene la definición de un 
ser humano, mientras que cada ser humano es una instancia u objeto de dicha clase.*/
public class Puntaje
{
    //Propiedades de la clase
    public string nombre;
    public string puntaje;

    /*Una vez establecidas las propiedades de la clase creamos un constuctor. 
     El contructor es un método con el nombre de la clase donde se establecen
     los parámetros y las acciones a ejecutar cuando sea que se cree un nuevo
     objeto o instancia (objeto e instancia es básicamente lo mismo) 
     de la clase persona. En Java las llamadas “funciones” de otros 
     lenguajes se conocen como “métodos”*/
    public Puntaje (string _nombre, string _puntaje)//Constructor
    {
        nombre = _nombre;
        puntaje = _puntaje;
    }



}



public class WorldRanking : MonoBehaviour {

   
    private string URL = "http://dreamlo.com/lb/";//URL de enlace con DREAMIO
    private string PublicKey = "580849ac8af6030d4c35c5cc";//la llave publica se utiliza para leer datos del servidor
    private string PrivateKey = "GG0SQJ4Dmk6EEV3RXI5a1QqrFgt5SjbEyaEITNYoGWKw";//La llave privada se utiliza para escribir datos al servidor
  Puntaje[] puntajes;//Creamos un ARRAY de nombres y puntajes
    public Text leaderBoard;//Texto donde aparecen los nombres y puntajes
    public InputField NombreBorrar, Nombreanadir, Puntajeanadir;//Campos donde se escriben los nombres y puntajes 

    public Button _Borrar, _Anadir;//Botones para agregar o borrar los nombres del servidor
    void Start()
    {
        StartCoroutine(ObtenerPuntajes());//Rutina encargada de traer los puntajes del servidor
        _Borrar.onClick.AddListener(Borrar);//
        _Anadir.onClick.AddListener(Añadir);
    }

    public void Borrar()
    {
        StartCoroutine(EliminarPuntaje(NombreBorrar.text));//Tomamos el nombre insertado en el inputfield y lo enviamos a la corrutina
    }
    public void Añadir()
    {
        StartCoroutine(AnadirPuntaje(new Puntaje(Nombreanadir.text, Puntajeanadir.text)));
    }
    IEnumerator AnadirPuntaje(Puntaje puntaje)
    {
        WWW www = new WWW (URL+PrivateKey+"/add/"+puntaje.nombre+"/"+puntaje.puntaje);//Cadena para añadir los datos al servidor
        yield return www;
        print(www.text);
        StartCoroutine(ObtenerPuntajes());//Rutina encargada de traer los puntajes del servidor

    }
    IEnumerator EliminarPuntaje(string name)//Tomamos el dato recibido desde el input field y lo movemos al string @name
    {
        WWW www = new WWW(URL + PrivateKey + "/delete/" + name);//Cadena para eliminar los datos al servidor
        yield return www;//Obtenemos las propiedades de la URL
        StartCoroutine(ObtenerPuntajes());//Rutina encargada de traer los puntajes del servidor
        print(www.text);
    }
    IEnumerator ObtenerPuntajes()//Rutina encargada de traer los puntajes del servidor
    {
        WWW www = new WWW(URL + PublicKey + "/quote/");//Traemos los datos del servidor mediante la cadena
        yield return www;//Obtenemos las propiedades de la URL
        string[] S = www.text.Split('\n');//Igualamos el array S al texto traido desde el servidor 
        string[,] matriz = new string[S.Length-1 ,5];//S.Length contiene la cantidad de datos traidos del server
      //  Debug.Log(matriz.Length);
        for (int i = 0; i < matriz.GetLength(0); i++)//Hacemos que se repita el ciclo una vez por la extencion total de archivor del servidor
        {
            string[] C = S[i].Split(',');//Separamos por coma cada registro

            for (int j = 0; j < S.Length; j++)
            {
                //Quita las comillas dobles que vienen con los datos
               matriz[i, j] = C[j].Replace('\u0022',' ');//Mediante el codigo  ASCII h ubicamos las comillas dobles("") y reeplazamos por un espacion vacio
             //  Debug.Log(j);
               // Debug.Log(i);
              //  Debug.Log(i);
                Debug.Log(matriz[i, j]);

            }
            // Debug.Log(i);
        }
        puntajes = new Puntaje[matriz.GetLength(0)];
        for (int i = 0; i < matriz.GetLength(0); i++)

        {

            puntajes[i] = new Puntaje(matriz[i, 0], matriz[i, 1]);
            Debug.Log(matriz[i, 0]);
            Debug.Log(matriz.GetLength(0));
        }
        Actualizar();
    }
    void Actualizar()
    {
        leaderBoard.text = ("LeadeBoard\n");
        for (int i = 0; i < puntajes.Length; i++)

        {
            leaderBoard.text += puntajes[i].nombre+" "+puntajes[i].puntaje+"\n";
        }


    }

}
