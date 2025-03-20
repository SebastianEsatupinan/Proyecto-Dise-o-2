using UnityEngine;

public class MachineController : MonoBehaviour
{
    public AudioSource machineAudioSource; // AudioSource que representa el sonido continuo de la máquina

    private bool isMachineOn = false; // Estado de la máquina

    /// <summary>
    /// Propiedad para obtener el estado actual del panel.
    /// </summary>
    public bool EstadoPanel => isMachineOn;

    /// <summary>
    /// Rutina pública para encender la máquina.
    /// </summary>
    public void EncenderMaquina()
    {
        if (!isMachineOn)
        {
            isMachineOn = true;

            // Activa el AudioSource si está asignado
            if (machineAudioSource != null)
            {
                machineAudioSource.enabled = true;
                if (!machineAudioSource.isPlaying)
                {
                    machineAudioSource.Play(); // Asegura que el audio comience a reproducirse
                }
            }
            else
            {
                Debug.LogWarning("No se asignó un AudioSource para el sonido de la máquina.");
            }

            Debug.Log("La máquina se ha encendido.");
        }
        else
        {
            Debug.Log("La máquina ya está encendida.");
        }
    }

    /// <summary>
    /// Rutina pública para apagar la máquina.
    /// </summary>
    public void ApagarMaquina()
    {
        if (isMachineOn)
        {
            isMachineOn = false;

            // Desactiva el AudioSource si está asignado
            if (machineAudioSource != null)
            {
                machineAudioSource.Stop(); // Detiene el audio
                machineAudioSource.enabled = false; // Desactiva el AudioSource
            }
            else
            {
                Debug.LogWarning("No se asignó un AudioSource para el sonido de la máquina.");
            }

            Debug.Log("La máquina se ha apagado.");
        }
        else
        {
            Debug.Log("La máquina ya está apagada.");
        }
    }
}
