using UnityEngine;
using UnityEngine.UI;

public class LightControlVR : MonoBehaviour
{
    private float intensidadActual;
    private float temperaturaActual;
    private float rotacionYActual;
    private float anguloActual;

    private string ultimoSliderModificado = "";

    [Header("Referencias")]
    public Light luzSpot;
    public GameObject panelUI;

    [Header("Sliders")]
    public Slider sliderIntensidad;
    public Slider sliderColorTemp;
    public Slider sliderRotacionY;
    public Slider sliderAngulo;

    [Header("Audio general")]
    public AudioSource audioSource;

    [Header("Audios de Intensidad")]
    public AudioClip[] audiosIntensidad;
    /*
        [0] Intensidad baja
        [1] Intensidad tenue (atmosférica)
        [2] Intensidad alta
        [3] Intensidad equilibrada
        [4] Intensidad dramática
        [5] Intensidad en rostro (demasiado alta)
    */

    [Header("Audios de Temperatura")]
    public AudioClip[] audiosTemperatura;
    /*
        [0] Muy cálida
        [1] Neutra
        [2] Fría
        [3] Emocional cálida
        [4] Técnica clínica
        [5] Impacto emocional
    */

    [Header("Audios de Rotación")]
    public AudioClip[] audiosRotacion;
    /*
        [0] Frontal
        [1] Lateral 45°
        [2] Diagonal ideal
        [3] Trasera tipo halo
        [4] Lateral para textura
        [5] Mala colocación (superior/frontal fuerte)
    */

    [Header("Audios de Ángulo")]
    public AudioClip[] audiosAngulo;
    /*
        [0] Ángulo cerrado
        [1] Ángulo medio
        [2] Ángulo abierto
        [3] Para enfoque
        [4] Ambiental
        [5] Teatral
    */

    private float targetYRotation;

    void Start()
    {
        ShowCanvas();

        if (sliderIntensidad != null && luzSpot != null)
        {
            sliderIntensidad.minValue = 0;
            sliderIntensidad.maxValue = 20;
            sliderIntensidad.value = luzSpot.intensity;
            sliderIntensidad.onValueChanged.AddListener((v) => { SetIntensity(v); ultimoSliderModificado = "intensidad"; });
        }

        if (sliderColorTemp != null)
        {
            sliderColorTemp.minValue = 1000;
            sliderColorTemp.maxValue = 10000;
            sliderColorTemp.value = 6500;
            sliderColorTemp.onValueChanged.AddListener((v) => { SetColorTemperature(v); ultimoSliderModificado = "temperatura"; });
        }

        if (sliderRotacionY != null)
        {
            sliderRotacionY.minValue = 0;
            sliderRotacionY.maxValue = 360;
            sliderRotacionY.value = transform.eulerAngles.y;
            sliderRotacionY.onValueChanged.AddListener((v) => { SetRotationY(v); ultimoSliderModificado = "rotacion"; });
        }

        if (sliderAngulo != null && luzSpot != null)
        {
            sliderAngulo.minValue = 10;
            sliderAngulo.maxValue = 120;
            sliderAngulo.value = luzSpot.spotAngle;
            sliderAngulo.onValueChanged.AddListener((v) => { SetSpotAngle(v); ultimoSliderModificado = "angulo"; });
        }

        targetYRotation = transform.eulerAngles.y;
    }

    public void ShowCanvas()
    {
        if (panelUI != null)
        {
            panelUI.SetActive(true);
        }
    }

    public void HideCanvas()
    {
        if (panelUI != null)
        {
            panelUI.SetActive(false);
        }
    }

    private void SetIntensity(float value)
    {
        intensidadActual = value;
        if (luzSpot != null)
            luzSpot.intensity = value;
    }

    private void SetColorTemperature(float kelvin)
    {
        temperaturaActual = kelvin;
        if (luzSpot != null)
            luzSpot.color = KelvinToRGB(kelvin);
    }

    private void SetRotationY(float value)
    {
        rotacionYActual = value;
        Vector3 rot = transform.eulerAngles;
        rot.y = value;
        transform.eulerAngles = rot;
    }

    private void SetSpotAngle(float value)
    {
        anguloActual = value;
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

    public DatosLuzExtra ObtenerDatos()
    {
        return new DatosLuzExtra
        {
            intensidad = intensidadActual,
            temperatura = temperaturaActual,
            rotacionY = rotacionYActual,
            angulo = anguloActual,
            tipoPreset = "N/A"
        };
    }

    public void DarSugerencia()
    {
        if (audioSource == null) return;

        switch (ultimoSliderModificado)
        {
            case "intensidad":
                if (intensidadActual < 1f)
                    audioSource.PlayOneShot(audiosIntensidad[0]);
                else if (intensidadActual < 3f)
                    audioSource.PlayOneShot(audiosIntensidad[1]);
                else if (intensidadActual > 15f)
                    audioSource.PlayOneShot(audiosIntensidad[2]);
                else if (intensidadActual >= 6f && intensidadActual <= 10f)
                    audioSource.PlayOneShot(audiosIntensidad[3]);
                else if (intensidadActual > 10f)
                    audioSource.PlayOneShot(audiosIntensidad[4]);
                else
                    audioSource.PlayOneShot(audiosIntensidad[5]);
                break;

            case "temperatura":
                if (temperaturaActual < 3000f)
                    audioSource.PlayOneShot(audiosTemperatura[0]);
                else if (temperaturaActual >= 3000f && temperaturaActual <= 5000f)
                    audioSource.PlayOneShot(audiosTemperatura[1]);
                else if (temperaturaActual > 7000f)
                    audioSource.PlayOneShot(audiosTemperatura[2]);
                else if (temperaturaActual < 4500f)
                    audioSource.PlayOneShot(audiosTemperatura[3]);
                else if (temperaturaActual > 8500f)
                    audioSource.PlayOneShot(audiosTemperatura[4]);
                else
                    audioSource.PlayOneShot(audiosTemperatura[5]);
                break;

            case "rotacion":
                if (rotacionYActual >= 0 && rotacionYActual <= 20)
                    audioSource.PlayOneShot(audiosRotacion[0]);
                else if (rotacionYActual >= 30 && rotacionYActual <= 60)
                    audioSource.PlayOneShot(audiosRotacion[1]);
                else if (rotacionYActual > 60 && rotacionYActual <= 100)
                    audioSource.PlayOneShot(audiosRotacion[2]);
                else if (rotacionYActual >= 170 && rotacionYActual <= 190)
                    audioSource.PlayOneShot(audiosRotacion[3]);
                else if (rotacionYActual >= 120 && rotacionYActual <= 150)
                    audioSource.PlayOneShot(audiosRotacion[4]);
                else
                    audioSource.PlayOneShot(audiosRotacion[5]);
                break;

            case "angulo":
                if (anguloActual < 30f)
                    audioSource.PlayOneShot(audiosAngulo[0]);
                else if (anguloActual >= 30f && anguloActual <= 60f)
                    audioSource.PlayOneShot(audiosAngulo[1]);
                else if (anguloActual > 90f)
                    audioSource.PlayOneShot(audiosAngulo[2]);
                else if (anguloActual <= 40f)
                    audioSource.PlayOneShot(audiosAngulo[3]);
                else if (anguloActual >= 100f)
                    audioSource.PlayOneShot(audiosAngulo[4]);
                else
                    audioSource.PlayOneShot(audiosAngulo[5]);
                break;
        }
    }
}
