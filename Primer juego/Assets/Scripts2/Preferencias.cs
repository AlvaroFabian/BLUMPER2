using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Preferencias : MonoBehaviour {
public InputField NombreJugador;//Referencia a el inputfiel del nombre del jugador
    public Text TextoActTuto;
    public Button Guardar;//Referencia boton guardar
    private string nombreJugador;//Variable que contiene el nombre del jugador y lo almacena en el player pref
    private int seguroNombre=0;//Variable de seguridad para que solo se pueda escribir una vez el nombre

    public Toggle ActivacionTutorial;

   
	// Use this for initialization
	void Start () {

        if (PlayerPrefs.GetInt("Tutorial")==1)//Verificamos si esta activo el modo tutorial
            {
            ActivacionTutorial.isOn = true;
        }
        else
        {
            ActivacionTutorial.isOn = false;
        }



        Guardar.onClick.AddListener(CargarPlayerPref);//Referencia al boton de cargar los datos
        if (PlayerPrefs.GetInt("SeguroName") == 1)//Si ya hay un nombre almacenado lo movemos al texto
        {
            NombreJugador.text = PlayerPrefs.GetString("PlayerName");
            Guardar.interactable = false;
            NombreJugador.interactable = false;
        }
        else//De lo contrario ponemos esto en el texto
        {
            NombreJugador.text = "Player0001";
            Guardar.interactable = true;
            NombreJugador.interactable = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
	/*if (ActivacionTutorial.isOn)
        {
            print("Tutorial Activo");
        }
        else
        {
            print("Tutorial Inactivo");
        }*/
	}
    void CargarPlayerPref()
    {
        print(PlayerPrefs.GetInt("SeguroName"));
        nombreJugador = NombreJugador.text;
        seguroNombre = 1;
        PlayerPrefs.SetString("PlayerName",nombreJugador);
        PlayerPrefs.SetInt("SeguroName",seguroNombre); 
    }
    public void EscribirPlayerPrefTutorial()
    {
        if (ActivacionTutorial.isOn)
        {
            PlayerPrefs.SetInt("Tutorial", 1);
            PlayerPrefs.SetInt("TutoSupOk", 0);//Rehabilitamos la informacion en modo supervivencia
            PlayerPrefs.SetInt("TutoCrOk", 0);
            TextoActTuto.text = "Activados";
            TextoActTuto.color = new Color32(57,135,57,255);
            
          
        }
        else
        {
            PlayerPrefs.SetInt("Tutorial", 0);
          
            TextoActTuto.text = "Desactivados";
            TextoActTuto.color = new Color32(171,21,6,255);
        }
    }
}
