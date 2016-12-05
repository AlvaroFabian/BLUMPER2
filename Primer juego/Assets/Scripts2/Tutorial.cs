using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Tutorial : MonoBehaviour {
    public GameObject PanelTutorial, BotonB, Oro, Vida, BotonB2, Bomba, Multiplicador, Especial,Globo, blue;
    public Text _TextoInfo, TextoSuperior;
    private string TextoInfo;
    private int DificultadGameMode;
    private int PasosTutorial =0;
    private int TutorialActivo = 1;
    public Button BotonOK, BotonOKCR;
    // Use this for initialization
    void Start () {
        DificultadGameMode = PlayerPrefs.GetInt("Dificultad");//Obtenemos el playerpref que contiene la dificultad de juego
        TutorialActivo= PlayerPrefs.GetInt("Tutorial");//Obtenemos el valor que nos dice si esta activo el modo tutorial
        if (TutorialActivo == 1)
        {
            switch (DificultadGameMode)
            {
                case 0://Facil
                    PanelTutorial.SetActive(true);
                    _TextoInfo.text = "Presiona (B) para saltar de una base a otra";
                    TextoSuperior.text = "Tutorial Salto";
                    BotonB.SetActive(true);
                    break;

                case 1://Medio
                    PanelTutorial.SetActive(true);
                    _TextoInfo.text = "Presiona (B) para saltar de una base a otra";
                    TextoSuperior.text = "Tutorial Salto";
                    BotonB.SetActive(true);
                    break;
                case 2://Dificil
                    PanelTutorial.SetActive(true);
                    _TextoInfo.text = "Presiona (B) para saltar de una base a otra";
                    TextoSuperior.text = "Tutorial Salto";
                    BotonB.SetActive(true);
                    break;
                case 3://Supervivencia
                   
                    break;
                case 4://Contrareloj
                  
                    break;


            }
        }
        if (DificultadGameMode == 3 && PlayerPrefs.GetInt("TutoSupOk")!=1)
        {
            PanelTutorial.SetActive(true);
            _TextoInfo.text = "Consigue la mayor cantidad de puntos con una sola vida, si obtienes un puntaje alto apareceras en el ranking";
            TextoSuperior.text = "Informacion";
            BotonB.SetActive(false);
            BotonOK.gameObject.SetActive(true);
        }
        if (DificultadGameMode == 4 && PlayerPrefs.GetInt("TutoCrOk") != 1)
        {
            PanelTutorial.SetActive(true);
            _TextoInfo.text = "Consigue la mayor cantidad de puntos en 1 minuto, el item +2 suma un par de segundos al tiempo restante";
            TextoSuperior.text = "Informacion";
            BotonB.SetActive(false);
            BotonOKCR.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
    void Update()
    {
        if (TutorialActivo == 1&&(DificultadGameMode != 4 && DificultadGameMode != 3))
        {
            if (GestorPuntos.PuntosGeneral > 0 && PasosTutorial == 0)
            {
                PanelTutorial.SetActive(true);
                _TextoInfo.text = "¡Bien hecho!, trata de nuevo";
                TextoSuperior.text = "Tutorial Salto";
                PasosTutorial = 1;
            }
            if (MovimientoBases.ConteoSaltos == 5 && PasosTutorial == 1)//Genero la  primera moneda
            {
                PanelTutorial.SetActive(true);
                _TextoInfo.text = "Recoge oro durante la partida y utilizalo para desbloquear objetos en la tienda";
                TextoSuperior.text = "Tutorial Oro";
                Oro.SetActive(true);
                BotonB.SetActive(false);
                PasosTutorial = 2;
            }
            if (MovimientoBases.ConteoSaltos == 7 && PasosTutorial == 2)//Genero la primera vida
            {
                PanelTutorial.SetActive(true);
                _TextoInfo.text = "Recoge vidas durante la partida, si caes en el salto la perderas";
                TextoSuperior.text = "Tutorial Vidas";
                Oro.SetActive(false);
                Vida.SetActive(true);
                PasosTutorial = 3;
                Invoke("CerrarPanel", 5);
            }
            if (GestorPuntos.PuntosGeneral >= 100 && PasosTutorial == 3)//Genera primera piraña
            {
                PanelTutorial.SetActive(true);
                _TextoInfo.text = "Deshazte de los enemigos presionando(A) para calcular la altura y luego(B) para disparar";
                TextoSuperior.text = "Tutorial Enemigos";
                BotonB2.SetActive(true);
                Vida.SetActive(false);
                PasosTutorial = 4;
                Invoke("CerrarPanel", 5);
            }
            if (GestorPuntos.PuntosGeneral >= 200 && PasosTutorial == 4)//Genera ave
            {
                PanelTutorial.SetActive(true);
                _TextoInfo.text = "Ten cuidado con las bombas, te pueden impulsar fuera de las bases, puedes dispararles para que caigan antes de tiempo";
                TextoSuperior.text = "Tutorial Bomba";
                Bomba.SetActive(true);
                BotonB2.SetActive(false);
                PasosTutorial = 5;
                Invoke("CerrarPanel", 6);
            }
            if (MovimientoBases.ConteoSaltos == 29 && PasosTutorial == 5)//Genero multipicador
            {
                PanelTutorial.SetActive(true);
                _TextoInfo.text = "Mientras este activo recibes el doble de puntos por base y el doble de vidas";
                TextoSuperior.text = "Multiplicador";
                Bomba.SetActive(false);
                Multiplicador.SetActive(true);
                PasosTutorial = 6;
                Invoke("CerrarPanel", 6 );
            }
            if (MovimientoBases.ConteoSaltos == 49 && PasosTutorial == 6)//Genero especial
            {
                PanelTutorial.SetActive(true);
                _TextoInfo.text = "Mientras este activo recibes una moneda por cada salto y recibes puntos por cada enemigo que elimines";
                TextoSuperior.text = "Especial";
                Multiplicador.SetActive(false);
                Especial.SetActive(true);
                PasosTutorial = 7;
                Invoke("CerrarPanel", 8);
            }
            if (MovimientoBases.ConteoSaltos == 53 && PasosTutorial == 7)
            {
                PanelTutorial.SetActive(true);
                _TextoInfo.text = "Llega a 1000 puntos y acaba con el enemigo final, disparando a sus puntos debiles";
                TextoSuperior.text = "Objetivo";
                Especial.SetActive(false);
                Globo.SetActive(true);
                PasosTutorial = 8;
                Invoke("CerrarPanel", 6);
            }
            if (MovimientoBases.ConteoSaltos == 57 && PasosTutorial == 8)//Genero la primera moneda
            {
                PanelTutorial.SetActive(true);
                _TextoInfo.text = "Puedes volver a activarlo en las configuraciones";
                TextoSuperior.text = "Fin del tutorial";
                PlayerPrefs.SetInt("Tutorial",0);
                Globo.SetActive(false);
                blue.SetActive(true);
                PasosTutorial = 9;
                Invoke("CerrarPanel", 10);
            }
           
        }
        else
        {
            if(DificultadGameMode!=4 && DificultadGameMode != 3)
            { 
            PanelTutorial.SetActive(false);//Si no esta activo el modo tutorial desactivamos la visibilidad del panel
            }
        }
         if (GestorPuntos.VidasT <= 0&& DificultadGameMode != 4)
            {
                CerrarPanel();
            }
    }
    void CerrarPanel()
    {
        PanelTutorial.SetActive(false);
    }
   public void TutoSupervivenciaVisto()
    {
        PlayerPrefs.SetInt("TutoSupOk",1);
        PanelTutorial.SetActive(false);
    }
    public void TutoCrVisto()
    {
        Time.timeScale = 1;
        PlayerPrefs.SetInt("TutoCrOk", 1);
        PanelTutorial.SetActive(false);
    }
}
   


              
