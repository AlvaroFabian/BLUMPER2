using UnityEngine;
using System.Collections;

public class Bomba : MonoBehaviour {
   public Rigidbody2D BombaRig;
    public Collider2D Bombacoll;
   
    void Start()
    {
       // BombaRig = GetComponent<Rigidbody2D>();
    }
	void OnTriggerEnter2D(Collider2D Objeto)
    {
       // Debug.Log(Objeto.tag);
       if (Objeto.tag == "Municion")//Si colisiona con un objeto con el tag"flecha"
        {
         
            Destroy(Objeto.gameObject);//Destruye el clon
        }


     
        BombaRig.isKinematic = false;
        Bombacoll.isTrigger = false;
    }
}
