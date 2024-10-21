using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DestroyOnTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip narratorClip;
    private bool reproduced = false;

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.CompareTag("Player")||collision.gameObject.CompareTag("Controller")) && !reproduced)
        {
            reproduced = true;
            NarratorController.DisplayAudio(narratorClip, true);
        }
    }
}
