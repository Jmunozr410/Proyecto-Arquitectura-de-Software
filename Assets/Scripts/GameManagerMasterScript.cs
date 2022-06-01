using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManagerMasterScript : MonoBehaviour
{
    public GameObject panelRanking;
    public GameObject panelInicio;
    public GameObject panelMuerte;
    ControladorCoche controladorCoche;

    // Start is called before the first frame update
    void Start()
    {
        //singleton
        controladorCoche = ControladorCoche.Instance;
        //Poner la escala del tiempo a cero para poder operar el menú
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //si has perdido se muestra el panel de muerte
        if (controladorCoche.hasPerdido)
        {
            MostrarPanelMuerte();
        }
    }
    //funcion para jugar
    public void Jugar()
    {

        Time.timeScale = 1;
        panelInicio.SetActive(false);
    }
    //funcion para mostrar el ranking
    public void MostrarRanking()
    {
        panelInicio.SetActive(false);
        panelRanking.SetActive(true);

    }
    //funcion para recargar la escena
    public void Reiniciar()
    {

        SceneManager.LoadScene(0);
    }
    //funcion parara mostrar el panel de muerte
    public void MostrarPanelMuerte()
    {
        panelInicio.SetActive(false);
        panelRanking.SetActive(false);
        panelMuerte.SetActive(true);
    }


}
