using UnityEngine;
using System.Collections;

public class MovimientoBases : MonoBehaviour
{
    public TextMesh TextBase1, TextBase2;
    public Transform Base1, Base2;//Referencia a las bases
    public static int NewPOSx;//posicion de desplazamiento aleatoria
    public static bool nosuma = true;//Bool se activa cuando el personaje revive para que no se cuenten esos puntos en el puntaje
    public Transform Personaje;//Referencia a el personaje para rehubicalo cuando muere
    public GameObject GreenBoss;//Referencia a green boss para activarlo como enemigo final
    public static bool Bossmode = false;//Se habilita cuando se activa el enemigo final
    public GameObject Piranas;//Referencia a el elemento que contiene las dos pirañas
    private int Cantidad = 1;//Gestiona la cantidad de pirañas
    private int Cantidadaux = 0;//Gestiona la cantidad de pirañas
    public float TiempoMin, TiempoMax;//Tiempos maximo y minimo en los que se generara cada bloque, vaalores asiganados desde el inspector del CDO
    private bool CorrecionPirana;//lo utilizamos para llamar al metodo de genracion de pirañas una sola vez
    public GameObject Green, Red;//Lo utilizamos para referenciar las pirañas para poder modificar su velocidad de aparicion y su vosibilidad
    private bool GeneroBase= false;//Auxiliar para saber que base esta creada y para evitar que las bases se muevan nuevamente cuando el personaje salta mas de una vez por base
    private bool GeneroBase2 = false;//Auxiliar para saber que base esta creada y para evitar que las bases se muevan nuevamente cuando el personaje salta mas de una vez por base

    public static int AuxPuntaje;//Variable auxiliar para conteo puntaje
    private int PosxAux;//Auxiliar para mostrar puntaje por base correcto
    public GameObject healtbar; //Referencia ala barra de vida de green para activarsu visibilidad cuando aparece
    private bool piranaActiva = false;//Bit para sabr cuando hay una piraña en el aire
    public GestorPowerUps GestorPU;//Referencia a el C#Script GetorPowerUps
    public static int ConteoSaltos = 0;
    public static bool SinEnemigos=false;
   
    void Start()
    {
        ConteoSaltos = 0;
        NotificationCenter.DefaultCenter().AddObserver(this, "PersonajePierdeVida");
        NotificationCenter.DefaultCenter().AddObserver(this, "PiranaMurio");
        int Dificultad = PlayerPrefs.GetInt("Dificultad");//Obtenemos el playerpref que contiene la dificultad de juego

        switch (Dificultad)
        {
            case 0:
               
                break;

            case 1:
              
                break;
            case 2:
               
                break;
            case 3://Supervivencia
                SinEnemigos = true;
                break;
            case 4://Contrareloj
                SinEnemigos = true;
                break;


        }
    }

    void PersonajePierdeVida()//Cuando el personaje pierde una vida vuelve a ser transportado hasta la posisicion de la base actual mas un poco de altura
    {
        GameObject White = GameObject.Find("White(Clone)");//Busca al personaje
        Destroy(White);//Destruye el clon
        GameObject Erizo = GameObject.Find("Erizo(Clone)");//Busca al personaje
        Destroy(Erizo);//Destruye el clon
        GameObject Lanza = GameObject.Find("Lanza(Clone)");//Busca al personaje
        Destroy(Lanza);//Destruye el clon
        Personaje.position = new Vector2(transform.position.x, transform.position.y + 6);

        nosuma = true;//Bit para evitar que el personaje sume puntos al morir
    }

    public void ActivarGreenBoos()//Activamos el modo enemigo final
    {
        Bossmode = true;//Activamos a el enemigo
        //NotificationCenter.DefaultCenter().PostNotification(this, "BossmodeEnabled");//notificamos que se a activado
        GreenBoss.SetActive(true);//Activamos la visibilidad del personje
    }



