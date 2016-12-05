using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class CambioESC : MonoBehaviour {
    public ElementoInteractivo Jugar;
    public GameObject GrupoBotones;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if(Jugar.pulsado)
        {
            SceneManager.LoadScene("Oceano");
        }
    
    }
    public void EsconderPlay()
    {
        gameObject.SetActive(false);
        GrupoBotones.SetActive(true);
    }
   public  void MostarPlay()
    {
        gameObject.SetActive(true);
        GrupoBotones.SetActive(false);
    }
}
