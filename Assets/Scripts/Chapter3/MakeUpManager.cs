using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MakeUpManager : MonoBehaviour
{
    [SerializeField] private float distance = 0.4f;
    [SerializeField] private float makeupTimer = 9f;
    [SerializeField] private float rechargeTime = 3f;
    [SerializeField] private Material correctorMaterial;
    [SerializeField] private Material whiteMaterial;
    [SerializeField] private MeshRenderer tip;
    private Transform mainCamera;
    private float tipTimer = 0f;
    private XRBaseControllerInteractor currentInteractor;
    private bool canBeUsed = false;

    private float timer = 0;
    [SerializeField] private AudioClip makeupFinished;
    [SerializeField] private AudioClip playerDistracted;
    [SerializeField] private AudioClip openDoor;
    
    void Awake()
    {
        mainCamera = GameObject.Find("Main Camera").transform;
    }

    void Update()
    {
        if (Vector3.Distance(mainCamera.position, this.transform.position) < distance && tipTimer > 0 && canBeUsed)
        {
            makeupTimer -= Time.deltaTime;
            tipTimer -= Time.deltaTime;
            if(currentInteractor) currentInteractor.SendHapticImpulse(0.25f, Time.deltaTime);
            if(tipTimer <= 0)
            {
                tip.material = whiteMaterial;
            }
            if (makeupTimer <= 0)
            {
                NarratorController.DisplayAudio(makeupFinished, true);
                StartCoroutine(DoorKnocked());
            }
        }
        timer += Time.deltaTime; //Si el jugador se distrae el narrador le dice que siga maquillándose:
        if (timer > 30 && makeupTimer > 0)
        {
            timer = -1000;
            NarratorController.DisplayAudio(playerDistracted, false);
        }
    }

    public void RechargeTip() //Llamado en Hover Entered
    {
        tip.material = correctorMaterial;
        tipTimer = rechargeTime;
        canBeUsed = false;
    }

    public void UseEnabler() //Llamado en Hover Exited
    {
        canBeUsed = true;
    }

    public void SetCurrentController(SelectEnterEventArgs args)
    {
        if (args.interactorObject is XRBaseControllerInteractor interactor)
        {
            currentInteractor = interactor;
        }
    }

    IEnumerator DoorKnocked()
    {
        yield return new WaitForSeconds(14.5f);
        if(MurderManager.trespinosShot > 0)
        {
            GameObject.FindObjectOfType<SceneChanger>().ChangeScene();
        }
        else
        {
            yield return new WaitForSeconds(1f);
            GameObject.Find("KnockSound").GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(1f);
            NarratorController.DisplayAudio(openDoor, true);
            GameObject.FindObjectOfType<OnDoorOpen>().GetComponent<BoxCollider>().enabled = true;
        }
        
    }
}
