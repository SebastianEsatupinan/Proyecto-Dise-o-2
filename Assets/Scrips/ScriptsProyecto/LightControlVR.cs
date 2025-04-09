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
    public Slider sliderAngulo;

    private float targetYRotation;

    void Start()
    {
        ShowCanvas();

        if (sliderIntensidad != null && luzSpot != null)
        {
            sliderIntensidad.minValue = 0;
            sliderIntensidad.maxValue = 20;
            sliderIntensidad.value = luzSpot.intensity;
            sliderIntensidad.onValueChanged.AddListener(SetIntensity);
        }

        if (sliderColorTemp != null)
        {
            sliderColorTemp.minValue = 1000;
            sliderColorTemp.maxValue = 10000;
            sliderColorTemp.value = 6500;
            sliderColorTemp.onValueChanged.AddListener(SetColorTemperature);
        }

        if (sliderRotacionY != null)
        {
            sliderRotacionY.minValue = 0;
            sliderRotacionY.maxValue = 360;
            sliderRotacionY.value = transform.eulerAngles.y;
            sliderRotacionY.onValueChanged.AddListener(SetRotationY);
        }

        if (sliderAngulo != null && luzSpot != null)
        {
            sliderAngulo.minValue = 10;
            sliderAngulo.maxValue = 120;
            sliderAngulo.value = luzSpot.spotAngle;
            sliderAngulo.onValueChanged.AddListener(SetSpotAngle);
        }

        targetYRotation = transform.eulerAngles.y;
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

    private void SetColorTemperature(float kelvin)
    {
        if (luzSpot != null)
            luzSpot.color = KelvinToRGB(kelvin);
    }

    private void SetRotationY(float value)
    {
        Vector3 rot = transform.eulerAngles;
        rot.y = value;
        transform.eulerAngles = rot;
    }

    private void SetSpotAngle(float value)
    {
        if (luzSpot != null)
            luzSpot.spotAngle = value;
    }

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
