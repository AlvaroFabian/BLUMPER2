using UnityEngine;
using System.Collections;

public class GestorPowerUps : MonoBehaviour {
    public GameObject Oro, Vida, Multiplicador, FlechaEsp, MasTiempo;//Referencia a los powerups para activarlos y desactivarlos segun corresponda
    private int PuntosOro = 5;//Variable que almacena la cantidad de saltos que hay que hacer para generar oro
    private int PuntosVida= 7;//Variable que almacena la cantidad de saltos que hay que hacer para generar una vida
    private int PuntosMultiplicador = 29;//Variable que almacena la cantidad de saltos que hay que hacer para generar una vida
    private int PuntosFlechaEsp = 49;//Variable que almacena la cantidad de saltos que hay que hacer para generar una vida
    private int PuntosTiempo = 3;//Variable que almacena la cantidad de saltos que hay que hacer para generar una vida
    public GameObject AnimaX2,AnimaEsp;
    private Rigidbody2D GeneradorPU;
    //private int TotalOro =1;
    public TextMesh TextOro;
    private int dificultad;
	// Use this for initialization
	void Start () {
        GeneradorPU = this.GetComponent<Rigidbody2D>();//Obtenemos el rigidbody del CDO y lo referenciamos a GeneradorPU
        NotificationCenter.DefaultCenter().AddObserver(this, "PersonajePierdeVida");//Nos inscribimos a este evento para desactivar los powerups en caso que el personaje muera
        dificultad = PlayerPrefs.GetInt("Dificultad");
    }
	
	// Update is called once per frame
	void Update () {

        if (MovimientoBases.ConteoSaltos == PuntosOro)//Cuando el jugador realiza 5 saltos comienzan a generarse las monedas
        {
            GenerarOro();//metodo activacion de monedas
        }

        if (MovimientoBases.ConteoSaltos == PuntosVida)//Cuando el jugador realiza 5 saltos comienzan a generarse las monedas
        {
            GenerarVida();//metodo activacion de monedas
        }
        if (MovimientoBases.ConteoSaltos == PuntosMultiplicador)//Cuando el jugador realiza 30 saltos
        {
            GenerarMultiplicador();
        }
        if (MovimientoBases.ConteoSaltos == PuntosFlechaEsp)//Cuando el jugador realiza 30 saltos
        {
            GenerarFlechaEsp();
        }
        if (MovimientoBases.ConteoSaltos == PuntosTiempo)//Cuando el jugador realiza 30 saltos
        {
            GenerarMasTiempo();
        }

    }


    void GenerarOro()
    { 
        FMO
        Oro.SetActive(true);//Activamos moneda
        GeneradorPU.isKinematic = false;//la hacemos dinamica 
        GeneradorPU.AddForce(Vector3.up * 7f, ForceMode2D.Impulse);//generamos un efecto de rebote
        PuntosOro = PuntosOro + 10;//despues de la primera vez comienza a generar oro cada 10 saltos
    }
    void GenerarVida()
    {
        if (!MovimientoBases.SinEnemigos)
        {
            Vida.SetActive(true);//Activamos moneda
            GeneradorPU.isKinematic = false;//la hacemos dinamica 
            GeneradorPU.AddForce(Vector3.up * 7f, ForceMode2D.Impulse);//generamos un efecto de rebote
            PuntosVida = PuntosVida + 12;//despues de la primera vez comienza a generar oro cada 10 saltos
        }
    }
    void GenerarMultiplicador()
    {
     
        Multiplicador.SetActive(true);//Activamos moneda
        GeneradorPU.isKinematic = false;//la hacemos dinamica 
        GeneradorPU.AddForce(Vector3.up * 7f, ForceMode2D.Impulse);//generamos un efecto de rebote
        PuntosMultiplicador = PuntosMultiplicador + 30;//despues de la primera vez comienza a generar oro cada 10 saltos
    }
    void GenerarFlechaEsp()
    {

        FlechaEsp.SetActive(true);//Activamos pu
        GeneradorPU.isKinematic = false;//la hacemos dinamica 
        GeneradorPU.AddForce(Vector3.up * 7f, ForceMode2D.Impulse);//generamos un efecto de rebote
        PuntosFlechaEsp = PuntosFlechaEsp + 50;//despues de la primera vez comienza a generar oro cada 10 saltos
    }
    void GenerarMasTiempo()
    {
        if (dificultad==4)
        { 
        MasTiempo.SetActive(true);//Activamos pu
        GeneradorPU.isKinematic = false;//la hacemos dinamica 
        GeneradorPU.AddForce(Vector3.up * 7f, ForceMode2D.Impulse);//generamos un efecto de rebote
        PuntosTiempo = PuntosTiempo + 4;//despues de la primera vez comienza a generar oro cada 10 saltos
    }
 
    }
    void OnTriggerEnter2D(Collider2D ObjetoColision)//   void OnTriggerEnter2D(Collider2D objeto)
    {
        if (ObjetoColision.name == "Base2")
        {
            GeneradorPU.isKinematic = true;//Cuando colisiona el generadorPU con la base volvemos el generador estatico
      
        }
    }

    public void ActualizarOro()//Metodo para actualizar el total de oro, es llamado cada vez que el jugador colisiona con la moneda
    {
       TextOro.text = ("x" + BonusPU.TotalOro.ToString());//Actualizamos en pantalla
    }

    void PersonajePierdeVida()//En caso que el personaje falle el salto pierde la oportunidad de obtener el PowerUP
    {
        BonusPU.MultiplicadorAct = false;//Desactivamos el multiplicador
        BonusPU.FlechaEspAct = false;
        Oro.SetActive(false);
        Vida.SetActive(false);
        AnimaX2.SetActive(false);
        AnimaEsp.SetActive(false);
    }
}
