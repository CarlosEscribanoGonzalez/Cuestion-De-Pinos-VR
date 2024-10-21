using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radio : MonoBehaviour
{
    [SerializeField] private AudioClip[] clips;
    [SerializeField] private AudioSource leftButtonSource;
    [SerializeField] private AudioSource rightButtonSource;
    [SerializeField] private GameObject text1;
    [SerializeField] private GameObject text2;
    private AudioSource source;
    private int index = 0;
    private bool isActive = false;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        text1.SetActive(false);
        text2.SetActive(false);
    }

    public void ChangeSong()
    {
        index++;
        if (index == clips.Length) index = 0;
        source.clip = clips[index];
        if(isActive) source.Play();
        rightButtonSource.Play();
    }

    public void ToggleRadio()
    {
        if (!isActive)
        {
            source.clip = clips[index];
            source.Play();
        }
        else source.Stop();
        isActive = !isActive;
        leftButtonSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            text1.SetActive(true);
            text2.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            text1.SetActive(false);
            text2.SetActive(false);
        }
    }
}
