using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MiniaturaConfiguracionUI : MonoBehaviour
{
    public RawImage miniatura;

    private string rutaMiniatura => Path.Combine(Application.persistentDataPath, "miniatura.png");

    void Start()
    {
        if (!File.Exists(rutaMiniatura))
        {
            Debug.LogWarning("⚠️ No se encontró miniatura.");
            return;
        }

        byte[] imagenBytes = File.ReadAllBytes(rutaMiniatura);
        Texture2D textura = new Texture2D(2, 2);
        textura.LoadImage(imagenBytes);
        miniatura.texture = textura;
    }
}
