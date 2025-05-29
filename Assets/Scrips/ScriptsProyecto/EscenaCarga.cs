using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenaCarga : MonoBehaviour
{
    public string escenaDestino;

    [Tooltip("Segundos a esperar antes de cargar la escena destino")]
    public float retardo = 1.5f;

    void Start()
    {
        StartCoroutine(CargarEscenaFinal());
    }

    IEnumerator CargarEscenaFinal()
    {
        yield return new WaitForSeconds(retardo);
        SceneManager.LoadScene(escenaDestino);
    }
}
