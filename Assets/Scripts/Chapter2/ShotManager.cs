using System.Collections;
using UnityEngine;

public class ShotManager : MonoBehaviour
{
    [Header("Narrator audios")]
    [SerializeField] private AudioClip[] narratorClips = new AudioClip[3];
    [Header("Shoots")]
    private string weapon;
    private int numShots;
    private int shotCount = 0;
    private int trespinosShot; //Disparo que mata a Trespiños (no sé qué nombre ponerle)
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip pistol;
    [SerializeField] private AudioClip sniper;
    [SerializeField] private float totalShootingTime;
    private float timeBetweenShots;
    [SerializeField] private GameObject arrow;
    [Header("CrowdSFX")]
    [SerializeField] private AudioSource crowdSource;
    [SerializeField] private AudioClip screams;

    void Awake()
    {
        weapon = MurderManager.weapon;
        numShots = MurderManager.numShots;
        trespinosShot = MurderManager.trespinosShot;
        timeBetweenShots = totalShootingTime / numShots;
        if (weapon == "Pistol") { source.clip = pistol; source.volume = 0.5f; }
        else if (weapon == "Sniper") source.clip = sniper;
        Debug.Log(numShots);
    }

    private void Start()
    {
        if (numShots > 0) Invoke("CoroutineAfterDelay", 5.0f);
    }

    private void CoroutineAfterDelay()
    {
        StartCoroutine(ShootTrespinos());
    }

    private IEnumerator ShootTrespinos()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        shotCount++;
        if(shotCount == 1)
        {
            crowdSource.clip = screams; 
            crowdSource.Play();
            NarratorController.DisplayAudio(narratorClips[0], true);
            if (trespinosShot == 0) Invoke("DisplayHideDialogue", 3.5f);
        }
        if (weapon != "Bow") source.Play();
        else
        {
            GameObject newArrow = Instantiate(arrow);
            float randomZ = Random.Range(-10, 7);
            float randomX = Random.Range(-35, -25);
            newArrow.transform.position = new Vector3(randomX, -16.252f, randomZ);
        }
        if (shotCount == trespinosShot)
        {
            NarratorController.DisplayAudio(narratorClips[2], true);
            GameObject.FindObjectOfType<SceneChanger>().ForceScene("Room", 20f);
        }
        else if (shotCount < numShots) StartCoroutine(ShootTrespinos());
    }

    private void DisplayHideDialogue()
    {
        NarratorController.DisplayAudio(narratorClips[1], true);
    }
}
