using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PrenderMaquina : MonoBehaviour
{
    public NeedleMovement leftNeedle; // Referencia a la aguja izquierda
    public NeedleMovement rightNeedle; // Referencia a la aguja derecha

    public void StartMachine()
    {
        if (leftNeedle != null)
        {
            leftNeedle.StartNeedleMovement();
        }
        if (rightNeedle != null)
        {
            rightNeedle.StartNeedleMovement();
        }
        //Debug.Log("La máquina está encendida y las agujas están en movimiento.");
    }

    public void StopMachine()
    {
        if (leftNeedle != null)
        {
            leftNeedle.ResetNeedle();
        }
        if (rightNeedle != null)
        {
            rightNeedle.ResetNeedle();
        }
        //Debug.Log("La máquina está apagada y las agujas han regresado a su posición.");
    }
}
