using UnityEngine;
using System.Collections;

public class SaltoUp : MonoBehaviour {

   // public Transform CS;//Transform relacionado a el transform del comprobador de suelo

    //private float RadioDetecion = 0.6f;
    //public LayerMask MascaraSuelo;

    private float _jumpVelocityChange = 700f;//Velocidad ejercida en (X)
    private float _jumpAcceleration = 0.8f;//Aceleracion px/s
    private float _airJumpTime = 3f;//Tiempo que se le suma a el tiempo inicial de salto
    private float _startJumpTime;//Tiempo en el que comienza el salto
    private float _maxJumpTime;//Esta variable es la suma de las dos anyteriores variables

    private bool _isJumping;//Booleano que indica que el personaje esta en el aire
    private int salto1 = 0;//Sirve para salir de la condicion

    private Rigidbody2D _rigidBody;//Creamos un rigidbody para referenciarlo al rigidbody del CDO
    public static Animator animaciones;//Referenciamos al animator que contiene todas las animaciones del personaje
    public static bool Disparo = false;//Variable relacionada a la variable "Disparo" del animator para controlar la animacion del disparo

    public static bool SaltoArriba = false;//Booleano para pruebas

    private bool aux = false; //cuando se resetea el salto se inicia este bool para que el reseteo se haga una sola vez


    private bool aux3 = false;//Lo utilizamos para salirnos de la funcion que agrega furza diagonal en el salto, esto con el fin de que no se mantenga activada la animacion del salto
    private bool aux4 = false;//Lo utilizamos para que se detenga la nimacion de salto al tocar el suelo y para que se ejecute una sola vez, se reinicia en el evento del reset
    private bool aux5 = false;//Lo utilizamos para que se envie una sola vez el bit de disparo
    public ElementoInteractivo BotonSaltoDisparo, BotonDisparo;//Referencias a los botones del juego(ANDROID)
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

       // SegundoSaltoEnSuelo = Physics2D.OverlapCircle(CS.position, RadioDetecion, MascaraSuelo);//Revisanos que el personaje este en contacto con el suelo


        if ((BotonSaltoDisparo.pulsado) && (!_isJumping) && (salto1 == 0)&&(!SegundoSalto.SaltoRight))
        // if (((Input.GetMouseButtonDown(0)) || (Input.GetButton("Fire1"))) && (!_isJumping) && (salto1 == 0)&& (correcionSalto == true))//Si presionamos el boton click izquierdo o si presionamos el boton"fire"(z) y no esta saltando y ademas no a realizado el primer salto
        {
            //Reseteo del disparo///////////
            GeneradorArma.OneShot = false;//reseteo del disparo
            aux5 = false;//booleano que controla que solamente se anime una vez el disparo
            ////////////////////////////////

            _startJumpTime = Time.time;//Movemos el tiempo actual a la variable  _startJumpTime
            _maxJumpTime = _startJumpTime + _airJumpTime;// definimos   _maxJumpTime como la suma de el tiempo de inicio del salto mas el tiempo de duracion en el aire
            _rigidBody.AddForce(this.transform.up * _jumpVelocityChange, ForceMode2D.Force);//Agregamos una fuerza en el eje x con el fin de mejorar el inicio del salto
            SaltoArriba = true;//Seteamos este booleano para evitar que el script del segundo salto realize alguna accion al momento de iniciarse ya este salto
            salto1 = 1;//Ponemos esta variable en 1 para evitar que se pueda hacer un segundo salto 
            _isJumping = true;//Avisamos que el personaje esta en el aire     
           
            

        }
        else if ((BotonSaltoDisparo.pulsado) && (_isJumping) && (_startJumpTime + _maxJumpTime > Time.time)&&(!aux3))
        //else if (Input.GetMouseButton(0) && _isJumping && (_startJumpTime + _maxJumpTime > Time.time)&&(!aux))//Si al momento de saltar con el click izquierdo seguimos presionando, va a imprimir una fuerza en el eje(y) y el eje(x)
        {
            _rigidBody.AddForce(Vector3.up * _jumpAcceleration, ForceMode2D.Impulse);//Imprimimos la fuerza en el rigidbody del personaje
            //SaltoArriba = true;
            aux = false;//PILAS PUEDE QUE SE REQUIERA PONER ARRIBA
            animaciones.SetBool("Jump", true);//Iniciamos la animacion del salto
        }

