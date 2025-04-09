using UnityEngine;

[RequireComponent(typeof(Transform))]
public class LightGrabVR : MonoBehaviour
{
    private Vector3 posicionInicial;
    private Quaternion rotacionInicial;

    void Start()
    {
        posicionInicial = transform.position;
        rotacionInicial = transform.rotation;
    }

    // Si en el futuro necesitas volver a la posición/rotación original:
    public void ResetTransform()
    {
        transform.position = posicionInicial;
        transform.rotation = rotacionInicial;
    }
}
