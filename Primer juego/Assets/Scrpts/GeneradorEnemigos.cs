using UnityEngine;
using System.Collections;

public class GeneradorEnemigos : MonoBehaviour {

    public Transform GeneradorWhite;//Objeto que sirve para decirle al programa en que lugar debe generar la piraña Opcional
    public GameObject[] Enemies;//Array que contiene el enemigo a generar
    public float TiempoMin, TiempoMax, TiempoMinE, TiempoMaxE;//Tiempo max y min para la generacion de aves y erizos
    private bool inicio = false;//Variable que indica que ya se genero la primera ave y evitar que se creen adicionales
    private string TipoEnemigo;//Cadena que almacena el nombre del CDO

    Vector2 PosicionInicialBomba;//Almacena la poscicion inicial del enemigo "white"
  
    
    void GenerarWhite()//Genera pirañas
    {
        {
        
            if (GestorPuntos.PuntosGeneral >= 200 && !MovimientoBases.Bossmode && !MovimientoBases.SinEnemigos)
            {
                //GeneradorPiranas.position = new Vector2(transform.position.x - NewPOSx2 - 1, transform.position.y);//Hubicamos el generador en la mitad de las bases

                // White.GetComponent<Rigidbody2D>().AddForce(Vector3.left * 4, ForceMode2D.Impulse);//Le asignamos una velocidad en su eje -x
                
                Instantiate(Enemies[Random.Range(0, Enemies.Length)], GeneradorWhite.transform.position, Quaternion.identity);//luego de generar el numero aleatorio tomamos el objeto asiganado a ese numero y lo ponemos en la posision del objeto al que se le haya asiganado este scrip
                Invoke("GenerarWhite", Random.Range(TiempoMin, TiempoMax));//volvemos a llamar la subfuncion "GenerarPiranas" 1 vez cada que se cumpla un lapso comprendido en un numero aleatorio que se encuentra entre los valores maximo y minimo del random.range   
                inicio = true;
            }
        }
    }
    void GeneradorErizos()//Genera Erizos
    {
        
            if (MovimientoBases.Bossmode)
            {
               Instantiate(Enemies[Random.Range(0, Enemies.Length)], GeneradorWhite.transform.position, Quaternion.identity);//luego de generar el numero aleatorio tomamos el objeto asiganado a ese numero y lo ponemos en la posision del objeto al que se le haya asiganado este scrip
               Invoke("GeneradorErizos", Random.Range(TiempoMinE, TiempoMaxE));//volvemos a llamar la subfuncion "GenerarPiranas" 1 vez cada que se cumpla un lapso comprendido en un numero aleatorio que se encuentra entre los valores maximo y minimo del random.range   
            
               }
       
    }
    // Use this for initialization
    void Start () {
       NotificationCenter.DefaultCenter().AddObserver(this, "PermisivoSalto");//Nos inscribimos a la notificacion que indica que el personaje a llegado de una base a otra
        //TipoEnemigo = gameObject.name;//Obtenemos el nombre del CDO para saber cual enemigo generar
        GeneradorErizos();
        // PosicionInicialBomba = White.transform.position;//Igualamos la posicion inical de white con la posicion de respaldo para rehubicar el ave
    }
    void PermisivoSalto()
    {
        if (!inicio)//Despues de llamar la primera vez no tiene encuenta la ave generada cada vez que el personaje llega a una base
        {
           
            GenerarWhite();//iniciamos los metodos una sola vez para evitar enemigos adicionales
                           //GenerarErizos();
          //  inicio = true;
        }
        
    }



}
