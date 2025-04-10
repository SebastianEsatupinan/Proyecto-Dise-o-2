using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscenarioManager : MonoBehaviour
{
    //gei el que pregunte para que sirve esto
    public string ObtenerNombreEscenario()
    {
        return SceneManager.GetActiveScene().name;
    }
}
