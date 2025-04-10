using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControladorCondicionesEscenario : MonoBehaviour
{
    [System.Serializable]
    public class SkyboxOpcion
    {
        public string nombre;
        public Material skyboxMaterial;
    }

    public List<SkyboxOpcion> skyboxes;
    public Slider intensidadSlider;

    private int indiceActual = 0;

    void Start()
    {
        if (skyboxes.Count > 0)
        {
            CambiarSkybox(0);
        }

        if (intensidadSlider != null)
        {
            intensidadSlider.minValue = 0.1f;
            intensidadSlider.maxValue = 2f;
            intensidadSlider.value = RenderSettings.ambientIntensity;

            intensidadSlider.onValueChanged.AddListener(valor =>
            {
                RenderSettings.ambientIntensity = valor;
                DynamicGI.UpdateEnvironment();
            });
        }
    }

    public void CambiarSkybox(int index)
    {
        if (index < 0 || index >= skyboxes.Count) return;

        RenderSettings.skybox = skyboxes[index].skyboxMaterial;
        DynamicGI.UpdateEnvironment();

        indiceActual = index;
    }
}
