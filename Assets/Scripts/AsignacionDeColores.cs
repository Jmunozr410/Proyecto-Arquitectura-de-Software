using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsignacionDeColores : MonoBehaviour
{
    //Definimos un array que contendrá las diferentes barreras del camino
    public GameObject[] barreras;
    //Definimos una lista con las diferentes posiciones en las que pueden colocarse
    public List<float> xPositions = new List<float>();


    void Start()
    {
        //Hacemos un bucle para cambiar la colocación de las barreras de forma aleatoria, asignado los valores de la lista al tranform.position de cada carril cambiando su posición en la x, para despues eliminar esa posición de la lista
        for (int i = 0; i < 5; i++)
        {
            int posicionLista = Random.Range(0, xPositions.Count );
            barreras[i].transform.position = new Vector3(xPositions[posicionLista],transform.position.y,transform.position.z);
            xPositions.RemoveAt(posicionLista);
        }
    }

   
}
