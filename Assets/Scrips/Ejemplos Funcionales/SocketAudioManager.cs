using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketAudioManager : MonoBehaviour
{
    // Referencia al componente AudioSource
    private AudioSource audioSource;

    void Start()
    {
        // Obtener el componente AudioSource como hijo del SocketInteractor
        audioSource = GetComponentInChildren<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("No se encontró un AudioSource como hijo del SocketInteractor.");
        }
    }

    // Función para reproducir el audio cuando el evento Select Entered se activa
    public void PlayAudio()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    // Función opcional para detener el audio en Select Exited
    public void StopAudio()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}
