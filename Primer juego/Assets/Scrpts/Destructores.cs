using UnityEngine;
using System.Collections;

public class Destructores : MonoBehaviour {
    //Destruye los objetos que colisionan con el CDO
   public static bool Murio;

    public GeneradorEnemigos GeneradorEnemies;
    void OnTriggerEnter2D(Collider2D objeto)
    {
        if (objeto.tag == "Pies"&&!Murio)// Si colisona con el jugador 
        {
            NotificationCenter.DefaultCenter().PostNotification(this, "PersonajePierdeVida");
            MovimientoBases.nosuma = true;//En caso de que el personaje muera seteamos este booleano para evitar que se sumen otra vez los puntos de la misma base
            Murio = true;
           
        }
       if (objeto.tag == "Red")//Si colisiona con la pirañas roja, busca el clon de la praña y lo destruye

        {NotificationCenter.DefaultCenter().PostNotification(this, "PiranaMurio");}


        if (objeto.tag == "White")//Si colisiona con el ave blanca, busca el clon del ave y lo destruye
        {
            Destroy(objeto.gameObject, 3);
        }
       
        if (objeto.tag == "Municion")//Si colisiona con la base 
        {
            GameObject Municion = GameObject.Find("Lanza(Clone)");//Busca al personaje
            Destroy(Municion);//Destruye el clon
        }

        if (objeto.tag == "Erizo")//Si colisiona con la base 
        {
            GameObject Erizo = GameObject.Find("Erizo(Clone)");//Busca al personaje
            Destroy(Erizo);//Destruye el clon
        }

        if (objeto.tag == "Green")//Si colisiona con la base 
        {
            NotificationCenter.DefaultCenter().PostNotification(this, "PiranaMurio");
        }

        if (objeto.tag == "Particulas")//Si colisiona con la base 
        {
            Destroy(objeto.gameObject);
        }


    }
}
