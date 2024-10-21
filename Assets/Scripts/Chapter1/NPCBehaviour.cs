using System.Collections;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    public static int killCount = 0;
    private static float speed = 2;
    private int randomDirection = 0;
    private float randomTimer = 0;
    private Animator anim;
    private bool killed = false;
    [SerializeField] private Sprite deadSprite;
    [SerializeField] private AudioSource normalCrowd;
    [SerializeField] private AudioSource screams;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(GenerateRandomNumber());
    }

    // Update is called once per frame
    void Update()
    {
        if (!killed) MoveCharacter();
    }

    private void MoveCharacter()
    {
        if(randomDirection == 1) this.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        else if(randomDirection == 3) this.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        else if(randomDirection == 2) this.transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        else if(randomDirection == 4) this.transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
        GetComponent<SpriteRenderer>().sortingOrder = (int) this.transform.position.x;
    }

    private void AnimateCharacter()
    {
        if (randomDirection == 2) GetComponent<SpriteRenderer>().flipX = false;
        
        if (randomDirection == 4)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            anim.SetInteger("Direction", 2);
        }
        else anim.SetInteger("Direction", randomDirection);
    }

    private void InvertDirection()
    {
        if (randomDirection == 1) randomDirection = 3;
        else if (randomDirection == 3) randomDirection = 1;
        else if (randomDirection == 2) randomDirection = 4;
        else if (randomDirection == 4) randomDirection = 2;
        AnimateCharacter();
    }

    IEnumerator GenerateRandomNumber()
    {
        int random = Random.Range(0, 2); //Con una probabilidad del 50% se fuerza a que el NPC se quede quieto para que sea más fácil darle
        if (random == 0) randomDirection = 0;
        else randomDirection = Random.Range(0, 5); 
        randomTimer = Random.Range(0.5f, 2);
        AnimateCharacter();
        yield return new WaitForSeconds(randomTimer);
        StartCoroutine(GenerateRandomNumber());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            KillCharacter();
            if(!this.CompareTag("Trespinos")) killCount++;
            Destroy(collision.gameObject);
            normalCrowd.enabled = false;
            screams.enabled = true;
            speed = 4; //Todos los personajes aceleran al presenciar una muerte. POSIBLE CAMBIO: que no haga falta matar a nadie si llevas un franco porque se escucha mucho
            Debug.Log("Asesino!");
        }
        else InvertDirection();
    }

    private void KillCharacter()
    {
        killed = true;
        StopAllCoroutines();
        anim.enabled = false;
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = deadSprite;
        renderer.sortingOrder = -2000;
        if (this.CompareTag("Trespinos")) GameObject.FindObjectOfType<MurderManager>().TrespinosKilled();
    }
}
