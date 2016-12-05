using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class GestorPuntos : MonoBehaviour {
    public static int PuntosDesdeBase;//Puntos que aparecen en cada base
    public static int PuntosGeneral = 0;//Acomulado de puntos
    public static bool Actualizado = false;//Booleano que avisa que ta se actualizo la primera vez
    public TextMesh puntuacionGeneral;
    public TextMesh Vidas;
    public static int VidasT;
    public GameObject efectoParticulas,FondoAventuraA,FondoAventuraB,FondoSupervivencia, FondoCR;
    public static string BaseActiva;
    public MovimientoBases MovimientoBase;
    public GameObject EfectoParticulasBomba;
    public Rigidbody2D _rigidBody;
    public Text Orotext, Puntostext,Recordtext;
    public GameObject NieveEfx, Luciernagas, Trajesupervivencia, TrajeCR;

    public GameObject BotonPlay,Canvas,Botonpausa;
    public Text textoCanvas;
    public static int Dificultad;
    
    /////////////////////Variables escritura DREAM.LO///////////////////
    const string URL = "http://dreamlo.com/lb/";//URL de enlace con DREAMIO
    const string PublicKey = "580849ac8af6030d4c35c5cc";//la llave publica se utiliza para leer datos del servidor
    const string PrivateKey = "GG0SQJ4Dmk6EEV3RXI5a1QqrFgt5SjbEyaEITNYoGWKw";//La llave privada se utiliza para escribir datos al servidor
    void Start()
    {
        AgregarPuntaje(PlayerPrefs.GetString("PlayerName"), PlayerPrefs.GetInt("RecordSurvivor"));//Escribimos siempre el record al iniciar el juego en caso que se juegue sin conexion
        NotificationCenter.DefaultCenter().AddObserver(this, "PersonajeGanaPuntos");//Nos inscribimos a la notificacion que indica que el personaje a ganado puntos o actualiza puntaje
        NotificationCenter.DefaultCenter().AddObserver(this, "PersonajePierdeVida");
        NotificationCenter.DefaultCenter().AddObserver(this, "TiempoAcabado");
        Dificultad = PlayerPrefs.GetInt("Dificultad");//Obtenemos el playerpref que contiene la dificultad de juego
                                                      // string ModoJuego = PlayerPrefs.GetString("ModoJuego");//Obtenemos el playerpref que contiene la dificultad de juego
        //TrajeCR.SetActive(true);
        //Trajesupervivencia.SetActive(false);
        switch (Dificultad)
            {
                case 0:
               FondoAventuraA.SetActive(true);
              FondoSupervivencia.SetActive(false);
                FondoAventuraB.SetActive(false);
                FondoCR.SetActive(false);
                NieveEfx.SetActive(false);
                VidasT = 20;
                    Vidas.text = ("x" + VidasT).ToString();
                    break;

                case 1:
                FondoAventuraB.SetActive(true);
                FondoSupervivencia.SetActive(false);
                FondoAventuraA.SetActive(false);
                FondoCR.SetActive(false);
                NieveEfx.SetActive(false);
                VidasT = 10;
                    Vidas.text = ("x" + VidasT).ToString();
                    break;
                case 2:
                FondoAventuraA.SetActive(true);
                FondoSupervivencia.SetActive(false);
                FondoAventuraB.SetActive(false);
                FondoCR.SetActive(false);
                NieveEfx.SetActive(false);
                VidasT = 5;
                    Vidas.text = ("x" + VidasT).ToString();
                    break;
            case 3://Supervivencia
               FondoSupervivencia.SetActive(true);
               FondoAventuraA.SetActive(false);
                FondoAventuraB.SetActive(false);
                FondoCR.SetActive(false);
                NieveEfx.SetActive(true);
                TrajeCR.SetActive(false);
                Trajesupervivencia.SetActive(true);
                VidasT = 1;
                Vidas.text = ("x" + VidasT).ToString();
                break;
            case 4://Contrareloj
                FondoCR.SetActive(true);
                FondoAventuraA.SetActive(false);
                FondoAventuraB.SetActive(false);
                FondoSupervivencia.SetActive(false);
                Luciernagas.SetActive(true);
                Vidas.text = ("∞");
                TrajeCR.SetActive(true);
               Trajesupervivencia.SetActive(false);
                break;


        }
      
    }
    void PersonajeGanaPuntos()
    {
        // PuntosDesdeBase = GeneradorBases.NewPOSx;//Traemos lo puntos que el jugador va a ganar representados por la distancia que saltara
        if (BonusPU.MultiplicadorAct)
        {
            if (MovimientoBases.SinEnemigos)//Si multiplicador y sin enemigos
            {

                if (Dificultad == 4)
                {
                    Vidas.text = ("∞");
                    PuntosDesdeBase = 2;
                    PuntosGeneral = PuntosGeneral + PuntosDesdeBase;//igualamos el puntaje general a el puntaje actual mas los puntos que gana el jugador al llegar a una nueva base
                    puntuacionGeneral.text = PuntosGeneral.ToString();
                    Actualizado = true;
                }
                else
                {
                    PuntosDesdeBase = 2;
                    PuntosGeneral = PuntosGeneral + PuntosDesdeBase;//igualamos el puntaje general a el puntaje actual mas los puntos que gana el jugador al llegar a una nueva base
                    puntuacionGeneral.text = PuntosGeneral.ToString();
                    Actualizado = true;//Avisamos que ya se actualizo el puntaje antes de generar bases
                    Vidas.text = ("x" + VidasT).ToString();
                }
            }
            else
            {
                PuntosDesdeBase = MovimientoBases.AuxPuntaje;//Traemos lo puntos que el jugador va a ganar representados por la distancia que saltara
                PuntosGeneral = PuntosGeneral + (PuntosDesdeBase * 2);//igualamos el puntaje general a el puntaje actual mas los puntos que gana el jugador al llegar a una nueva base
                puntuacionGeneral.text = PuntosGeneral.ToString();
                Actualizado = true;//Avisamos que ya se actualizo el puntaje antes de generar bases
                Vidas.text = ("x" + VidasT).ToString();
            }
        }
        else if (MovimientoBases.SinEnemigos)//Solo suma un punto cuando esta en modo supervivencia o contrarreloj
        {
            PuntosDesdeBase = 1;//Traemos lo puntos que el jugador va a ganar representados por la distancia que saltara
            PuntosGeneral = PuntosGeneral + PuntosDesdeBase;//igualamos el puntaje general a el puntaje actual mas los puntos que gana el jugador al llegar a una nueva base
            puntuacionGeneral.text = PuntosGeneral.ToString();
            Actualizado = true;//Avisamos que ya se actualizo el puntaje antes de generar bases
            if (Dificultad == 4)
            { Vidas.text = ("∞"); }
            else
            {
                Vidas.text = ("x" + VidasT).ToString();
            }
        }
        else
        {
            PuntosDesdeBase = MovimientoBases.AuxPuntaje;//Traemos lo puntos que el jugador va a ganar representados por la distancia que saltara
            PuntosGeneral = PuntosGeneral + PuntosDesdeBase;//igualamos el puntaje general a el puntaje actual mas los puntos que gana el jugador al llegar a una nueva base
            puntuacionGeneral.text = PuntosGeneral.ToString();
            Actualizado = true;//Avisamos que ya se actualizo el puntaje antes de generar bases
            Vidas.text = ("x" + VidasT).ToString();
        }
    }
    void PersonajePierdeVida()
    {
        //Si personaje pierde todas las vidas y puntaje > a puntaje record, esntones escriba en la red
        if (Dificultad == 4)
        {
            Vidas.text = ("∞");
           // Vidas.fontSize = 50;
        }
        else
        {
            VidasT = VidasT - 1;
            Vidas.text = ("x" + VidasT).ToString();
            if (VidasT <= 0)
            {
                Canvas.SetActive(true);
                BotonPlay.SetActive(false);
                Botonpausa.SetActive(false);
                textoCanvas.text = "¡Fin del juego!";
                Orotext.text = ("Oro:                              " + BonusPU.TotalOro);
                Puntostext.text = ("Puntuacion:               " + PuntosGeneral.ToString());
                EscribirOroTotal();
              



                if (Dificultad == 3)//Condicion para escribir en el ranking//Modo de juego supervivencia
                {
                    AgregarPuntaje(PlayerPrefs.GetString("PlayerName"), PuntosGeneral);
                    Recordtext.text = ("Tu mejor puntaje: " + PlayerPrefs.GetInt("RecordSurvivor"));
                    if (PuntosGeneral > PlayerPrefs.GetInt("RecordSurvivor"))//si el nuevo puntaje es mayor al record
                    {
                        PlayerPrefs.SetInt("RecordSurvivor", PuntosGeneral);//lo sobreescribe
                        Recordtext.text = ("Tu mejor puntaje: " + PlayerPrefs.GetInt("RecordSurvivor"));
                        print("carga");
                    }
                }
               
            }
        }
    }
    void OnCollisionEnter2D(Collision2D objeto)
    {
       //Debug.Log(objeto.gameObject.tag);
        if (objeto.gameObject.name == "Erizo(Clone)")// Si colisona con el jugador 
        {
            Instantiate(efectoParticulas, objeto.transform.position, Quaternion.identity);
            Destroy(objeto.gameObject);//Destruye el clon
        }
        if (objeto.gameObject.tag == "Bomba" || objeto.gameObject.tag == "White")// Si colisona con la bomba 
        {

            Instantiate(EfectoParticulasBomba, new Vector3(transform.position.x,transform.position.y + 1, transform.position.z-3), Quaternion.identity);
            _rigidBody.AddForce(Vector3.left * 30, ForceMode2D.Impulse);
            Destroy(objeto.gameObject);
        }
        
        BaseActiva = objeto.gameObject.name;//Movemos la base activa a la variable para la gestion de movimiento de bases
        MovimientoBase.MovimientoBase();
    }
    /////////////Lo que hace al acabarse el tiempo/////////////
    void TiempoAcabado()
    {
   
            Recordtext.text = ("Tu mejor puntaje: " + PlayerPrefs.GetInt("RecordCR"));
            if (PuntosGeneral > PlayerPrefs.GetInt("RecordCR"))//si el nuevo puntaje es mayor al record
            {
                PlayerPrefs.SetInt("RecordCR", PuntosGeneral);//lo sobreescribe
                Recordtext.text = ("Tu mejor puntaje: " + PlayerPrefs.GetInt("RecordCR"));
                print("carga");
            }
       
        Canvas.SetActive(true);
        BotonPlay.SetActive(false);
        Botonpausa.SetActive(false);
        textoCanvas.text = "¡Se te acabo el tiempo!";
        Orotext.text = ("Oro:                              " + BonusPU.TotalOro);
        Puntostext.text= ("Puntuacion:              " + PuntosGeneral.ToString());

    }
    ////////////////Codigo para subir puntaje a ranking mundial////////
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

            //leaderBoard.text = "Error al enviar datos";
        }






    }
    void EscribirOroTotal()//Sumatoria oro total
    {
        int OroTotal = BonusPU.TotalOro;
        int TotalOro = PlayerPrefs.GetInt("TotalOro") + OroTotal;


        PlayerPrefs.SetInt("TotalOro",TotalOro);//Escribimos  en  playerpref
        print(PlayerPrefs.GetInt("TotalOro"));
    }
}
