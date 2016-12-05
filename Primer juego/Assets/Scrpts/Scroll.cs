using UnityEngine;
using System.Collections;
//Descripcion del script
//Script encargado de monitorear los casos en los cuales se debe iniciar y detener la animacion Scroll Parallax del juego
//Script asignado a las imagenes de textura agregadas en los Quad de los fondos lejano y cercano
public class Scroll : MonoBehaviour {
    public float velocidad;//Esta constante es la encargada definir a que valocidad se va a mover cada uno de los fondos
    //Al ser una variable publica su valor debe ser asiganado desde el inspector del objeto contenedor(Quad) o FondoCerca y FondoLejano
    public bool Enmovimiento = false;//Esta variable la utilizamos para controlar el efecto Scroll Parallax pero unicamente en la escena #Portada
    //Esta variable se agrega ya que si no se hiciera la animacion iniciaria hasta que el personaje empieza a correr y en la escena de portada NO HAY PERSONAJE
    private float tiempoInicio = 0f;//Esta constante sirve para mejorar el tiempo de inicio de la animacion tratando de acercarlo lo mas posible a 0
    public bool IniciarEnMovimiento = false;//Este booleano lo utilizamos para poder iniciar la animacion automaticamente en la escena de portada
    //Al ser de tipo público se le da su valor desde el inspector del objeto contenedor y al iniciar en @true la animacion podra iniciar sin tener en cuenta el estado del personaje
    // Use this for initialization
    void Start () {
        /* NotificationCenter.DefaultCenter().AddObserver(this, "PersonajeEmpiezaACorrer");//Observa la notificacion asignada cuando el personaje comienza a correr
         //Esta notificacion se da desde el C#Scrip(ControladoPersonaje) Al dar click y al verificar qu el personaje no esta quieto 0 en el suelo
         NotificationCenter.DefaultCenter().AddObserver(this, "PersonajeMuere");//Observa la notificacion generada al momento que el personaje cae de alguna de las bases                                                                         //Esta notificacion se da desde el C#Script(Destructor) y se da al momento en que el personaje entra en contacto con el collider nombrado #Destructor
         //Esta notificacion se da desde el C#Scrip(destructor) cuando el personaje choca con el collider del (destructor)
         if (IniciarEnMovimiento)//Verifica si este bit esta habilitado desde el objeto contenedor, de ser asi la animacion iniciara al iniciar la escena de lo contrario esperara hasta 
             //que se de la notificacion de que el pesrsonaje comienza a correr
         {
             Enmovimiento = true;//inicia la animacion
         }*/
        Enmovimiento = true;//inicia la animacion
    }
    void PersonajeEmpiezaACorrer()//Metodo iniciado al recibir la notificacion de que el personaje comenzo a correr 
    {
        Enmovimiento = true;//inicia la animacion
        tiempoInicio = Time.time;//Almacena el tiempo del momento cuando el personaje empieza a correr
    }
    /*void PersonajeMuere()//Metodo iniciado al recibir la notificacion de que el personaje murio 
    {
        Enmovimiento = false;//Detiene la animacion
    }*/
    // Update is called once per frame
    void Update () {
        if (Enmovimiento)//Cuando es verdadadera inicia la animacion
        {
           
            // renderer.material.mainTextureOffset = new Vector2(Time.time * velocidad, 0);//versiones anteriores de unity
            GetComponent<Renderer>().material.mainTextureOffset = new Vector2(((Time.time - tiempoInicio) * velocidad) % 1, 0);
            //Finalmente ingresamos al offset de la textura del material del conponente render y aplicamos la velocidad a la que se desplazara la animacion
         //   if(gameObject.name=="Agua"&& SegundoSalto.VelocidadAgua>0)
           // {
           //     GetComponent<Renderer>().material.mainTextureOffset = new Vector2(((Time.time - tiempoInicio) * 0.4f) % 1, 0);
           // 
           // }
        
        }
     
    }
}
