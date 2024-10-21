using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorManager : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    private float timer = 0; //El jugador debe tocar la puerta como haría una persona, dos o tres veces de seguido en un intervalo corto
    private Transform player;
    private ActionBasedController leftController;
    private ActionBasedController rightController;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        leftController = GameObject.Find("Left Controller").GetComponent<ActionBasedController>();
        rightController = GameObject.Find("Right Controller").GetComponent<ActionBasedController>();
        StartCoroutine(HandVibration()); 
    }

    private void Update()
    {
        timer += Time.deltaTime; //Al timer se le suma el tiempo
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Controller"))
        {
            source.transform.position = collision.transform.position;
            source.pitch = Random.Range(0.9f, 1.1f); 
            source.Play();
            if (timer > 0 && timer < 0.5)
            {
                GameObject.FindObjectOfType<SceneChanger>().ChangeScene();
            }
            timer = 0;
        }
    }

    private IEnumerator HandVibration()
    {
        float distance = Vector3.Distance(player.position, this.transform.position);
        float intensity = Mathf.Clamp(1 - distance / 10, 0, 1); //A mayor distancia menor intensidad
        float random1 = Random.Range(0.1f, 0.2f);
        float random2 = Random.Range(0.1f, 0.2f);
        leftController.SendHapticImpulse(intensity, random1);
        rightController.SendHapticImpulse(intensity, random1);
        yield return new WaitForSeconds(0.3f);
        leftController.SendHapticImpulse(intensity, random2);
        rightController.SendHapticImpulse(intensity, random2);
        yield return new WaitForSeconds(1.3f - intensity);
        StartCoroutine(HandVibration());
    }
}
