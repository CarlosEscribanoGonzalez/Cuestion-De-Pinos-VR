using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopEnabler : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private GameObject light;

    void Awake()
    {
        if (MurderManager.trespinosShot <= 0) Destroy(door);   
        else
        {
            Destroy(light);
            GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) GameObject.FindObjectOfType<SceneChanger>().ChangeScene();
    }
}
