using UnityEngine;
using System.Collections;

public class Bases : MonoBehaviour
{//Gestion y configuarcion del comportamiento de las bases
    //public static TextMesh TextoPuntos;//Texto relacionado a los puntos que recibe cada jugador por base
    //private bool Inicio = false;//Variable de seguridad para evitar generar puntos y bases adicionales, hace que solamente se hagan una vez las acciones
    //de este Script
   void Start()
    {
       // TextoPuntos.text = GeneradorBases.PuntosPorBase.ToString(); //Traemos el dato de puntos por base al momento que se inicia el script y lo movemos al texto en pantalla
    }

    void OnCollisionEnter2D(Collision2D collision){
        Destructores.Murio = false;//se resetea para permitir que vuelva avisar cuando el personaje muera
        SegundoSalto.aux3 = false;//Reseta este booleano para permitir que se vuelva a resetear la condicion de sumar puntaje
     
        if (collision.gameObject.tag == "Pies")
        {
            NotificationCenter.DefaultCenter().PostNotification(this, "PermisivoSalto");//Permitimos que se resetee el salto con esta notificacion
        }
            if (collision.gameObject.name == "BlueBodyPlay")//Si el objeto colisiono y el objeto con el que colisiono tiene como nombre "BlueBodyPlay"
            {
         
                if (!MovimientoBases.nosuma)
                {
                NotificationCenter.DefaultCenter().PostNotification(this, "PersonajeGanaPuntos");//Avisamos que el jugador gano los puntos que se encuentan actualmente en @textopuntos.text
                MovimientoBases.nosuma = true;
            }
        }
      
        }
    }
