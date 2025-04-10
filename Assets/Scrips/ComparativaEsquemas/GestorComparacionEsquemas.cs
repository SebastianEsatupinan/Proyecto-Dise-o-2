using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorComparacionEsquemas : MonoBehaviour
{
    [Header("Referencias")]
    public CapturadorEsquemas capturador;
    public SuperposicionEsquemasUI superposicionUI;

    [Header("Paneles de vista")]
    public GameObject panelSuperposicion;
    public GameObject panelLadoALado;

    private Texture2D imagenA;
    private Texture2D imagenB;

    public void CapturarA()
    {
        imagenA = capturador.Capturar();
        ActualizarVista();
        Debug.Log("📸 Esquema A capturado.");
    }

    public void CapturarB()
    {
        imagenB = capturador.Capturar();
        ActualizarVista();
        Debug.Log("📸 Esquema B capturado.");
    }

    public void CambiarVistaSuperposicion()
    {
        panelSuperposicion.SetActive(true);
        panelLadoALado.SetActive(false);
        ActualizarVista();
        Debug.Log("🔁 Vista: Superposición activada.");
    }

    public void CambiarVistaLadoALado()
    {
        panelSuperposicion.SetActive(false);
        panelLadoALado.SetActive(true);
        ActualizarVista();
        Debug.Log("🧭 Vista: Lado a lado activada.");
    }

    private void ActualizarVista()
    {
        if (superposicionUI != null && imagenA != null && imagenB != null)
        {
            superposicionUI.SetEsquemas(imagenA, imagenB);
        }
    }
}
