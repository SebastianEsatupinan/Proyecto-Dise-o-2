using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using UnityEngine;

public class ExportadorDeEscena : MonoBehaviour
{
    [Header("Configuración")]
    public Camera camaraCenital;
    public int resolucion = 1024;
    public List<LightControlVR> lucesAExportar;

    private string carpetaDescargas => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
    private string carpetaTemporal => Path.Combine(Application.temporaryCachePath, "ExportacionIluminacion");

    public void ExportarSimulacion()
    {
        Directory.CreateDirectory(carpetaTemporal); // Limpia temporal

        string rutaImagen = Path.Combine(carpetaTemporal, "captura_iluminacion.png");
        string rutaJSON = Path.Combine(carpetaTemporal, "datos_luces.json");
        string nombreZIP = $"Simulacion_{DateTime.Now:yyyyMMdd_HHmmss}.zip";
        string rutaZIP = Path.Combine(carpetaDescargas, nombreZIP);

        CapturarImagen(rutaImagen);
        ExportarDatosLuces(rutaJSON);
        ComprimirArchivos(rutaZIP, rutaImagen, rutaJSON);

        Debug.Log($"📦 Exportación completada. ZIP guardado en:\n{rutaZIP}");
        Application.OpenURL("file://" + carpetaDescargas); // Abrir carpeta al finalizar
    }

    void CapturarImagen(string ruta)
    {
        if (camaraCenital == null)
        {
            Debug.LogWarning("⚠️ Cámara cenital no asignada.");
            return;
        }

        RenderTexture rt = new RenderTexture(resolucion, resolucion, 24);
        camaraCenital.targetTexture = rt;
        camaraCenital.Render();

        RenderTexture.active = rt;
        Texture2D imagen = new Texture2D(resolucion, resolucion, TextureFormat.RGB24, false);
        imagen.ReadPixels(new Rect(0, 0, resolucion, resolucion), 0, 0);
        imagen.Apply();

        camaraCenital.targetTexture = null;
        RenderTexture.active = null;
        Destroy(rt);

        byte[] bytes = imagen.EncodeToPNG();
        File.WriteAllBytes(ruta, bytes);
    }

    void ExportarDatosLuces(string ruta)
    {
        List<DatosLuzExportable> datos = new List<DatosLuzExportable>();

        for (int i = 0; i < lucesAExportar.Count; i++)
        {
            var luz = lucesAExportar[i];
            if (luz == null) continue;

            DatosLuzExtra extra = luz.ObtenerDatos();

            datos.Add(new DatosLuzExportable
            {
                nombre = luz.name,
                posicion = luz.transform.position,
                intensidad = extra.intensidad,
                temperatura = extra.temperatura,
                rotacionY = extra.rotacionY,
                angulo = extra.angulo,
                tipoPreset = extra.tipoPreset
            });
        }

        string json = JsonUtility.ToJson(new Wrapper<DatosLuzExportable> { items = datos }, true);
        File.WriteAllText(ruta, json);
    }

    void ComprimirArchivos(string salidaZIP, string rutaImagen, string rutaJSON)
    {
        if (File.Exists(salidaZIP)) File.Delete(salidaZIP);

        using (FileStream zipToOpen = new FileStream(salidaZIP, FileMode.Create))
        {
            using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Create))
            {
                archive.CreateEntryFromFile(rutaImagen, "captura_iluminacion.png");
                archive.CreateEntryFromFile(rutaJSON, "datos_luces.json");
            }
        }
    }

    [Serializable]
    public class Wrapper<T>
    {
        public List<T> items;
    }

    [Serializable]
    public class DatosLuzExportable
    {
        public string nombre;
        public Vector3 posicion;
        public float intensidad;
        public float temperatura;
        public float rotacionY;
        public float angulo;
        public string tipoPreset;
    }
}
