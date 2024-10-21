using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarratorController : MonoBehaviour
{
    private static AudioSource source;
    private static NarratorController Instance;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        if (Instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            Instance = this;
        }
        else Destroy(this.gameObject);
    }

    public static bool DisplayAudio(AudioClip clip, bool isPriority)
    {
        if (source.isPlaying && !isPriority) //Si el nuevo audio no es prioritario y hay otro ejecutándose no lo ejecuta
        {
            return false;
        }
        else
        {
            source.clip = clip;
            source.Play();
            return true;
        }
    }
}
