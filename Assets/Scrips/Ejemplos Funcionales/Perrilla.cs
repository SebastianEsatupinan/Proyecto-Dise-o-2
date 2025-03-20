using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Perrilla : MonoBehaviour
{
    public Transform rotationPivot; // Punto de rotación de la perilla
    public float minAngle = 0f; // Ángulo mínimo de rotación en el eje Z
    public float maxAngle = 180f; // Ángulo máximo de rotación en el eje Z
    public bool allowClockwise = true; // Determina si el giro es en sentido horario

    private XRBaseInteractor interactor; // Interactor que agarra la perilla
    private bool isRotating = false; // Estado de rotación
    private float initialInteractorAngle; // Ángulo inicial del interactor
    private float initialKnobAngle; // Ángulo inicial de la perilla
    private Vector3 initialPosition; // Posición fija del pivote

    void Start()
    {
        var grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
            grabInteractable.selectExited.AddListener(OnReleased);
        }
        initialPosition = rotationPivot.localPosition;
    }

    public void OnGrabbed(SelectEnterEventArgs args)
    {
        interactor = args.interactorObject as XRBaseInteractor;
        isRotating = true;
        initialInteractorAngle = CalculateAngle(interactor.transform.position);
        initialKnobAngle = rotationPivot.localEulerAngles.z;
    }

    public void OnReleased(SelectExitEventArgs args)
    {
        isRotating = false;
        interactor = null;
    }

    void Update()
    {
        if (isRotating && interactor != null)
        {
            RotateKnob();
        }
        rotationPivot.localPosition = initialPosition;
    }

    private void RotateKnob()
    {
        float currentInteractorAngle = CalculateAngle(interactor.transform.position);
        float angleDelta = currentInteractorAngle - initialInteractorAngle;

        // Ajustar el ángulo permitido según la dirección permitida
        if (!allowClockwise && angleDelta > 0)
            angleDelta = 0; // Bloquea giro horario
        if (allowClockwise && angleDelta < 0)
            angleDelta = 0; // Bloquea giro antihorario

        float newAngle = Mathf.Clamp(initialKnobAngle + angleDelta, minAngle, maxAngle);

        // Aplica solo la rotación en Z y bloquea X e Y
        rotationPivot.localRotation = Quaternion.Euler(0f, 0f, newAngle);
    }

    private float CalculateAngle(Vector3 position)
    {
        Vector3 direction = position - rotationPivot.position;
        return Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
    }
}
