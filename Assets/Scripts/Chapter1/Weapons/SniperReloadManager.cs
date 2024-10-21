using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperReloadManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private SniperShoot shootManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Bolt")
        {
            source.Play();
            shootManager.EnableShoot();
        }
    }
}
