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

        // Guardar estado original
        bool estabaActiva = camaraCenital.enabled;
        RenderTexture rtOriginal = camaraCenital.targetTexture;

        // Crear RenderTexture temporal
        RenderTexture rtTemporal = new RenderTexture(resolucion, resolucion, 24);
        camaraCenital.targetTexture = rtTemporal;

        // Activar la cámara solo para renderizar (sin cambiar cámara activa del XR)
        camaraCenital.enabled = true;
        camaraCenital.Render();

        // Capturar la imagen
        RenderTexture.active = rtTemporal;
        Texture2D imagen = new Texture2D(resolucion, resolucion, TextureFormat.RGB24, false);
        imagen.ReadPixels(new Rect(0, 0, resolucion, resolucion), 0, 0);
        imagen.Apply();

        // Restaurar todo
        camaraCenital.targetTexture = rtOriginal;
        camaraCenital.enabled = estabaActiva;
        RenderTexture.active = null;
        rtTemporal.Release();
        Destroy(rtTemporal);

        // Guardar en disco (ajusta ruta si usas Descargas)
        string ruta = Path.Combine(Application.persistentDataPath, nombreArchivo);
        File.WriteAllBytes(ruta, imagen.EncodeToPNG());

        Debug.Log($"📷 Esquema capturado sin interferir. Guardado en: {ruta}");
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
