using UnityEngine;
using UnityEngine.UI; // Necesario para Dropdown
using System.Collections; // Opcional si deseas usar corrutinas en el futuro
using TMPro;

public class LightTypePresetVR : MonoBehaviour
{
    [Header("Referencias")]
    public Light luzSpot; // La luz que se va a controlar
    public TMP_Dropdown dropdownTipoLuz; // UI Dropdown con los tipos de luz

    void Start()
    {
        if (dropdownTipoLuz != null)
        {
            dropdownTipoLuz.onValueChanged.AddListener(ApplyPreset);
        }
        else
        {
            Debug.LogWarning("Dropdown de tipo de luz no asignado.");
        }
    }

    private void ApplyPreset(int index)
    {
        if (luzSpot == null)
        {
            Debug.LogWarning("Luz no asignada.");
            return;
        }

        switch (index)
        {
            case 0: // Directa
                luzSpot.spotAngle = 30f;
                luzSpot.intensity = 12f;
                luzSpot.range = 10f;
                break;

            case 1: // Difusa
                luzSpot.spotAngle = 100f;
                luzSpot.intensity = 4f;
                luzSpot.range = 8f;
                break;

            case 2: // Focalizada
                luzSpot.spotAngle = 15f;
                luzSpot.intensity = 15f;
                luzSpot.range = 6f;
                break;

            default:
                Debug.Log("Tipo de luz no reconocido.");
                break;
        }

        Debug.Log("Preset aplicado: " + dropdownTipoLuz.options[index].text);
    }
}
