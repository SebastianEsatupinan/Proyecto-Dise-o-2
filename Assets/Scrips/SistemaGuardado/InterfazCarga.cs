using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class InterfazCarga : MonoBehaviour
{
    public Button botonCargar;
    public TextMeshProUGUI textoResumen;
    public RawImage imagenMiniatura;
    public string nombreEscenaADescargar;

    void Start()
    {
        MostrarResumen();
    }

    public void MostrarResumen()
    {
        string resumenPath = Path.Combine(Application.persistentDataPath, "resumen.txt");
        if (File.Exists(resumenPath))
        {
            textoResumen.text = File.ReadAllText(resumenPath);
        }
        else
        {
            textoResumen.text = "❌ No hay resumen guardado.";
        }

        string imagenPath = Path.Combine(Application.persistentDataPath, nombreEscenaADescargar + "_miniatura.png");
        if (File.Exists(imagenPath))
        {
            byte[] datos = File.ReadAllBytes(imagenPath);
            Texture2D textura = new Texture2D(2, 2);
            textura.LoadImage(datos);
            imagenMiniatura.texture = textura;
        }
    }
}
