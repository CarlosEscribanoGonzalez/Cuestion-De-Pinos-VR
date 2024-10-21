using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnDoorOpen : MonoBehaviour
{
    [SerializeField] private AudioClip narratorClip;
    public void DoorOpened()
    {
        GetComponent<AudioSource>().Play();
        NarratorController.DisplayAudio(narratorClip, true);
        GameObject.FindObjectOfType<SceneChanger>().ForceScene("Credits", 0.25f);
    }
}
