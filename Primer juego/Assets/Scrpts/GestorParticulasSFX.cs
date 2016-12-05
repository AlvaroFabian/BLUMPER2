using UnityEngine;
using System.Collections;

public class GestorParticulasSFX : MonoBehaviour
{
    public GameObject efectoParticulas;
    public GameObject efectoParticulasG;
    public GameObject EfectoParticulasBomba;

    void OnCollisionEnter2D(Collision2D objeto)
    {
        if (objeto.gameObject.tag == "Red")// Si colisona con el jugador 
        {

            Instantiate(efectoParticulas, objeto.transform.position, Quaternion.identity);
            NotificationCenter.DefaultCenter().PostNotification(this, "PiranaMurio");
            //GameObject Municion = GameObject.Find("Lanza(Clone)");//Busca la lanza y la borra
            Destroy(gameObject);//Destruye el clon
            if(gameObject.tag=="Bomba")
            {
                Instantiate(EfectoParticulasBomba, new Vector3(transform.position.x, transform.position.y , transform.position.z-3), Quaternion.identity);
                print("Bienhecho");
            }
        }
        if (objeto.gameObject.tag == "Green")// Si colisona con el jugador 
        {

            Instantiate(efectoParticulasG, objeto.transform.position, Quaternion.identity);
            NotificationCenter.DefaultCenter().PostNotification(this, "PiranaMurio");
            //GameObject Municion = GameObject.Find("Lanza(Clone)");//Busca la lanza y la borra
            Destroy(gameObject);//Destruye el clon
            if (gameObject.tag == "Bomba")
            {
                Instantiate(EfectoParticulasBomba, new Vector3(transform.position.x, transform.position.y, transform.position.z-3), Quaternion.identity);
                print("Bienhecho");
            }
        }
     
    }
}