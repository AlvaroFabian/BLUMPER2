using UnityEngine;
using System.Collections;
using Image = UnityEngine.UI.Image;

public class GreenBoss : MonoBehaviour
{
    Image BarraSalud;
    private float NewPosY;
    public bool YaSalto;
    public static float DanoRecibido;

    // Use this for initialization
    void Start()
    {
        BarraSalud = GameObject.Find("Canvas").transform.FindChild("Image (1)").transform.FindChild("BarraSalud").GetComponent<Image>();//Buscamos la imagen que nos sirve como simulador de bargraph
        GetComponent<Rigidbody2D>().AddForce(Vector3.up * 10, ForceMode2D.Impulse);  //Fuerza ejercida ala piraña al ejeY almomento que esta aparece
      }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y <= 0 )
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            NewPosY = Random.Range(1,16);
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * NewPosY, ForceMode2D.Impulse);  //Fuerza ejercida ala piraña al ejeY almomento que esta aparece

        }
    }
  void OnTriggerEnter2D(Collider2D objeto)
    {
     
       if (objeto.tag == "Municion")// Si colisona con el jugador 
        {
            DanoRecibido = 0.005f;
           // NotificationCenter.DefaultCenter().PostNotification(this, "PersonajePierdeVida");
            //GameObject Municion = GameObject.Find("Lanza(Clone)");//Busca al personaje
            Destroy(objeto.gameObject);//Destruye el clon
            BarraSalud.fillAmount = BarraSalud.fillAmount - DanoRecibido;
        }
        if (objeto.tag == "Erizo")// Si colisona con el jugador 
        {
            GameObject Erizo = GameObject.Find("Erizo(Clone)");//Busca al personaje
            Destroy(Erizo);//Destruye el clon
            DanoRecibido = 0.050f;
            //NotificationCenter.DefaultCenter().PostNotification(this, "PersonajePierdeVida");
            BarraSalud.fillAmount = BarraSalud.fillAmount - DanoRecibido;
        }
    }
}