using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnearCarreteras : MonoBehaviour
{
    // Start is called before the first frame update
    //singleton
    ObjectPooler objectPooler;

    //jugador
    public GameObject player;
    //vector 3 de la ultima carretera spawneada
    public Vector3 lastRoad;
    //vector 3 de la ultima barrera spawneada
    public Vector3 lastBarrier;
    //gameobject de la primera carretera
    public GameObject firstRoad;
    //gameobject de la primera barrera
    public GameObject firstBarrier;
    //la distancia a la que se spawnean las carreteras
    public int spawnDistance = 200;



    void Start()
    {   //hacemos una referencia al singleton para mayor rapidez de codigo
        objectPooler = ObjectPooler.Instance;
        //spawneamos la primera carretera desde la función definida en Object pooler en la posición cero y la asignamos a la variable firsroad
        firstRoad = objectPooler.SpawnFromPool("Carretera", Vector3.zero, Quaternion.identity);
        //asignamos la posición del gameobject que acabamos de spawnear a la variable vector 3 lastroad
        lastRoad = firstRoad.transform.position;
        //spawneamos la primera barrera desde la función definida en Object pooler en la posición cero y la asignamos a la variable firsroad
        firstBarrier = objectPooler.SpawnFromPool("Barreras", new Vector3(0,1,50), Quaternion.identity);
        //asignamos la posición del gameobject que acabamos de spawnear a la variable vector 3 lastBarrier
        lastBarrier = firstBarrier.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //si la distancia de la última carretera spawneada al jugador es menor que la variable spawnDistance entonces
        if ((lastRoad.z - player.transform.position.z) <= spawnDistance)
        {   //se spawnea una carretera a 50 unidades mas lejos en el eje z de la ultima spawneada, y se guardan los valores de posición de esta como la última
            objectPooler.SpawnFromPool("Carretera", lastRoad = new Vector3(lastRoad.x, lastRoad.y, lastRoad.z + 50), Quaternion.identity);
        }
        if ((lastBarrier.z - player.transform.position.z) <= spawnDistance)
        {   //se spawnea una barrera a 50 unidades mas lejos en el eje z de la ultima spawneada, y se guardan los valores de posición de esta como la última
            objectPooler.SpawnFromPool("Barreras", lastBarrier = new Vector3(lastBarrier.x, lastBarrier.y, lastBarrier.z + 50), Quaternion.identity);
        }
    }
}