        ///////////////////////////////////////////////////////////////Codigo reseteo del salto//////////////////////////////////////////////////////////////////////////////////////////////////
        if (SegundoSalto.EnSuelo)//Si esta en el suelo llama una vez la funcion de las amimaciones
        {
   
            if (!BotonSaltoDisparo.pulsado && !aux) //Si esta en el suelo y los dos botones esten sueltos permite realizar un nuevo salto
            {
                _isJumping = false;//El personaje esta en el suelo
                salto1 = 0;//Volvemos a permitir que realize salto
                SaltoArriba = false;//Ayuda control de animaciones y evita errores en la logica
                animaciones.SetBool("Jump", false);
               
            
                aux3 = false;//Permititimod que luego de tocar el suelo vuelva a realizar otros saltos en el eje Y
                aux4 = false;//Se permite que se vuelva a llamr la funcion que resetea la anikmacion del salto al caer el personaje
               
      
                aux = true; //cuando se resetea el salto se inicia este bool para que el reseteo se haga una sola vez
            }
         
        }
        if (!SegundoSalto.EnSuelo && !BotonSaltoDisparo.pulsado && _isJumping) //Si no esta en el suelo y suelto el boton del salto, evitamos que se le generen fuerzas extra al salto del personaje
        {

            _isJumping = false;//Desabilitamos todas las fuerza
        }

        if (transform.position.y > 4.2 && _isJumping)//Se utiliza como un limite para el salto sin que afecte la velocidad y configuracion del salto
        {
            _isJumping = false;//Desabilitamos todas las fuerza
        }


        //////////////////////Animacion del disparo solo activacion/////////////////////////////////////////

        if (SaltoArriba && BotonDisparo.pulsado && !aux5)//Si salto en direccion Up y presiono el boton de disparo
         {
            //animaciones.SetBool("Disparo", true);//Iniciamos la animacion del disparo
       
       Disparo = true;
            animaciones.SetBool("Disparo", Disparo);//Desactivamos la animacion del salto
            _isJumping = false;//Desabilitamos todas las fuerza
            aux5 = true;
        }


    }
    /////////////////////////////////////////////Control de la animacion del salto////////////////////////////////////////////////////////
    public void Animacion(int SaltoArriba )//Metodo utilizado para controlar la animacion del salto, este metodo es llamado al momento que finaliza la animacion para mandar su valor a 0
        //Se hizo esto para evitar que se quedara pegada la animacion de salto al caer al suelo
    {
        aux3 = true;//Seteamos este bool para evitar que el salto siga enviando la animacion de salto
       if ((SaltoArriba==1) && (!aux4))//traemos el valor entero depositado en la variuable que guarda la animacion
        {
           
            animaciones.SetBool("Jump", false);//Desactivamos la animacion del salto
         
            aux4 = true;//Activamos este bool para que esta accion solo se realize una vez//PUEDE QUE NO SEA NECESARIO
        }
      

    }
   

    void PermisivoSalto()//Acciones realizadas al momento que el personaje llega de una base a otra, obtenemos este llamado mediante las notificaciones
    {
        _isJumping = false;//El personaje esta en el suelo
        salto1 = 0;//Volvemos a permitir que realize salto
        SaltoArriba = false;//Ayuda control de animaciones y evita errores en la logica
        animaciones.SetBool("Jump", false);

   
        aux3 = false;
        aux4 = false;

        aux = true; //cuando se resetea el salto se inicia este bool para que el reseteo se haga una sola vez
    }





}
