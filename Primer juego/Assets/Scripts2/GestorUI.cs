using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class GestorUI : MonoBehaviour {
   
    public ElementoInteractivo ConfigB, TutorialB, PlayB, RankB, StoreB, LogrosB,MenuB;//Referencia a los scripts de los botones del main menu
    public GameObject JugarG, RankingG, ConfigG, Panel;//Referencia a los grupos para mostrar segun boton oprimido
    //public Toggle Black, DarkBlue, DarkRed;//Tpggle de seleccion color de fondo
    private int BotonActivo = 0;//Variable que cambia segun el boton clickeado para animar colores
    public Image Config,Tutorial,Play,Rank,Store,Logros;//Referencia a los botones del main menu
    public Text Titulo,DificultadText, RecordSupervivencia,TxtLista;//Pausa,Fin del juego
    public Camera camara;//Referencia a la camara para cambiar su color de fondo
    public Dropdown Dificultad, Modo;//Referencia a los selectores de nivel y modo de juego
	// Use this for initialization
	void Start () {
     
        Play.color = new Color32(255, 255, 255, 255);
       // Dificultad.onValueChanged.AddListener(delegate{SeleccionDificultad(); });//Utilizamos el evento onvaluechanged del dropdowm para modificar el valor de la dificultad del juego
        Modo.onValueChanged.AddListener(delegate { SeleccionModo(); });//Utilizamos el evento onvaluecha nged del dropdowm para modificar el valor de la dificultad del juego
        //Dificultad.value = PlayerPrefs.GetInt("Dificultad");
        RecordSupervivencia.text = ("Tu mejor puntaje: " + PlayerPrefs.GetInt("RecordSurvivor").ToString());
        switch ( PlayerPrefs.GetString("ModoJuego"))//obtenemos el player pref de modo de juego para cargarlo al dropdownlist
        {
            case ("Aventura"):
                Modo.value = 0;
                break;
            case ("Supervivencia"):
                Modo.value = 1;
                break;
            case ("Contrarreloj"):
                Modo.value = 2;
                break;
              

        }
        switch (PlayerPrefs.GetInt("Dificultad"))
        {
            case (0):
                TxtLista.text = "Facil";
                break;
            case (1):
                TxtLista.text = "Medio";
                break;
            case (2):
                TxtLista.text = "Dificil";
                break;
              

        }
      //  print(PlayerPrefs.GetInt("Dificultad")); // PlayerPrefs.SetInt("Dificultad", 1);//Iniciamos la dificultad en facil
    }
	
	// Update is called once per frame
	void Update () {

        if (MenuB.pulsado)
        {
            //JugarG.SetActive(false);
            RankingG.SetActive(false);
            ConfigG.SetActive(false);
            //Panel.SetActive(false);
            MenuB.pulsado = false;
            MenuB.gameObject.SetActive(false);
            Titulo.text = "Seleccion modo de juego";
        }
        //Mantenemos llamando a la animacion del color de los botones
        if (RankB.pulsado)
        {
            MenuB.gameObject.SetActive(true);
            BotonActivo = 1;
            Titulo.text = "Ranking Supervivencia";
            AnimacionBotones();
        }
  else if (StoreB.pulsado)
        {
            BotonActivo = 2;
            Titulo.text = "Tienda";
            AnimacionBotones();
        }
        else if (LogrosB.pulsado)
        {
            BotonActivo = 3;
            Titulo.text = "Logros";
            AnimacionBotones();
        }

        else if (ConfigB.pulsado)
        {
            MenuB.gameObject.SetActive(true);
            BotonActivo = 4;
            Titulo.text = "Configuracion";
            AnimacionBotones();
        }

        else if (TutorialB.pulsado)
        {
            BotonActivo = 5;
            Titulo.text = "Tutorial";
            AnimacionBotones();
        }

        else if (PlayB.pulsado)
        {
            BotonActivo = 6;
            Titulo.text = "Jugar";
            AnimacionBotones();
        }






    }

    void AnimacionBotones()//Color de los botones segun seleccion
    {
        if (BotonActivo != 1)
        {

            //Rank.color = new Color32(255, 255, 255, 50);
            RankingG.SetActive(false);
        }
        else
        {
            //Rank.color = new Color32(255, 255, 255, 255);
            RankingG.SetActive(true);
            Panel.SetActive(true);
        }


        if (BotonActivo != 2)
        {
           // Store.color = new Color32(255, 255, 255, 50);
        }
        else
        {
           // Store.color = new Color32(255, 255, 255, 255);
        }


        if (BotonActivo != 3)
        {
           // Logros.color = new Color32(255, 255, 255, 50);
        }
        else
        {
           // Logros.color = new Color32(255, 255, 255, 255);
        }


        if (BotonActivo != 4)
        {
           // Config.color = new Color32(255, 255, 255, 50);
            ConfigG.SetActive(false);
           
        }
        else
        {
           // Config.color = new Color32(255, 255, 255, 255);
            ConfigG.SetActive(true);
            Panel.SetActive(true);
        }

        if (BotonActivo != 5)
        {
           // Tutorial.color = new Color32(255, 255, 255, 50);
        }
        else
        {
           // Tutorial.color = new Color32(255, 255, 255, 255);
        }
        if (BotonActivo != 6)
        {
            //Play.color = new Color32(255, 255, 255, 50);
           // JugarG.SetActive(false);
        
        }
        else
        {
            //Play.color = new Color32(255, 255, 255, 255);
            JugarG.SetActive(true);
            Panel.SetActive(true);
        }
    }
       /* void ColorFondoCambio()
    {
        if(ColorFondo==1)
        {
       
            camara.backgroundColor = new Color32(0, 10, 13, 0);
            DarkRed.isOn = false;
            Black.isOn = false;
        }
        if (ColorFondo == 2)
        {
         
            camara.backgroundColor = new Color32(0, 0, 0, 255);
            DarkRed.isOn = false;
            DarkBlue.isOn = false;
        }
        if (ColorFondo == 3)
        {
       
            camara.backgroundColor = new Color32(17, 10, 13, 255);
            Black.isOn = false;
            DarkBlue.isOn = false;
        }

    }*/

    public void SeleccionDificultad(int Dificultad)
    {
        switch(Dificultad)
        {
            ////////////////Seleccion de dificultad y almacenaje como playerpref//////////////////////
            case 0:
                PlayerPrefs.SetInt("Dificultad", 0);
                TxtLista.text = "Facil";
                break;

            case 1:
                PlayerPrefs.SetInt("Dificultad", 1);
                TxtLista.text = "Medio";
                break;

            case 2:
                PlayerPrefs.SetInt("Dificultad", 2);
                TxtLista.text = "Dificil";
                break;
        }
      


    }
    void SeleccionModo()
    {
     
        switch (Modo.value)
        {
            ////////////////Seleccion de modo de juego y almacenaje como playerpref//////////////////////
            case 0:
                PlayerPrefs.SetString("ModoJuego", "Aventura");
                Dificultad.interactable = true;
                Dificultad.image.color = new Color32(255, 255, 255, 255);
                DificultadText.color = new Color32(0, 136, 183, 255);
                Dificultad.value = 0;
                PlayerPrefs.SetInt("Dificultad", 0);
                break;

            case 1:
                PlayerPrefs.SetString("ModoJuego", "Supervivencia");
                Dificultad.interactable = false;//Desactivamos la opcion de elegir dificultad
                Dificultad.image.color = new Color32(156,161,175,141);
                DificultadText.color = new Color32(255, 255, 255, 255);
                Dificultad.value = 2;
                PlayerPrefs.SetInt("Dificultad", 3);
                break;

            case 2:
                PlayerPrefs.SetString("ModoJuego", "Contrarreloj");
                Dificultad.image.color = new Color32(156, 161, 175, 141);
                DificultadText.color = new Color32(255,255,255,255);
                Dificultad.interactable = false;//Desactivamos la opcion de elegir dificultad
                Dificultad.value = 2;
                PlayerPrefs.SetInt("Dificultad", 4);
                break;
        }
    }
 

    }
    