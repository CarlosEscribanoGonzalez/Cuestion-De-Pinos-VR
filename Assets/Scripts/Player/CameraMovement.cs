using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private CharacterController player;
    //Movimiento de cámara:
    [SerializeField] private float ySpeed;
    [SerializeField] private float xSpeed;
    [SerializeField] private float maxOffset;
    private bool canChange = true;
    private bool activated = true;
    private float initPos;
    //SFX de caminar:
    [SerializeField] private AudioSource footstepSFX;
    private bool isMoving = false;
    private bool inCooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<CharacterController>();
        Transform camera = transform.Find("Main Camera");
        camera.position = new Vector3(player.transform.position.x, camera.position.y, player.transform.position.z);
        initPos = this.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(player.velocity.x) > 0.7 || Mathf.Abs(player.velocity.z) > 0.7) isMoving = true;
        else isMoving = false;
        
        if (isMoving && activated)
        {
            MoveCamera();
            StopAllCoroutines();
        }
        else if(isMoving && !inCooldown)
        {
            footstepSFX.Play();
            StartCoroutine(FootstepCooldown());
        }
    }

    private void MoveCamera()
    {
        this.transform.position += new Vector3(xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, 0);
        if (transform.localPosition.y < initPos - maxOffset && canChange)
        {
            footstepSFX.Play();
            xSpeed *= -1;
            ySpeed *= -1;
            canChange = false;
        }
        else if (transform.localPosition.y > initPos + maxOffset && canChange)
        {
            ySpeed *= -1;
            canChange = false;
        }
        else if (transform.localPosition.y < initPos + maxOffset && transform.localPosition.y > initPos - maxOffset)
        {
            canChange = true;
        }
    }

    IEnumerator FootstepCooldown()
    {
        inCooldown = true;
        yield return new WaitForSeconds(0.6f);
        inCooldown = false;
    }

    public void SetActive(bool active)
    {
        activated = active;
    }

    public void SetOffset(float newOffset)
    {
        maxOffset = newOffset;
    }

    public void ResetPosition()
    {
        this.transform.localPosition = new Vector3(0, initPos, 0);
    }
}
