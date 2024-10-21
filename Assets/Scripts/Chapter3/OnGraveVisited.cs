using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGraveVisited : MonoBehaviour
{
    [SerializeField] private AudioClip narratorClip;
    public void GraveVisited()
    {
        NarratorController.DisplayAudio(narratorClip, true);
        GameObject.FindObjectOfType<SceneChanger>().ChangeScene();
    }
}
