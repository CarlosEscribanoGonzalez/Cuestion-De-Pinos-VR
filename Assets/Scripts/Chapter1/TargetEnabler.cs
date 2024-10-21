using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEnabler : MonoBehaviour
{
    [SerializeField] private GameObject lamp;
    [SerializeField] private AudioClip narratorClip;

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Collider1") //Nombre de uno de los collider de la pistola
        {
            if(lamp) lamp.SetActive(true);
            StartCoroutine(DisplayNarratorAudio());
        }
    }

    IEnumerator DisplayNarratorAudio()
    {
        if (NarratorController.DisplayAudio(narratorClip, false))
        {
            Destroy(gameObject);
        }
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(DisplayNarratorAudio());
    }
}
