using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MurderManager : MonoBehaviour
{
    public static string weapon = null;
    public static int numShots = 0;
    public static int trespinosShot = -1; //Tiro que acaba con la vida de Trespiños
    private bool inPosition = false;
    [SerializeField] private AudioClip[] narratorClips = new AudioClip[7];
    private int killCount = 0;



    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //Audios:
            if (numShots == 0) NarratorController.DisplayAudio(narratorClips[4], true);
            else if (weapon == "Sniper" || (weapon == "Pistol" && NPCBehaviour.killCount > 0))
                NarratorController.DisplayAudio(narratorClips[5], true);            
            else NarratorController.DisplayAudio(narratorClips[6], true);
            //Cambio de escena:
            GameObject.FindObjectOfType<SceneChanger>().ChangeScene();
            inPosition = false;
            //Un bug hace que las flechas se detecten dobles. Se debe dividir el número de disparos entre 2
            if (weapon == "Bow")
            {
                numShots /= 2;
                trespinosShot /= 2;
            }
            Debug.Log($"Arma usada: {weapon}");
        }
        else if (other.CompareTag("Bullet") && inPosition)
        {
            numShots++;
            Debug.Log($"Número de disparos: {numShots}");
            StartCoroutine(DisplayAudio());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inPosition = true;
            NarratorController.DisplayAudio(narratorClips[0], true);
        }
        else if (other.CompareTag("Pistol")) weapon = "Pistol";
        else if (other.CompareTag("Sniper")) weapon = "Sniper";
        else if (other.CompareTag("Bow")) weapon = "Bow";
    }

    public void TrespinosKilled()
    {
        trespinosShot = numShots;
        NarratorController.DisplayAudio(narratorClips[1], true);
        Debug.Log($"Disparo que mató a Trespiños: {trespinosShot}");
    }

    IEnumerator DisplayAudio()
    {
        yield return new WaitForSeconds(0.5f);
        if(NPCBehaviour.killCount == 0)
        {
            NarratorController.DisplayAudio(narratorClips[2], false);
        }
        else if(killCount != NPCBehaviour.killCount)
        {
            killCount = NPCBehaviour.killCount;
            NarratorController.DisplayAudio(narratorClips[3], true);
        }
    }
}
