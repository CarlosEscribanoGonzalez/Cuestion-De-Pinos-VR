using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PunchController : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip narratorClip;
    private bool playerIsNear = false;
    private static float punchCount = 0;
    //Controlador izquierdo:
    private Transform leftController;
    private Vector3 leftVelocity;
    private Vector3 leftPos;
    //Controlador derecho:
    private Transform rightController;
    private Vector3 rightVelocity;
    private Vector3 rightPos;
    
    void Awake()
    {
        leftController = GameObject.Find("Left Controller").transform;
        rightController = GameObject.Find("Right Controller").transform;

        leftPos = leftController.position;
        rightPos = rightController.position;
    }

    void Update()
    {
        if (playerIsNear)
        {
            CalculateControllersSpeed();
        }
    }

    private void CalculateControllersSpeed()
    {
        Vector3 newRightPos = rightController.position;
        Vector3 newLeftPos = leftController.position;
        rightVelocity = (newRightPos - rightPos) / Time.deltaTime;
        leftVelocity = (newLeftPos - leftPos) / Time.deltaTime;
        rightPos = newRightPos;
        leftPos = newLeftPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsNear = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Controller"))
        {
            source.transform.position = collision.transform.position;
            //La velocidad máxima a la que alcanzo a poner el controlador es a 5,
            //por lo que usamos 0,2 para que en la máxima velocidad el volumen sea 1
            if (collision.gameObject.name == "LeftHand")
            {
                source.volume = 0.2f * leftVelocity.magnitude;
                leftController.GetComponent<ActionBasedController>().SendHapticImpulse(source.volume, 0.1f);
            }
            else
            {
                source.volume = 0.2f * rightVelocity.magnitude;
                rightController.GetComponent<ActionBasedController>().SendHapticImpulse(source.volume, 0.1f);
            }

            if (source.volume >= 0.5f)
            {
                punchCount++; //Sólo se cuentan los puñetazos considerablemente fuertes
                if (punchCount == 10)
                {
                    NarratorController.DisplayAudio(narratorClip, true);
                    Debug.Log("Puñeteado");
                }
                else if (punchCount == 40) GameObject.FindObjectOfType<SceneChanger>().ChangeScene();
            }
            source.Play();
        }
    }
}
