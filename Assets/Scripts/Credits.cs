using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    private float timer;
    void Update()
    {
        this.transform.position += new Vector3(0, 2 * Time.deltaTime, 0);
        timer += Time.deltaTime;
        if (timer >= 30) GameObject.FindObjectOfType<SceneChanger>().ForceScene("Gym", 1.0f);
    }
}
