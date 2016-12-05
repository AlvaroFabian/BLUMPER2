using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;//Libreria Adicional
using System;//Libreria Adicional
//Descripcion del Script
//Configuracion de los botones 
//Asignado a los botones (ANDROID)
public class ElementoInteractivo : MonoBehaviour, IPointerDownHandler, IPointerUpHandler//estos dos elemetos se encargan de gestionar los valores booleanos asignados a presionar y soltar el click
{
    public bool pulsado;//Asignamos este script a los botones de las flechas y al pulsar la imagen se cambiara el estado de este booleano

    

    public void OnPointerDown(PointerEventData eventData)
    {
        pulsado = true;//indica que se presiono el click o la pantalla
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        pulsado = false;//Indica que se solto el click o se dejo de presionar la pantalla
    }
    // Use this for initialization
    // public void OnPointerDown(PointerEventData eventData)
    //{

    //}

}
