using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioEscena : MonoBehaviour
{
    // Nombre de la escena a la que quieres cambiar
    public string sceneToLoad;

    // Este método se llama cuando otro objeto entra en el colisionador que es trigger
    public void Cambio()
    {
        // Cambia a la escena especificada
        SceneManager.LoadScene(sceneToLoad);
    }
}