    void GenerarPiranas()//Genera pirañas
    {
    
        if (GestorPuntos.PuntosGeneral >= 100)//Si el puntaje es mayor a 100 activa la piraña numero 1
        {
            
            Cantidadaux = Random.Range(1, Cantidad);//Genera un numero entre 1 y la cantidad definida inicialmente como 1
            Piranas.transform.SetParent(null);
            if (!Bossmode && !SinEnemigos)//Solo genera los enemigos mientras no este activo el modo de enemigo final y no este en modo supervivencia ni contrarreloj
             {
                if (Cantidadaux == 1)//la variable cantidadaux almacena la cantidad de pirañas habilitadas
                {
                    Red.SetActive(true);
                    Red.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 12.2f, ForceMode2D.Impulse);
                    piranaActiva = true;//Variable para saber cuando hay una piraña en el aire
                    CorrecionPirana = true;
                }
                else if(Cantidadaux == 2)//la variable cantidadaux almacena la cantidad de pirañas habilitadas cuando cambia a 2 empieza a generar los dos tipos de piraña
                {
                    Green.SetActive(true);//Activa la visibilidad de la piraña
                    Green.GetComponent<Rigidbody2D>().AddForce(Vector3.up * 11.7f, ForceMode2D.Impulse);//genera um impulso en el eje (y)
                    piranaActiva = true;//Variable para saber cuando hay una piraña en el aire
                }

                if (GestorPuntos.PuntosGeneral >= 250)//Cuando el puntaje es mayor a 250 cambia la cifra de cantidad para que genere los dos enemigos no se tiene encuenta el 3
                {

                    Cantidad = 3;
                }

                Invoke("GenerarPiranas", Random.Range(TiempoMin, TiempoMax));//volvemos a llamar la subfuncion "GenerarPiranas" 1 vez cada que se cumpla un lapso comprendido en un numero aleatorio que se encuentra entre los valores maximo y minimo del random.range  
           
             }

        }

    }
    void PiranaMurio()//Metodo llamado cada vez que el personaje mata una piraña o cada vez que entra en contacto con los destructores
    {
    ///////////////Desabilita sus visibilidades y las rehubica
        Red.SetActive(false);
        Green.SetActive(false);
        Red.transform.position = Piranas.transform.position;
        Green.transform.position = Piranas.transform.position;
        piranaActiva = false;//Variable para saber cuando hay una piraña en el aire
        MovimientoPiranas();
       // GenerarPiranas();

    }

    public void MovimientoBase()//Metodo encargado de controlar el movimiento de las bases 
    {
        SegundoSalto.animaciones.SetBool("Jump", false);//Variable que controla las animacion de salto
        NewPOSx = Random.Range(6, 16);//Rango aleatorio en el cual pueden aparecer las bases

        if (GestorPuntos.BaseActiva == ("Base") && (!GeneroBase))//Cunado el personaje esta sobre la base 1 y salta, la base 2 se reposiciona
        {
            ConteoSaltos++;
            TextBase1.text = NewPOSx.ToString();
            TextBase2.text = NewPOSx.ToString();
            PosxAux = NewPOSx;
            AuxPuntaje = NewPOSx;
            Base2.transform.position = new Vector2(Base1.position.x + NewPOSx, transform.position.y);//Movemos la base 
           // Debug.Log(ConteoSaltos);
          //  Debug.Log(ConteoSaltos);
          //  Debug.Log(GestorPuntos.BaseActiva);
            if (piranaActiva==false)
            {

                MovimientoPiranas();
            }

            if (BonusPU.FlechaEspAct)
            {
                BonusPU.TotalOro++;
                GestorPU.ActualizarOro();//Actualizamos el marcador en la pantalla
            }

            if (!CorrecionPirana)//Llama a la funcion solamente una vez para que no gener pirañas adicionales
            {
                GenerarPiranas();
            }

           GeneroBase2 = false;//Auxiliar para saber que base esta creada y para evitar que las bases se muevan nuevamente cuando el personaje salta mas de una vez por base
            GeneroBase = true;//Auxiliar para saber que base esta creada y para evitar que las bases se muevan nuevamente cuando el personaje salta mas de una vez por base


        }

        if (GestorPuntos.BaseActiva == ("Base2") && (!GeneroBase2))
        {
            ConteoSaltos++;
            TextBase2.text = NewPOSx.ToString();
            TextBase1.text = NewPOSx.ToString();
            AuxPuntaje = NewPOSx;
            PosxAux = NewPOSx;
            Base1.transform.position = new Vector2(Base2.position.x + NewPOSx, transform.position.y);
           // Debug.Log(ConteoSaltos);
            //Debug.Log(GestorPuntos.BaseActiva);
            if (piranaActiva==false)
            {
                MovimientoPiranas();
            }
            if (BonusPU.FlechaEspAct)
            {
                BonusPU.TotalOro++;
                GestorPU.ActualizarOro();//Actualizamos el marcador en la pantalla
            }


            GeneroBase = false;//Auxiliar para saber que base esta creada y para evitar que las bases se muevan nuevamente cuando el personaje salta mas de una vez por base
            GeneroBase2 = true;//Auxiliar para saber que base esta creada y para evitar que las bases se muevan nuevamente cuando el personaje salta mas de una vez por base
         
        }

        if (GestorPuntos.PuntosGeneral >= 1000 && NewPOSx <= 10 && !Bossmode && !SinEnemigos)
        {
            ActivarGreenBoos();
            healtbar.SetActive(true);
        }

    }

    void MovimientoPiranas()
    {
        if (GestorPuntos.BaseActiva == ("Base"))
        {
            Piranas.transform.position = new Vector2(Base1.position.x + PosxAux / 2, Piranas.transform.position.y);//Movemos el generador de pirañas
        }

        if (GestorPuntos.BaseActiva == ("Base2"))
        {
            Piranas.transform.position = new Vector2(Base2.position.x + PosxAux / 2, Piranas.transform.position.y);//Movemos el generador de pirañas
        }
    }
   
}
