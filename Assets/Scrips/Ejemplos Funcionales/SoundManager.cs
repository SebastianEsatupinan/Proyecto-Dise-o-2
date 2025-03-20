using System.Collections;
using UnityEngine;

public class SoundActivator : MonoBehaviour
{
    // Referencia al AudioSource que se activará
    public AudioSource audioSource;
    // Etiqueta a detectar
    public string targetTag = "hands"; // Configurable desde el Inspector
    private bool canPlaySound = true; // Controla si el sonido puede reproducirse

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra tiene la etiqueta correcta
        if (other.CompareTag(targetTag) && canPlaySound)
        {
            PlaySound();
            canPlaySound = false; // Deshabilita la reproducción hasta que salga
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verifica si el objeto que sale tiene la etiqueta correcta
        if (other.CompareTag(targetTag))
        {
            canPlaySound = true; // Habilita la reproducción nuevamente
        }
    }

    private void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No se ha asignado ningún AudioSource.");
        }
    }
}
