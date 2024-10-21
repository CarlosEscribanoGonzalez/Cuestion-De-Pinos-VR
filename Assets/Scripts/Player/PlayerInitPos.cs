using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerInitPos : MonoBehaviour
{
    private void Awake()
    {
        GameObject.FindWithTag("Player").transform.position = this.transform.position;
        GameObject.FindWithTag("Player").transform.rotation = this.transform.rotation;
        GameObject.FindWithTag("Player").transform.localScale = this.transform.localScale;
        //Por algún motivo al cambiar de escena el direct interactor se desactiva. 
        GameObject.Find("Right Controller").GetComponent<XRDirectInteractor>().enabled = false;
        GameObject.Find("Right Controller").GetComponent<XRDirectInteractor>().enabled = true;
        GameObject.Find("Left Controller").GetComponent<XRDirectInteractor>().enabled = false;
        GameObject.Find("Left Controller").GetComponent<XRDirectInteractor>().enabled = true;
    }
}
