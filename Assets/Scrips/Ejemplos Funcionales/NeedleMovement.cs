using System.Collections;
using UnityEngine;

public class NeedleMovement : MonoBehaviour
{
    public Vector3 rotationAxis = Vector3.forward; // Eje de rotación (por ejemplo, Vector3.forward para el eje Z)
    public float minRotation = -45f; // Ángulo mínimo relativo a la rotación actual
    public float maxRotation = 45f; // Ángulo máximo relativo a la rotación actual
    public float speed = 2f; // Velocidad de la animación

    private bool isMoving = false; // Controla si la aguja está en movimiento
    private Quaternion originalRotation; // Rotación inicial del objeto
    private Quaternion minRotationQuaternion; // Rotación mínima como Quaternion
    private Quaternion maxRotationQuaternion; // Rotación máxima como Quaternion

    private void Start()
    {
        // Guarda la rotación inicial del objeto
        originalRotation = transform.localRotation;

        // Calcula las rotaciones mínima y máxima en base a la rotación actual del objeto
        minRotationQuaternion = originalRotation * Quaternion.AngleAxis(minRotation, rotationAxis);
        maxRotationQuaternion = originalRotation * Quaternion.AngleAxis(maxRotation, rotationAxis);

        // Establece la posición inicial de la aguja en el ángulo mínimo relativo a la rotación actual
        transform.localRotation = minRotationQuaternion;
    }

    // Método para iniciar el movimiento de la aguja
    public void StartNeedleMovement()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveNeedle());
        }
    }

    // Método para detener el movimiento y reiniciar la posición de la aguja
    public void ResetNeedle()
    {
        isMoving = false;
        StopAllCoroutines();
        transform.localRotation = minRotationQuaternion; // Regresa la aguja a la posición mínima relativa a la rotación inicial
    }

    private IEnumerator MoveNeedle()
    {
        isMoving = true;
        Quaternion targetRotation = maxRotationQuaternion;

        while (isMoving)
        {
            // Interpola la rotación de la aguja hacia el ángulo objetivo usando Quaternion.Lerp
            transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * speed);

            // Cambia la dirección de la aguja cuando alcanza el ángulo objetivo
            if (Quaternion.Angle(transform.localRotation, targetRotation) < 0.1f)
            {
                targetRotation = (targetRotation == maxRotationQuaternion) ? minRotationQuaternion : maxRotationQuaternion;
            }

            yield return null;
        }
    }
}
