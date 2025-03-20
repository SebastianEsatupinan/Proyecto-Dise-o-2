using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
public class Gun : MonoBehaviour
{
    public XRBaseInteractable grabInteract;
    public DisparoRay disparo;
    // Start is called before the first frame update
    void Start()
    {
        grabInteract.activated.AddListener(x => Disparando());
        grabInteract.deactivated.AddListener(x => DejarDiparo());
    }

    public void Disparando()
    {
        disparo.Disparar();
    }

    public void DejarDiparo()
    {
        Debug.Log("Disparandont");
    }

}
