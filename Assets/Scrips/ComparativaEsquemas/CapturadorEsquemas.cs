using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class CapturadorEsquemas : MonoBehaviour
{
    public Camera camaraCenital;
    public int resolucion = 512;

    public void CapturarEsquema(string nombreArchivo)
    {
        if (camaraCenital == null)
        {
            Debug.LogWarning("⚠️ Cámara cenital no asignada.");
            return;
        }

        // Creamos un RenderTexture temporal que no interfiere con la cámara en escena
        RenderTexture rtOriginal = camaraCenital.targetTexture;

        RenderTexture rtTemporal = new RenderTexture(resolucion, resolucion, 24);
        camaraCenital.targetTexture = rtTemporal;

        // Forzamos render sin cambiar cámara activa
        camaraCenital.Render();

        RenderTexture.active = rtTemporal;
        Texture2D imagen = new Texture2D(resolucion, resolucion, TextureFormat.RGB24, false);
        imagen.ReadPixels(new Rect(0, 0, resolucion, resolucion), 0, 0);
        imagen.Apply();

        // Restauramos todo
        camaraCenital.targetTexture = rtOriginal;
        RenderTexture.active = null;
        rtTemporal.Release();
        Destroy(rtTemporal);

        byte[] bytes = imagen.EncodeToPNG();
        string ruta = Path.Combine(Application.persistentDataPath, nombreArchivo);
        File.WriteAllBytes(ruta, bytes);

        Debug.Log($"📷 Esquema capturado sin cambiar cámara. Guardado en: {ruta}");
    }

    public Texture2D CargarEsquema(string nombreArchivo)
    {
        string ruta = Path.Combine(Application.persistentDataPath, nombreArchivo);
        if (!File.Exists(ruta))
        {
            Debug.LogWarning("❌ No se encontró la imagen del esquema.");
            return null;
        }

        byte[] bytes = File.ReadAllBytes(ruta);
        Texture2D textura = new Texture2D(2, 2);
        textura.LoadImage(bytes);
        return textura;
    }
}
