using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CapturadorDeMiniatura : MonoBehaviour
{
    public Camera camaraCenital;
    public int resolucion = 512;

    public void CapturarImagen(string nombreArchivo)
    {
        if (camaraCenital == null)
        {
            Debug.LogWarning("⚠️ No se asignó la cámara cenital.");
            return;
        }

        RenderTexture rt = new RenderTexture(resolucion, resolucion, 24);
        camaraCenital.targetTexture = rt;

        Texture2D imagen = new Texture2D(resolucion, resolucion, TextureFormat.RGB24, false);
        camaraCenital.Render();
        RenderTexture.active = rt;
        imagen.ReadPixels(new Rect(0, 0, resolucion, resolucion), 0, 0);
        imagen.Apply();

        camaraCenital.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        byte[] bytes = imagen.EncodeToPNG();
        string ruta = Path.Combine(Application.persistentDataPath, nombreArchivo);
        File.WriteAllBytes(ruta, bytes);

        Debug.Log("📸 Miniatura guardada en: " + ruta);
        // 🔁 Restaurar cámara original
        camaraCenital.gameObject.SetActive(false);
    }
}
