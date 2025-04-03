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

    void Start()
    {
        // Mostrar el Canvas al iniciar
        ShowCanvas();

        if (sliderIntensidad != null)
            sliderIntensidad.onValueChanged.AddListener(SetIntensity);

        if (sliderColorTemp != null)
            sliderColorTemp.onValueChanged.AddListener(SetColorTemperature);

        if (sliderRotacionY != null)
            sliderRotacionY.onValueChanged.AddListener(SetRotationY);
    }

    public void ShowCanvas()
    {
        if (panelUI != null)
        {
            panelUI.SetActive(true);
            Debug.Log("Canvas mostrado.");
        }
    }

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

    private void SetColorTemperature(float value)
    {
        // Simula temperatura de color modificando el color de la luz (rojo a azul)
        if (luzSpot != null)
        {
            Color color = Color.Lerp(Color.red, Color.blue, value); // 0 = rojo, 1 = azul
            luzSpot.color = color;
        }
    }

    private void SetRotationY(float value)
    {
        // Rota el objeto (luz) en el eje Y
        Vector3 currentRotation = transform.eulerAngles;
        transform.eulerAngles = new Vector3(currentRotation.x, value, currentRotation.z);
    }
}
