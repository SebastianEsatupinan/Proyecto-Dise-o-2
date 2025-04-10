using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturadorEsquemas : MonoBehaviour
{
    public Camera camaraCenital;
    public RenderTexture renderTexture;

    public Texture2D Capturar()
    {
        // Guardar estado actual
        RenderTexture anterior = RenderTexture.active;
        camaraCenital.targetTexture = renderTexture;

        // Renderiza la cámara en el RenderTexture
        RenderTexture.active = renderTexture;
        camaraCenital.Render();

        // Crea una nueva imagen 2D desde el RenderTexture
        Texture2D imagen = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);
        imagen.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        imagen.Apply();

        // Restaurar estados
        camaraCenital.targetTexture = null;
        RenderTexture.active = anterior;

        return imagen;
    }
}
