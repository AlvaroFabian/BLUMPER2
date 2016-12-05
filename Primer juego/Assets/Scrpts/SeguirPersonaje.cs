using UnityEngine;
using System.Collections;
//Descripcion del Script
//Script encargado de controlar el comportamiento de la camara para que siga los movimientos del personaje
//Asignado a la Main Camera
public class SeguirPersonaje : MonoBehaviour
{

    public Transform Personaje; //Definimos uns variable tipo tranform donde vamos a obtener la informacion de la posisicion del personaje
    //Realizamos la referencia al transform del personaje desde el CDO
    public float Separacion = 0f;//Espacio que se movera la camara al iniciar la escena para que el personaje quede hubicado en la parte inferior izquierda de la pantalla
                                 // Update is called once per frame
    void Update()
    {//Se ejecuta siempre
        //Posision camara es igual a el valor en X del personaje, el valor Y de la camara y el valor z de la camara.
        transform.position = new Vector3(Personaje.position.x + Separacion, transform.position.y, transform.position.z);//Hacemos de durante cada frame se actualize la posision en X del personaje y definimos la posisicion (Y,Z)Con los mismos valores del trasnform que ya traia la camara
                                                                                                                        //Al referirnos al transform.position, nos estamos refiriendo al trasform que tiene los valores de la posicion de la camara
    }
}