using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class ResumenConfiguracion : MonoBehaviour
{
    public TextMeshProUGUI resumenTexto;

    private string ruta => Path.Combine(Application.persistentDataPath, "config.json");

    void Start()
    {
        if (!File.Exists(ruta))
        {
            resumenTexto.text = "❌ No hay configuraciones guardadas aún.";
            return;
        }

        string json = File.ReadAllText(ruta);
        ConfiguracionEscenario config = JsonUtility.FromJson<ConfiguracionEscenario>(json);

        string resumen = $"📸 Resumen de Configuración\n";
        resumen += $"• Escenario: {config.nombreEscenario}\n";
        resumen += $"• Posición del jugador: {config.posicionJugador}\n";
        resumen += $"• Luces guardadas: {config.luces.Count}\n";
        resumen += $"• Objetos interactivos: {config.objetos.Count}\n";

        // 💡 Mostrar resumen de parámetros guardados de luz
        if (config.valoresLuz != null && config.valoresLuz.Count > 0)
        {
            resumen += $"\n💡 Iluminación personalizada (última sesión):";
            int index = 1;
            foreach (var luz in config.valoresLuz)
            {
                resumen += $"\n🧾 Luz {index++}:";
                resumen += $"\n• Intensidad: {luz.intensidad:F2}";
                resumen += $"\n• Temperatura: {luz.temperatura:F0}K";
                resumen += $"\n• Ángulo: {luz.angulo:F1}°";
                resumen += $"\n• Rotación Y: {luz.rotacionY:F1}°";
                resumen += $"\n• Preset: {luz.tipoPreset}";
            }
        }

        resumenTexto.text = resumen;
    }
}
