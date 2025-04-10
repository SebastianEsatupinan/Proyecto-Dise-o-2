using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


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
        capturador.CapturarEsquema("esquemaA.png");

        Texture2D texA = capturador.CargarEsquema("esquemaA.png");
        if (texA != null)
        {
            Texture2D texB = capturador.CargarEsquema("esquemaB.png"); // intenta cargar B si ya existe
            superposicionUI.SetEsquemas(texA, texB);
        }

        Debug.Log("📸 Esquema A capturado y actualizado en la vista.");
    }

    public void CapturarB()
    {
        capturador.CapturarEsquema("esquemaB.png");

        Texture2D texB = capturador.CargarEsquema("esquemaB.png");
        if (texB != null)
        {
            Texture2D texA = capturador.CargarEsquema("esquemaA.png"); // intenta cargar A si ya existe
            superposicionUI.SetEsquemas(texA, texB);
        }

        Debug.Log("📸 Esquema B capturado y actualizado en la vista.");
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
    public void VerComparacion()
    {
        Texture2D texA = capturador.CargarEsquema("esquemaA.png");
        Texture2D texB = capturador.CargarEsquema("esquemaB.png");

        if (texA == null || texB == null)
        {
            Debug.LogWarning("⚠️ No se encontraron ambos esquemas para comparar.");
            return;
        }

        superposicionUI.SetEsquemas(texA, texB);

        // Activamos vista por defecto: superposición
        CambiarVistaSuperposicion();
    }

    public void CargarEsquemasDesdeArchivos()
    {
        Texture2D texA = capturador.CargarEsquema("esquemaA.png");
        Texture2D texB = capturador.CargarEsquema("esquemaB.png");

        if (texA != null && texB != null)
            superposicionUI.SetEsquemas(texA, texB);
    }
}
