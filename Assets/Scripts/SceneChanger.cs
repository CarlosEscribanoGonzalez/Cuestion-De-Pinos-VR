using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private Animator anim;
    [SerializeField] private float fadeinSpeed;
    [SerializeField] private float fadeoutSpeed;
    private bool forced = false;
    private string forcedScene;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        anim.speed = fadeinSpeed;
    }

    public void ChangeScene()
    {
        anim.speed = fadeoutSpeed;
        anim.SetTrigger("ChangeScene");
    }

    public void ForceScene(string name, float speed)
    {
        anim.speed = speed;
        forcedScene = name;
        forced = true;
        anim.SetTrigger("ChangeScene");
    }

    private void OnAnimationEnded()
    {
        GameObject distanceGrab = GameObject.Find("Distance Grab");
        if(distanceGrab) distanceGrab.SetActive(false);
        if (!forced) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        else SceneManager.LoadScene(forcedScene);
    }
}
