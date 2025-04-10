using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SuperposicionEsquemasUI : MonoBehaviour
{
    [Header("Imágenes de superposición")]
    public RawImage imagenA;
    public RawImage imagenB;

    [Header("Sliders de opacidad (opcional)")]
    public Slider sliderOpacidadA;
    public Slider sliderOpacidadB;

    [Header("Imágenes para vista lateral (opcional)")]
    public RawImage imagenA_Lateral;
    public RawImage imagenB_Lateral;

    void Start()
    {
        if (sliderOpacidadA != null)
        {
            sliderOpacidadA.onValueChanged.AddListener(valor => {
                Color c = imagenA.color;
                c.a = valor;
                imagenA.color = c;
            });
        }

        if (sliderOpacidadB != null)
        {
            sliderOpacidadB.onValueChanged.AddListener(valor => {
                Color c = imagenB.color;
                c.a = valor;
                imagenB.color = c;
            });
        }
    }

    public void SetEsquemas(Texture2D texA, Texture2D texB)
    {
        if (imagenA != null) imagenA.texture = texA;
        if (imagenB != null) imagenB.texture = texB;

        if (sliderOpacidadA != null) sliderOpacidadA.value = 1f;
        if (sliderOpacidadB != null) sliderOpacidadB.value = 1f;

        if (imagenA_Lateral != null) imagenA_Lateral.texture = texA;
        if (imagenB_Lateral != null) imagenB_Lateral.texture = texB;
    }
}
