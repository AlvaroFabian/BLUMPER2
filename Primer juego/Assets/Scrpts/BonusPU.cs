using UnityEngine;
using System.Collections;

public class BonusPU : MonoBehaviour {

    private string NombrePu;//Variable que va a contener el tag del powerUP que contiene el script
    public static int TotalOro = 0;//Cantidad de oro ganado por ronda
    public GestorPowerUps GestorPU;//Referencia a el C#Script GetorPowerUps
    public GameObject Animax2;
    public static bool MultiplicadorAct = false;
    public static bool FlechaEspAct = false;
    void Start()
    {
        NombrePu = gameObject.tag;//Asiganmos el tag del objeto a la variable
    }


    void OnTriggerEnter2D(Collider2D ObjetoColision)//Metodo para detectar si el personaje toma el item
    {
      
        if(ObjetoColision.name=="BlueBodyPlay"&& NombrePu=="Oro")//Si el personaje colisiona con el Oro
        {
            if (GestorPuntos.Dificultad == 0)//Dificultad medio
            {
                TotalOro++;//Se suma una moneda al total
                gameObject.SetActive(false);//Desactivamos la moneda
                GestorPU.ActualizarOro();//Actualizamos el marcador en la pantalla
            }
            if (GestorPuntos.Dificultad==1)//Dificultad medio
            {
                TotalOro = TotalOro + 2;
                gameObject.SetActive(false);//Desactivamos la moneda
                GestorPU.ActualizarOro();//Actualizamos el marcador en la pantalla
            }
            if (GestorPuntos.Dificultad == 2 || GestorPuntos.Dificultad == 3 || GestorPuntos.Dificultad == 4)//Dificil
            {
                TotalOro = TotalOro + 3;
                gameObject.SetActive(false);//Desactivamos la moneda
                GestorPU.ActualizarOro();//Actualizamos el marcador en la pantalla
            }
            if (MultiplicadorAct)
            {
                TotalOro=TotalOro + 3;//Si esta activo el multiplicador asigna dos monedas
                gameObject.SetActive(false);//Desactivamos la moneda
                GestorPU.ActualizarOro();//Actualizamos el marcador en la pantalla
            }
            else
            {
                
            }
        }
        if (ObjetoColision.name == "BlueBodyPlay" && NombrePu == "Vida")//Si el personaje colisiona con la vida
        {
            if (MultiplicadorAct)
            {
                GestorPuntos.VidasT = GestorPuntos.VidasT + 2;//Si esta activo el multiplicador asigna dos vidas
                gameObject.SetActive(false);//Desactivamos la moneda
            }
            else
            {
                GestorPuntos.VidasT++;//Si el personaje toma la vida se le suma al total de vidas
                gameObject.SetActive(false);//Desactivamos la moneda
            }
          
        }
        if (ObjetoColision.name == "BlueBodyPlay" && NombrePu == "Multiplicador")//Si el personaje colisiona con el multiplicador
        {

            Animax2.SetActive(true);
            gameObject.SetActive(false);//Desactivamos la moneda
            MultiplicadorAct = true;//activamos el multiplicador

        }

        if (ObjetoColision.name == "BlueBodyPlay" && NombrePu == "FlechaEsp")//Si el personaje colisiona con el multiplicador
        {

            FlechaEspAct = true;
            gameObject.SetActive(false);//Desactivamos la moneda
            Animax2.SetActive(true);

        }
        if (ObjetoColision.name == "BlueBodyPlay" && NombrePu == "MasTiempo")//Si el personaje colisiona con el multiplicador
        {
            //FlechaEspAct = true;
            GestorDificultad.tiempoRestante= GestorDificultad.tiempoRestante + 2;
            gameObject.SetActive(false);//Desactivamos la moneda


        }

    }

   



    }
