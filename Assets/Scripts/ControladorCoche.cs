using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorCoche : MonoBehaviour
{

    #region Singleton
    public static ControladorCoche Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion


    JsonEncript jsonEncript;
    public Text yourScoreT;
    public int yourScore;
    public float velocidad;
    public CharacterController characterController;
    public int carrilAct;
    public float duration = 1;
    public string[] colores;
    public string color;
    public Material[] materials;
    public bool hasPerdido;
    public bool puedoMoverme = true;
    

    void Start()
    {
        //hacemos una referencia al singleton
        jsonEncript = JsonEncript.Instance;
        //asignamos el character controller a la variable
        characterController = GetComponent<CharacterController>();
        //establecemos que el carril inicial en el que nos encontramos es el 3 osea el del medio
        carrilAct = 3;
        //asignamos un color aleatorio al coche al inicio de la partida
        color = colores[Random.Range(0,5)];
        
    }

    // Update is called once per frame
    void Update()
    {
        //si el character controller esta activo movemos el coche hacia delante a la velocidad dada
        if (characterController.enabled==true)
        {
            characterController.Move(Vector3.forward * Time.deltaTime * velocidad);
        }
        //si pulsamos la tecla A el coche se mueve un carril a la izquierda si no se encuentra en el ultimo carril y si no se ha perdido, y se resta un carril al carril actual
        if (Input.GetKeyDown(KeyCode.A) & carrilAct > 1 & hasPerdido==false & puedoMoverme)
        {
            StartCoroutine(MoveCar(transform.position + new Vector3(-3.5f, 0, 0)));
            carrilAct--;
        }
        //si pulsamos la tecla D el coche se mueve un carril a la derecha si no se encuentra en el ultimo carril y si no se ha perdido, y se suma un carril al carril actual
        if (Input.GetKeyDown(KeyCode.D) & carrilAct < 5 & hasPerdido == false & puedoMoverme)
        {
            StartCoroutine(MoveCar(transform.position + new Vector3(3.5f, 0, 0)));
            carrilAct++;
        }
        //asignamos un material al coche dependiendo del color que se le haya asignado
        switch (color)
        {
            case "Verde":
                this.GetComponent<MeshRenderer>().material = materials[0];
                break;
            case "Rojo":
                this.GetComponent<MeshRenderer>().material = materials[1];
                break;
            case "Azul":
                this.GetComponent<MeshRenderer>().material = materials[2];
                break;
            case "Amarillo":
                this.GetComponent<MeshRenderer>().material = materials[3];
                break;
            case "Negro":
                this.GetComponent<MeshRenderer>().material = materials[4];
                break;
        }
        //se actualiza el texto a medida que la variable de puntuación cambia
        yourScoreT.text = "Puntos: " + yourScore;
        //si la puntuación es mayor que la puntuación máxima se actualiza la puntuación máxima
       

    }

    //Si chocas con una barrera de distinto color al del coche, se desactiva el character controler, salta un mensaje de que has perdido y la variable booleana hasPerdido se pone en true, si el modo facil esta activado no perderás
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Barrera")
        {
            

            if (other.gameObject.GetComponent<DatosColores>().color != color & jsonEncript.opcionDesarrollador==false)
            {
                Debug.Log("Has perdido");
                hasPerdido = true;
                characterController.enabled = false;
            }


        }
    }
    //Al salir de una barrera del mismo color se cambia el color del coche de forma aleatoria y se aumenta la puntuación
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<DatosColores>().color == color)
        {
            color = colores[Random.Range(0, 5)];
            yourScore++;
        }
    }
    //Corrutina que permite mover el coche y cambiar de carril
    IEnumerator MoveCar(Vector3 targetPosition)
    {
        puedoMoverme = false;
        float timeElapsed = 0;
        Vector3 startPosition = transform.position;
        while (timeElapsed < duration)
        {
            float miX = Mathf.Lerp(startPosition.x, targetPosition.x, timeElapsed / duration);
            transform.position = new Vector3(miX, transform.position.y, transform.position.z);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        puedoMoverme = true;
    }
}
