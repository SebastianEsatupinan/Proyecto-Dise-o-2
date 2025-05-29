using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MostrarCanvasConRotacion : MonoBehaviour
{

    public GameObject canvas;
    public Transform mano; // Left Controller

    // Rango de activación por eje
    public float minX = -115f;
    public float maxX = -70f;

    public float minY = 55f;
    public float maxY = 105f;


    void Update()
    {
        Vector3 rot = mano.rotation.eulerAngles;

        // Corregir el valor de X para que sea de -180 a 180
        float x = rot.x > 180f ? rot.x - 360f : rot.x;

        bool dentroRangoX = x >= minX && x <= maxX;
        bool dentroRangoY = rot.y >= minY && rot.y <= maxY;

        canvas.SetActive(dentroRangoX && dentroRangoY);
    }
}
