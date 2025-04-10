using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; //para el manejo de guardado local
using UnityEngine.XR.Management;

public class GestorConfiguraciones : MonoBehaviour
{
    public Transform jugador; // El XR

    private List<Light> lucesEnEscena; 
    private List<Transform> objetosInteractuables; //los que tienen el tag de InteractuablesGuardables

    private string rutaGuardado => Path.Combine(Application.persistentDataPath, "config.json");

    void Awake()
    {
        lucesEnEscena = new List<Light>();
        objetosInteractuables = new List<Transform>();

        GameObject[] todos = GameObject.FindGameObjectsWithTag("InteractuablesGuardables");

        // Ordenamos alfabéticamente para garantizar mismo orden entre sesiones
        List<GameObject> ordenados = new List<GameObject>(todos);
        ordenados.Sort((a, b) => a.name.CompareTo(b.name));

        foreach (GameObject go in ordenados)
        {
            Light luz = go.GetComponent<Light>();
            if (luz != null)
            {
                lucesEnEscena.Add(luz);
            }
            else
            {
                objetosInteractuables.Add(go.transform);
            }
        }

        Debug.Log($"🔍 Encontradas {lucesEnEscena.Count} luces y {objetosInteractuables.Count} objetos interactuables.");
    }


    public void GuardarConfiguracion()
    {
        ConfiguracionEscenario config = new ConfiguracionEscenario
        {
            nombreEscenario = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name,
            posicionJugador = jugador.position,
            rotacionJugador = jugador.rotation
        };

        foreach (Light luz in lucesEnEscena)
        {
            config.luces.Add(new DatosLuz
            {
                posicion = luz.transform.position,
                rotacion = luz.transform.rotation,
                intensidad = luz.intensity,
                color = luz.color
            });
        }

        foreach (Transform obj in objetosInteractuables)
        {
            config.objetos.Add(new ObjetoInteractivo
            {
                nombre = obj.name,
                posicion = obj.position,
                rotacion = obj.rotation,
                escala = obj.localScale
            });
        }

        var controles = FindObjectsOfType<LightControlVR>();
        foreach (var control in controles)
        {
            config.valoresLuz.Add(control.ObtenerDatos());
        }

        string json = JsonUtility.ToJson(config, true);
        File.WriteAllText(rutaGuardado, json);

        // Llamada a captura de miniatura
        CapturadorDeMiniatura capturador = FindObjectOfType<CapturadorDeMiniatura>();
        if (capturador != null)
        {
            string nombreArchivo = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name + "_miniatura.png";
            capturador.CapturarImagen(nombreArchivo);
        }
        else
        {
            Debug.LogWarning("⚠️ No se encontró el componente CapturadorDeMiniatura en la escena.");
        }

        string resumenSimple = $"📸 Escenario: {config.nombreEscenario}\n" +
                       $"• Posición jugador: {config.posicionJugador}\n" +
                       $"• Luces: {config.luces.Count}\n" +
                       $"• Objetos: {config.objetos.Count}\n";

        if (config.valoresLuz != null && config.valoresLuz.Count > 0)
        {
            resumenSimple += $"\n💡 Iluminación personalizada:\n";
            int index = 1;
            foreach (var luz in config.valoresLuz)
            {
                resumenSimple += $"🧾 Luz {index++}:\n";
                resumenSimple += $"• Intensidad: {luz.intensidad:F2}\n";
                resumenSimple += $"• Temperatura: {luz.temperatura:F0}K\n";
                resumenSimple += $"• Ángulo: {luz.angulo:F1}°\n";
                resumenSimple += $"• Rotación Y: {luz.rotacionY:F1}°\n";
                resumenSimple += $"• Preset: {luz.tipoPreset}\n";
            }
        }

        string rutaResumen = Path.Combine(Application.persistentDataPath, "resumen.txt");
        File.WriteAllText(rutaResumen, resumenSimple);
    }

    public void CargarConfiguracion()
    {
        if (!File.Exists(rutaGuardado))
        {
            Debug.LogWarning("⚠️ No se encontró archivo de configuración.");
            return;
        }

        string json = File.ReadAllText(rutaGuardado);
        ConfiguracionEscenario config = JsonUtility.FromJson<ConfiguracionEscenario>(json);

        jugador.position = config.posicionJugador;
        jugador.rotation = config.rotacionJugador;

        for (int i = 0; i < config.luces.Count && i < lucesEnEscena.Count; i++)
        {
            lucesEnEscena[i].transform.position = config.luces[i].posicion;
            lucesEnEscena[i].transform.rotation = config.luces[i].rotacion;
            lucesEnEscena[i].intensity = config.luces[i].intensidad;
            lucesEnEscena[i].color = config.luces[i].color;
        }

        for (int i = 0; i < config.objetos.Count && i < objetosInteractuables.Count; i++)
        {
            objetosInteractuables[i].position = config.objetos[i].posicion;
            objetosInteractuables[i].rotation = config.objetos[i].rotacion;
            objetosInteractuables[i].localScale = config.objetos[i].escala;
        }

        StartCoroutine(ReiniciarXR());

        Debug.Log("📂 Configuración cargada exitosamente.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) GuardarConfiguracion();
    }

    IEnumerator ReiniciarXR()
    {
        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();

        yield return null;

        XRGeneralSettings.Instance.Manager.InitializeLoaderSync();
        XRGeneralSettings.Instance.Manager.StartSubsystems();
    }
}
