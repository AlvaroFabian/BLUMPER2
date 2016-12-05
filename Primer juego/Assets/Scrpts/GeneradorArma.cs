using UnityEngine;
using System.Collections;

public class GeneradorArma : MonoBehaviour {

    public Transform GeneradorLanza;//Objeto que sirve para decirle al programa en que lugar debe generar la lanza
    public GameObject Armas,FlechaESP;
    //public float TiempoMin, TiempoMax;

    public static bool OneShot = true;

    // private bool inicio = false;//Variable que indica que ya se genero la primera ave y evitar que se creen adicionales
    void Start()
    {
        NotificationCenter.DefaultCenter().AddObserver(this, "PermisivoSalto");//Nos inscribimos a la notificacion que indica que el personaje a llegado de una base a otra
    }
    void PermisivoSalto()
    {
        //OneShot = false;
    }
    void FixedUpdate()
    {
        if(SaltoUp.Disparo)
        {
            GeneraDisparo();//Llamamos a la funcion que genera la flecha
        }
    }
 
    void GeneraDisparo()//Genera pirañas
    {
        if (!OneShot && !BonusPU.FlechaEspAct)//Condicion para que solo dispare uuna vez
        {
            //GeneradorPiranas.position = new Vector2(transform.position.x - NewPOSx2 - 1, transform.position.y);//Hubicamos el generador en la mitad de las bases
            Instantiate(Armas, GeneradorLanza.transform.position, Quaternion.identity);//luego de generar el numero aleatorio tomamos el objeto asiganado a ese numero y lo ponemos en la posision del objeto al que se le haya asiganado este scrip
            SaltoUp.Disparo = false;
            SaltoUp.animaciones.SetBool("Disparo", SaltoUp.Disparo);//Desactivamos la animacion del salto
            OneShot = true;                                                                                                     // Invoke("GeneraDisparo", Random.Range(TiempoMin, TiempoMax));//volvemos a llamar la subfuncion "GenerarPiranas" 1 vez cada que se cumpla un lapso comprendido en un numero aleatorio que se encuentra entre los valores maximo y minimo del random.range   
        }
        if (!OneShot && BonusPU.FlechaEspAct)//Condicion para que solo dispare uuna vez
        {
            //GeneradorPiranas.position = new Vector2(transform.position.x - NewPOSx2 - 1, transform.position.y);//Hubicamos el generador en la mitad de las bases
            Instantiate(FlechaESP, GeneradorLanza.transform.position, Quaternion.identity);//luego de generar el numero aleatorio tomamos el objeto asiganado a ese numero y lo ponemos en la posision del objeto al que se le haya asiganado este scrip
            SaltoUp.Disparo = false;
            SaltoUp.animaciones.SetBool("Disparo", SaltoUp.Disparo);//Desactivamos la animacion del salto
            OneShot = true;                                                                                                     // Invoke("GeneraDisparo", Random.Range(TiempoMin, TiempoMax));//volvemos a llamar la subfuncion "GenerarPiranas" 1 vez cada que se cumpla un lapso comprendido en un numero aleatorio que se encuentra entre los valores maximo y minimo del random.range   
        }


    }

}
