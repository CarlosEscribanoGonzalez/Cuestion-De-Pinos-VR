using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PetalBehaviour : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable flowerInteractable;
    [SerializeField] private AudioSource source;
    [SerializeField] private Collider petalCollider;
    private bool grabbed = false;
    private static int petalsDetached = 0;
    [SerializeField] private AudioClip teQuiere;
    [SerializeField] private AudioClip noTeQuiere;

    private void Awake()
    {
        flowerInteractable.selectEntered.AddListener((args) => petalCollider.enabled = true);
        flowerInteractable.selectExited.AddListener((args) => { if (!grabbed) petalCollider.enabled = false; });
    }

    private void Update()
    {
        if (grabbed)
        {
            transform.SetParent(null); //Un bug no hace el SetParent bien mientras mantengo la flor agarrada.
            GetComponent<Rigidbody>().AddForce(new Vector3(0, Physics.gravity.y/20, 0), ForceMode.Force); 
            //Los pétalos caen a una velocidad mucho más lenta que el resto de objetos; en el rb tienen la gravedad desactivada
        }
    }

    public void DetachFlower()
    {
        if (!grabbed)
        {
            grabbed = true;
            source.Play();
            petalsDetached++; //Variable estática
            Destroy(this.gameObject.GetComponent<FixedJoint>());
            if (petalsDetached % 2 == 1) NarratorController.DisplayAudio(teQuiere, true);
            else NarratorController.DisplayAudio(noTeQuiere, true);
            if (petalsDetached == 7) GameObject.FindObjectOfType<SceneChanger>().ChangeScene();
        }
    }
}
