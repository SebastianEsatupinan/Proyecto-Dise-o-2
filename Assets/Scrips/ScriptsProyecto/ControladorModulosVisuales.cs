using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorModulosVisuales : MonoBehaviour
{
    public List<GameObject> modulos;
    private int indiceActual = 0;
    public GameObject mensajeFinal;

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

        // Mostrar mensaje final solo si estamos en el último módulo (antes de reiniciar)
        if (indiceActual == 0 && mensajeFinal != null)
        {
            mensajeFinal.SetActive(true); // 🔥 Muestra el mensaje al terminar ciclo completo
        }
        else if (mensajeFinal != null)
        {
            mensajeFinal.SetActive(false); // Oculta si está viendo cualquier otro módulo
        }
    }

    private void ActivarModulo(int index)
    {
        for (int i = 0; i < modulos.Count; i++)
        {
            modulos[i].SetActive(i == index);
        }
    }
}
