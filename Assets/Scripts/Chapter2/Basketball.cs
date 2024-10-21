using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class Basketball : MonoBehaviour
{
    private int bounceCount = 0;
    private int numScores = 0;
    private Rigidbody rb;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioSource boardSource;
    [SerializeField] private AudioClip floorBounceSFX;
    [SerializeField] private AudioClip grabSFX;
    [SerializeField] private AudioClip narratorClip;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        bounceCount++;
        if (collision.gameObject.CompareTag("Board"))
        {
            boardSource.volume = 0.1f * rb.velocity.magnitude;
            boardSource.transform.position = collision.transform.position;
            boardSource.Play();
        }
        else
        {
            source.clip = floorBounceSFX;
            source.volume = 0.1f * rb.velocity.magnitude;
            source.Play();
        }

        if (bounceCount == 30 && numScores == 0) StartCoroutine(NarratorDialogue());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "BallDetector" && rb.velocity.y < 0)
        {
            numScores++;
            other.GetComponent<AudioSource>().Play();
        }
    } 

    public void DisplayGrabSound()
    {
        source.clip = grabSFX;
        source.volume = 0.5f;
        source.Play();
    }

    IEnumerator NarratorDialogue()
    {
        yield return new WaitForSeconds(0.5f);
        if(!NarratorController.DisplayAudio(narratorClip, false)) StartCoroutine(NarratorDialogue());
    }
}
