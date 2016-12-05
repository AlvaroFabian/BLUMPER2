using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GestorUIGame : MonoBehaviour {
    public GameObject  grupoConfig,_Bpausa, PanelTutorial;
    public ElementoInteractivo Bplay, Breintentar, Bmenu, Bsalir, Bpausa;
    private int Dificultad;
	// Use this for init     ialization
	void Start () {
        Dificultad = PlayerPrefs.GetInt("Dificultad");
    }
	
	// Update is called once per frame
	void Update () {

        if (Bplay.pulsado)
        {
            if (PlayerPrefs.GetInt("Tutorial")==1 && Dificultad != 3 && Dificultad != 4)//Solo aparece el panel cuando esta el modo tutorial pero no esta en CR ni Supervivencia
            { PanelTutorial.SetActive(true); }
           

            _Bpausa.SetActive(true);
            Bplay.pulsado = false;
            grupoConfig.SetActive(false);
            Time.timeScale = 1;
          
        }
    

        if (Bpausa.pulsado)
        {
            PanelTutorial.SetActive(false);
            Bpausa.pulsado = false;
            grupoConfig.SetActive(true);
            Time.timeScale = 0;
           _Bpausa.SetActive(false);

        }
        if (Bmenu.pulsado)
        {
            ReiniciarMarcadores();
            Time.timeScale = 1;
            SceneManager.LoadScene("MainMenu");
        }
        if (Breintentar.pulsado)
        {
            ReiniciarMarcadores();
            Time.timeScale = 1;
            SceneManager.LoadScene("Oceano");
        }
    }

    private void ReiniciarMarcadores()//Reinicia las variables staticas entre cambios de modos de juego
    {
        GestorPuntos.PuntosGeneral = 0;
        GestorPuntos.VidasT = 10;
        MovimientoBases.Bossmode = false;
        MovimientoBases.SinEnemigos = false;
        MovimientoBases.nosuma = true;
        GestorDificultad.tiempoRestante = 61;
       BonusPU.TotalOro = 0;


    }
}
