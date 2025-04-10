using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDControladorSugerencia : MonoBehaviour
{
    public TextMeshProUGUI textoSugerencias;
    public void MostrarSugerencias(string[] sugerencias) {
        textoSugerencias.text = "💡 Sugerencias:\n- " + string.Join("\n- ", sugerencias);
    }
}
