using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq; //reemplaza el no tener que usar un for

[System.Serializable]
public class SugerenciasPorEscenario
{
    public string nombreEscenario; // aqui van los escenarios que vamos a poner
    public string[] sugerencias; // y los comentarios de que ajustar en cada una
}

public class SugerenciaManager : MonoBehaviour
{
    public List<SugerenciasPorEscenario> baseDeSugerencias;
    public HUDControladorSugerencia hud;
    private string escenaActual;

    void Start()
    {
       string escenaActual = SceneManager.GetActiveScene().name;
        MostrarSugerencias(escenaActual);
    }

    void MostrarSugerencias(string nombreEscena)
    {
    Debug.Log("Buscando sugerencias para escena: " + nombreEscena);

    var sugerencia = baseDeSugerencias.FirstOrDefault(s => s.nombreEscenario == nombreEscena);

    if (sugerencia != null)
    {
        Debug.Log("¡Sugerencias encontradas para: " + sugerencia.nombreEscenario + "!");
        hud.MostrarSugerencias(sugerencia.sugerencias);
    }
    else
    {
        Debug.LogWarning("No se encontraron sugerencias para la escena: " + nombreEscena);
    }
    }
}
