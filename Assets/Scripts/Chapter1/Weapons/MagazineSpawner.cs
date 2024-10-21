using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MagazineSpawner : MonoBehaviour
{
    [SerializeField] private GameObject magazine;
    private GameObject lastInstance;

    private void Start()
    {
        InstantiateMagazine(null);
    }

    public void InstantiateMagazine(SelectEnterEventArgs args)
    {
        if(lastInstance) lastInstance.GetComponent<XRGrabInteractable>().firstSelectEntered.RemoveListener(InstantiateMagazine);
        lastInstance = Instantiate(magazine, this.transform);
        lastInstance.GetComponent<XRGrabInteractable>().firstSelectEntered.AddListener(InstantiateMagazine);
    }
}
