using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Cambioescenatemporizador : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Invoke("Cambioescena",5f);

    }
	
	// Update is called once per frame
	void Update () {
	
	}
    void Cambioescena()
    {
   
        SceneManager.LoadScene("MainMenu");
    }

}
