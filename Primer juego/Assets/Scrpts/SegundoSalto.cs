using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(Rigidbody2D))]
//Descripcion Script
//Script que sirve para configurar el salto de altura y distancia variable
public class SegundoSalto : MonoBehaviour
{
    public Transform CS;//Transform relacionado a el transform del comprobador de suelo
    public static bool EnSuelo;
    private float RadioDetecion = 0.1f;
    public LayerMask MascaraSuelo;

    private float _jumpVelocityChange = 700f;//Velocidad ejercida en (X)
    private float _jumpAcceleration= 0.8f;//Aceleracion px/s
    private float _airJumpTime =3f;//Tiempo que se le suma a el tiempo inicial de salto
    private float _startJumpTime;//Tiempo en el que comienza el salto
    private float _maxJumpTime;//Esta variable es la suma de las dos anyteriores variables

    private bool _isJumping;//Booleano que indica que el personaje esta en el aire
    private int salto1 = 0;//Sirve para salir de la condicion

    private Rigidbody2D _rigidBody;//Creamos un rigidbody para referenciarlo al rigidbody del CDO
    public static Animator animaciones;//Referenciamos al animator que contiene todas las animaciones del personaje
    public static bool Disparo = false;//Variable relacionada a la variable "Disparo" del animator para controlar la animacion del disparo
 
    public static bool SaltoRight = false;//Indica que el personaje esta saltando de una base a otra
    public static float VelocidadAgua;
    private bool aux = false;

    private bool aux2 = false;
    public static bool aux3 = false;
    public ElementoInteractivo BotonSaltoDisparo;//Referencias a los botones del juego(ANDROID)
    void Start()
    {
        NotificationCenter.DefaultCenter().AddObserver(this, "PermisivoSalto");//Nos inscribimos a la notificacion que indica que el personaje a llegado de una base a otra
    }
    void Awake()
    {
        _rigidBody = this.GetComponent<Rigidbody2D>();//Obtenemos el rigidbody del CDO y lo referenciamos a _rigiBody
        animaciones = GetComponent<Animator>();//Obtenemos el animator de CDO y lo movemos al animator creado
    }

    void FixedUpdate()
    {
       
        EnSuelo = Physics2D.OverlapCircle(CS.position, RadioDetecion, MascaraSuelo);//Revisanos que el personaje este en contacto con el suelo
        

        if ((BotonSaltoDisparo.pulsado) && (!_isJumping) && (salto1 == 0)&&(!SaltoUp.SaltoArriba))
        // if (((Input.GetMouseButtonDown(0)) || (Input.GetButton("Fire1"))) && (!_isJumping) && (salto1 == 0)&& (correcionSalto == true))//Si presionamos el boton click izquierdo o si presionamos el boton"fire"(z) y no esta saltando y ademas no a realizado el primer salto
        { 
            _startJumpTime = Time.time;//Movemos el tiempo actual a la variable  _startJumpTime
            _maxJumpTime = _startJumpTime + _airJumpTime;// definimos   _maxJumpTime como la suma de el tiempo de inicio del salto mas el tiempo de duracion en el aire
            _rigidBody.AddForce(this.transform.up * _jumpVelocityChange, ForceMode2D.Force);//Agregamos una fuerza en el eje x con el fin de mejorar el inicio del salto
            SaltoRight = true;
            // GeneradorBases.nosuma = false;//Reseteamos este valor para que cuando salte pueda volver a sumar puntos//IMPORTANTE
           // MovimientoBases.nosuma = false;//Reinicia la opcion de sumar puntaje luego de que el el personaje ya ha sumado
            salto1 = 1;//Ponemos esta variable en 1 para evitar que se pueda hacer un segundo salto 
            _isJumping = true;//Avisamos que el personaje esta en el aire     
           
        }
        else if ((BotonSaltoDisparo.pulsado) && (_isJumping) && (_startJumpTime + _maxJumpTime > Time.time))
        //else if (Input.GetMouseButton(0) && _isJumping && (_startJumpTime + _maxJumpTime > Time.time)&&(!aux))//Si al momento de saltar con el click izquierdo seguimos presionando, va a imprimir una fuerza en el eje(y) y el eje(x)
        {
            _rigidBody.AddForce(Vector3.up * _jumpAcceleration, ForceMode2D.Impulse);//Imprimimos la fuerza en el rigidbody del personaje
            _rigidBody.AddForce(Vector3.right * _jumpAcceleration, ForceMode2D.Impulse);//Imprimimos la fuerza en el rigidbody del personaje
   
            aux = false;
            aux2 = false;
            Animaciones();
         
        }

        ///////////////////////////////////////////////////////////////Codigo reseteo del salto//////////////////////////////////////////////////////////////////////////////////////////////////
        if (EnSuelo)
        {
         
            if (!BotonSaltoDisparo.pulsado &&!aux) //Si esta en el suelo y los dos botones esten sueltos permite realizar un nuevo salto
            {
                _isJumping = false;//El personaje esta en el suelo
                salto1 = 0;//Volvemos a permitir que realize salto
                SaltoRight = false;//Ayuda control de animaciones y evita errores en la logica
                animaciones.SetBool("Jump", false);
                aux = true;
              
            }
        }
        if (!EnSuelo && !BotonSaltoDisparo.pulsado && _isJumping) //Si no esta en el suelo y suelto el boton del salto, evitamos que se le generen fuerzas extra al salto del personaje
        {

            _isJumping = false;

        }

        if (transform.position.y > 4.2 && _isJumping)//Se utiliza como un limite para el salto sin que afecte la velocidad y configuracion del salto
        {
            _isJumping = false;//Desabilitamos todas las fuerzas
       
        }
        if(_rigidBody.velocity.x >= 4 && !aux3)
        {
            MovimientoBases.nosuma = false;//Reinicia la opcion de sumar puntaje luego de que el el personaje ya ha sumado
            aux3 = true;
        }

        VelocidadAgua = _rigidBody.velocity.x;
    }
    void Animaciones()
    {
        if ((SaltoRight) && (!EnSuelo) && (!aux2))
        {

            animaciones.SetBool("Jump", true);
            aux2 = true;
        }

    }
    void PermisivoSalto()
    {
        _isJumping = false;//El personaje esta en el suelo
        salto1 = 0;//Volvemos a permitir que realize salto
        SaltoRight = false;//Ayuda control de animaciones y evita errores en la logica
        animaciones.SetBool("Jump", false);
        aux = true;
    }
    /////////////////////////////////////////////Control de animaciones////////////////////////////////////////////////////////







}
    // This C# function can be called by an Animation Event
   


