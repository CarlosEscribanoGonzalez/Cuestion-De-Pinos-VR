using UnityEngine;

public class ReloadManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private SimpleShoot shootManager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Slider")
        {
            source.Play();
            shootManager.EnableShoot();
        }
    }
}
