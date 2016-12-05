using UnityEngine;
using System.Collections;

public class GestorVelocidades : MonoBehaviour
{
   

    private string NombreCDO;//Variable que va a contener el nombre de CDO para saber que velocidad asignar


    void Start()
    {
        NombreCDO = gameObject.name;//Asiganmos el nombre del objeto a la variable
        GestorVelocidad();//LLamamos al metodo que gestiona las velocidades de obetos
    }

    void GestorVelocidad()
    {
        ////////////////////////////Asiganacion de velocidad por nombres/////////////////
        if (NombreCDO == "White(Clone)")
        { GetComponent<Rigidbody2D>().AddForce(Vector3.left * 4, ForceMode2D.Impulse); }//Asignamos la velocidad a white

        else if (NombreCDO == "Erizo(Clone)")
        { GetComponent<Rigidbody2D>().AddForce(Vector3.left * 40, ForceMode2D.Impulse); }//Asignamos la velocidad a el erizo

        else if (NombreCDO == "Lanza(Clone)")
        { GetComponent<Rigidbody2D>().AddForce(Vector3.right * 12, ForceMode2D.Impulse); }//Asignamos la velocidad a la lanza
                                                                                          ////////////////////////////Asiganacion de velocidad por tag/////////////////
        else if (NombreCDO == "LanzaB(Clone)")
        { GetComponent<Rigidbody2D>().AddForce(Vector3.right * 13, ForceMode2D.Impulse); }
       // if (TagCDO == "PowerUp")
       // { GetComponent<Rigidbody2D>().AddForce(Vector3.up * 5f, ForceMode2D.Impulse); }//genera um impulso en el eje (y)}//Asignamos la velocidad a white 
    }

        void OnCollisionEnter2D(Collision2D objeto)
    
        { 
        if (objeto.gameObject.tag == "Municion"&& NombreCDO == "Erizo(Clone)")// Si colisona con el jugador 
        {
            Destroy(objeto.gameObject);//Destruye la lanza
                                       // { GetComponent<Rigidbody2D>().AddForce(Vector3.zero); }
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.right * 80, ForceMode2D.Impulse);///Devolvemos erizo 
        
        }
      
    }
}
   
