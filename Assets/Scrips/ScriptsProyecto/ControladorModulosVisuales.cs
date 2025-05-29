using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorModulosVisuales : MonoBehaviour
{
    public List<GameObject> modulos;
    private int indiceActual = 0;

    void Start()
    {
        ActivarModulo(indiceActual);
    }

    public void IrAlSiguiente()
    {
        modulos[indiceActual].SetActive(false);

        // Calcula el siguiente índice en loop
        indiceActual = (indiceActual + 1) % modulos.Count;

        modulos[indiceActual].SetActive(true);
    }

    private void ActivarModulo(int index)
    {
        for (int i = 0; i < modulos.Count; i++)
        {
            modulos[i].SetActive(i == index);
        }
    }
}
