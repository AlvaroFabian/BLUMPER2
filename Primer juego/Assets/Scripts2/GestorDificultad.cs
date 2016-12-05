using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
//Gestion del tamaño de las bases segun nivel de dificultad
public class GestorDificultad : MonoBehaviour {
    //Gestion dificultad//
   public GameObject basePrincipal1, baseMedia1, baseFacil1, basePrincipal2, baseMedia2, baseFacil2,BaseHielo1, BaseHielo2, Basecr1, Basecr2;
    public int Dificultad;
    public SpriteRenderer basePrincipal, baseMedia, baseFacil, base2Principal, base2Media, base2Facil;
    public BoxCollider2D ColliderBasePrincipal;
    public TextMesh textotiempo;
    private float tiempoinicio = 3.99f;
    public static  float tiempoRestante =61;
    private int auxtiempo;
    public SegundoSalto Salto;
    void Start () {
       Dificultad = PlayerPrefs.GetInt("Dificultad");//Obtenemos el playerpref que contiene la dificultad de juego
    
        if(Dificultad==4)
        {
            Salto.gameObject.SetActive(false);
            textotiempo.gameObject.SetActive(true);
        }
        BaseDificultad();
    }

    void BaseDificultad()
    {
        switch (Dificultad)
        {
            case 0://Facil
                basePrincipal.color = new Color32(255, 255, 255, 0);
                base2Principal.color = new Color32(255, 255, 255, 0);
                // ColliderBasePrincipal.size = new Vector2();
                basePrincipal1.GetComponent<BoxCollider2D>().size = new Vector2(3.08f, 0.8944392f);
                basePrincipal2.GetComponent<BoxCollider2D>().size = new Vector2(3.08f, 0.8944392f);
                baseFacil1.SetActive(true);
                baseFacil2.SetActive(true);
                break;

            case 1://Medio
                basePrincipal.color = new Color32(255, 255, 255, 0);
                base2Principal.color = new Color32(255, 255, 255, 0);
                basePrincipal1.GetComponent<BoxCollider2D>().size = new Vector2(2.67f, 0.8944392f);
                basePrincipal2.GetComponent<BoxCollider2D>().size = new Vector2(2.67f, 0.8944392f);
                baseMedia1.SetActive(true);
                baseMedia2.SetActive(true);
                break;

          
            case 3://Dificil
                basePrincipal.color = new Color32(255, 255, 255, 0);
                base2Principal.color = new Color32(255, 255, 255, 0);
                ///basePrincipal1.GetComponent<BoxCollider2D>().size = new Vector2(2.67f, 0.8944392f);
                ///basePrincipal2.GetComponent<BoxCollider2D>().size = new Vector2(2.67f, 0.8944392f);
                BaseHielo1.SetActive(true);
                BaseHielo2.SetActive(true);
                break;
            case 4://Dificil
                basePrincipal.color = new Color32(255, 255, 255, 0);
                base2Principal.color = new Color32(255, 255, 255, 0);
                ///basePrincipal1.GetComponent<BoxCollider2D>().size = new Vector2(2.67f, 0.8944392f);
                ///basePrincipal2.GetComponent<BoxCollider2D>().size = new Vector2(2.67f, 0.8944392f);
                Basecr1.SetActive(true);
                Basecr2.SetActive(true);
                break;
        } 

    }
    void Update()
    {
        if(Dificultad==4)//Si esta activo el modo contrarreloj
        {
            tiempoinicio -= Time.deltaTime;
            auxtiempo = (int) tiempoinicio;
            textotiempo.text = auxtiempo.ToString();
            if(tiempoinicio<=1)
            {
                auxtiempo = (int)tiempoRestante;
                textotiempo.text = auxtiempo.ToString();
                textotiempo.fontSize = 50;
                Salto.gameObject.SetActive(true);
                tiempoRestante -= Time.deltaTime;
                if(auxtiempo==0)
                {
                    NotificationCenter.DefaultCenter().PostNotification(this, "TiempoAcabado");
                    textotiempo.gameObject.SetActive(false);
                }
            }
        }
    }
}
