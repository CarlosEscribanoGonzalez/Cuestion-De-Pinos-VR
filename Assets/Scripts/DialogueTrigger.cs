using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private bool isPriority = true;

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NarratorController.DisplayAudio(clip, isPriority);
            Destroy(this.gameObject);
        }
    }*/

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NarratorController.DisplayAudio(clip, isPriority);
            Destroy(this.gameObject);
        }
    }
}
