using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LightSelector : MonoBehaviour
{
    private LightControlVR[] lightControllers;

    void Start()
    {
        lightControllers = FindObjectsOfType<LightControlVR>();

        foreach (var controller in lightControllers)
        {
            controller.HideCanvas();

            XRBaseInteractable interactable = controller.GetComponent<XRBaseInteractable>();
            if (interactable != null)
            {
                interactable.selectEntered.AddListener((args) =>
                {
                    OnLightSelected(controller);
                });
            }
        }
    }

    void OnLightSelected(LightControlVR selected)
    {
        foreach (var controller in lightControllers)
        {
            if (controller == selected)
                controller.ShowCanvas();
            else
                controller.HideCanvas();
        }
    }
}
