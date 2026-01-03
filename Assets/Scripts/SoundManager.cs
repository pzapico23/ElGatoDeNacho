using System.Collections.Generic;
using UnityEngine;


//Clase para almacenar las entradas de audio con su clave correspondiente
//porque el diccionario tal cual no me deja modificarlo en el inspector
[System.Serializable]
public class AudioEntry
{
    public string clave;
    public AudioClip audio;
}

public class SoundManager : MonoBehaviour
{

    [Header("Audios reproducibles")]
    [SerializeField] private AudioEntry[] audioEntries; //Esto es lo que voy a poder toquetear

    private Dictionary<string, AudioClip> audios; //esto es lo que va a usar el código

    private AudioSource controlAudio;

    ///////////////////////////////////////////////////////////
    // Al iniciar convierte el array de entradas en un diccionario para acceso rápido
    ///////////////////////////////////////////////////////////
    private void Awake()
    {
        //Inicializo el diccionario y lo lleno con las entradas del array
        audios = new Dictionary<string, AudioClip>();
        foreach (var entry in audioEntries)
        {
            audios[entry.clave] = entry.audio;
        }
        
        controlAudio = GetComponent<AudioSource>();
    }


    //////////////////////////////////////////////////////////
    // Reproducción de sonidos
    // Entrada: clave del audio a reproducir, volumen base, variabilidad de volumen y variabilidad de pitch
    ///////////////////////////////////////////////////////////
    public void PlaySound(string clave, float volume = 1f, float randVol = 0f, float randPitch = 0f)
    {
        //Si no encuentro el audio, aviso y salgo
        //si sí que encuentro lo reproduzco con las variaciones indicadas
        if (!audios.TryGetValue(clave, out AudioClip clip))
        {
            Debug.LogWarning($"Audio no encontrado: {clave}");
            return;
        }

        //Quiero poder añadir variabilidad para que el sonido no se haga repetitivo
        //que os aseguro que taladra mucho la cabeza si no es algo variable
        float finalVolume = volume + Random.Range(-randVol, randVol);
        float finalPitch = 1f + Random.Range(-randPitch, randPitch);

        controlAudio.pitch = finalPitch;
        controlAudio.PlayOneShot(clip, finalVolume);
    }
}
