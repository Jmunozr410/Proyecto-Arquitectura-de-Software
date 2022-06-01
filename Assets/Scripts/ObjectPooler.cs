using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    //Vamos a crear un diccionario que guaradara diferentes pools de Gameobjects, dentro de cada pool habra una que funcionar� como una lista

    //Esto se a�ade para que la clase Pool sea editable y visible desde el editor de unity
    [System.Serializable]
    //esta clase nos permitir� crear todas las pools que queramos desde el inspector de unity
    public class Pool
    {
        //tag que definir� los gameobjects de esta pool
        public string tag;
        //prefab de los gameobjects de esta pool
        public GameObject prefab;
        //numero de gameobjects que se crear�n antes de empezar a reutilizarlos
        public int size;
    }


    #region Singleton
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion




    //creamos una lista de pools para poder definir el numero de pools dentro de unity
    public List<Pool> pools;
    //creamos el diccionario de tipo string que almacenar� una queue de tipo gameobject
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start()
    {
        //definimos un nuevo diccionario que rellenaremos mediante el inspector de unity gracias a la clase Pool
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        //vamos a a�adir cada pool al diccionario e instanciar cada gameobject para tenerlos reservado para cuando se vaya a usar, para ello se desactivar�
        //para cada pool que queramos crear 
        foreach (Pool pool in pools)
        {
            //creamos una queue llena de objects
            Queue<GameObject> objectPool = new Queue<GameObject>();
            //nos aseguramos de a�adir todos los objetos a la queue
            for (int i = 0; i < pool.size; i++)
            {
                //instanciamos una copia del prefab y la referenciamos como obj
                GameObject obj = Instantiate(pool.prefab);
                //lo desactivamos
                obj.SetActive(false);
                //y lo a�adimos al final de la queue
                objectPool.Enqueue(obj);

            }
            //finalmente a�adimos esta pool al diccionario
            poolDictionary.Add(pool.tag, objectPool);
        }
    }
    //creamos una funci�n para spawnear objetos de la pool, donde la pool sea la correspondiente al tag e indiquemos la posicion y la rotaci�n deseada
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {

        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("La pool con el tag" + tag + "no existe");
            return null;
        }
        //seleccionamos el primer objeto en la queue referente a la pool del tag, lo sacamos del queue y lo asignamos a la variable objectToSpawn
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        //despues los volvemos activo
        objectToSpawn.SetActive(true);
        //le indicamos la posici�n donde debe colocarse
        objectToSpawn.transform.position = position;
        //le indicamos la rotaci�n en la que debe colocarse
        objectToSpawn.transform.rotation = rotation;

        //lo a�adimos a la queue cuando la queue se quede sin gameobject que spawnear
        poolDictionary[tag].Enqueue(objectToSpawn);

        //esto nos permite obtener el objeto que hallamos spawneado
        return objectToSpawn;
    }
}

