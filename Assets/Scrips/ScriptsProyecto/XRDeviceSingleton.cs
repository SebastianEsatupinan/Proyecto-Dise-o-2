using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XRDeviceSingleton : MonoBehaviour
{
    private static XRDeviceSingleton instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject); // Destruye el duplicado
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject); // Persiste entre escenas
    }
}
