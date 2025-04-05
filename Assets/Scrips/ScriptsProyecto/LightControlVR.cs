using UnityEngine;
using UnityEngine.UI;

public class LightControlVR : MonoBehaviour
{
    [Header("Referencias")]
    public Light luzSpot;
    public GameObject panelUI;

    [Header("Sliders")]
    public Slider sliderIntensidad;
    public Slider sliderColorTemp;
    public Slider sliderRotacionY;
    public Slider sliderAngulo;  // Nuevo slider para �ngulo de luz

    private float targetYRotation;
    private float smoothSpeed = 3f;
    [Header("Rotaci�n con pivote externo")]
    public Transform pivote; // pivote alrededor del cual rotar� la luz
    public float distanciaAlPivote = 2f; // distancia desde el pivote



    void Start()
    {
        // Mostramos el panel al iniciar (puedes comentar esto si prefieres iniciar oculto)
        ShowCanvas();

        // Configurar Slider de Intensidad
        if (sliderIntensidad != null && luzSpot != null)
        {
            sliderIntensidad.minValue = 0;
            sliderIntensidad.maxValue = 20;
            sliderIntensidad.value = luzSpot.intensity;
            sliderIntensidad.onValueChanged.AddListener(SetIntensity);
        }

        // Configurar Slider de Temperatura
        if (sliderColorTemp != null)
        {
            sliderColorTemp.minValue = 1000;
            sliderColorTemp.maxValue = 10000;
            sliderColorTemp.value = 6500;
            sliderColorTemp.onValueChanged.AddListener(SetColorTemperature);
        }

        // Configurar Slider de Rotaci�n
        if (sliderRotacionY != null)
        {
            sliderRotacionY.minValue = 0;
            sliderRotacionY.maxValue = 360;
            sliderRotacionY.value = transform.eulerAngles.y;
            sliderRotacionY.onValueChanged.AddListener(SetRotationY);
        }

        // Configurar Slider de �ngulo
        if (sliderAngulo != null && luzSpot != null)
        {
            sliderAngulo.minValue = 10;
            sliderAngulo.maxValue = 120;
            sliderAngulo.value = luzSpot.spotAngle;
            sliderAngulo.onValueChanged.AddListener(SetSpotAngle);
        }

        // Guardamos rotaci�n inicial
        targetYRotation = transform.eulerAngles.y;
    }

    void Update()
    {
        if (pivote == null)
            return;

        // Calcular rotaci�n suavizada en Y
        float currentY = transform.eulerAngles.y;
        float newY = Mathf.LerpAngle(currentY, targetYRotation, Time.deltaTime * smoothSpeed);

        // Calcular la nueva posici�n alrededor del pivote
        Quaternion rot = Quaternion.Euler(0f, newY, 0f);
        Vector3 offset = rot * Vector3.forward * distanciaAlPivote;
        transform.position = pivote.position + offset;

        // Hacer que siempre mire hacia el pivote
        transform.LookAt(pivote.position);
    }


    /// <summary>
    /// Muestra el panel de UI.
    /// </summary>
    public void ShowCanvas()
    {
        if (panelUI != null)
        {
            panelUI.SetActive(true);
            Debug.Log("Canvas mostrado.");
        }
    }

    /// <summary>
    /// Oculta el panel de UI.
    /// </summary>
    public void HideCanvas()
    {
        if (panelUI != null)
        {
            panelUI.SetActive(false);
            Debug.Log("Canvas ocultado.");
        }
    }

    private void SetIntensity(float value)
    {
        if (luzSpot != null)
            luzSpot.intensity = value;
    }

    private void SetColorTemperature(float kelvin)
    {
        if (luzSpot != null)
            luzSpot.color = KelvinToRGB(kelvin);
    }

    private void SetRotationY(float value)
    {
        targetYRotation = value;
    }

    private void SetSpotAngle(float value)
    {
        if (luzSpot != null)
            luzSpot.spotAngle = value;
    }

    /// <summary>
    /// Convierte un valor Kelvin en un color RGB aproximado.
    /// </summary>
    private Color KelvinToRGB(float kelvin)
    {
        float temp = kelvin / 100f;
        float r, g, b;

        if (temp <= 66)
        {
            r = 255;
            g = Mathf.Clamp(99.47f * Mathf.Log(temp) - 161.12f, 0, 255);
            b = temp <= 19 ? 0 : Mathf.Clamp(138.52f * Mathf.Log(temp - 10) - 305.04f, 0, 255);
        }
        else
        {
            r = Mathf.Clamp(329.7f * Mathf.Pow(temp - 60, -0.133f), 0, 255);
            g = Mathf.Clamp(288.1f * Mathf.Pow(temp - 60, -0.0755f), 0, 255);
            b = 255;
        }

        return new Color(r / 255f, g / 255f, b / 255f);
    }
}
