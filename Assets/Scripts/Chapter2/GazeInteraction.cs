using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GazeInteraction : MonoBehaviour
{
    [SerializeField] private AudioClip narratorClip;
    [SerializeField] private XRSimpleInteractable interactable;
    private bool clipReproduced = false;

    private void Awake()
    {
        
    }

    public void OnSight(HoverEnterEventArgs args)
    {
        if (!clipReproduced)
        {
            Debug.Log("VISTO");
            NarratorController.DisplayAudio(narratorClip, true);
            clipReproduced = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && MurderManager.numShots == 0)
        {
            GameObject.FindObjectOfType<XRGazeInteractor>().hoverEntered.AddListener(OnSight);
            interactable.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.FindObjectOfType<XRGazeInteractor>().hoverEntered.RemoveListener(OnSight);
            interactable.enabled = false;
        }
    }
}
