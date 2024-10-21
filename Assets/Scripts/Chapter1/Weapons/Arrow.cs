using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Arrow : MonoBehaviour
{
    public float speed = 10f;
    public Transform tip;
    private Rigidbody _rigidBody;
    private bool _inAir = false;
    private Vector3 _lastPosition = Vector3.zero;

    private bool shot = false;
    [SerializeField] private AudioSource arrowSource;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private AudioClip shotClip;
    [SerializeField] private Collider collider;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        PullInteraction.PullActionReleased += Release;
        Stop();
    }

    private void OnDestroy()
    {
        PullInteraction.PullActionReleased -= Release;
    }

    private void Release(float value)
    {
        PullInteraction.PullActionReleased -= Release;
        gameObject.transform.parent = null;
        _inAir = true;
        SetPhysics(true);
        Vector3 force = transform.forward * value * speed;
        _rigidBody.AddForce(force, ForceMode.Impulse);
        StartCoroutine(RotateWithVelocity());
        _lastPosition = tip.position;
        GetComponent<XRGrabInteractable>().enabled = true;
        shot = true;
        arrowSource.clip = shotClip;
        arrowSource.Play();
    }

    private IEnumerator RotateWithVelocity()
    {
        yield return new WaitForFixedUpdate();
        while (_inAir)
        {
            Quaternion newRotation = Quaternion.LookRotation(_rigidBody.velocity, transform.up);
            transform.rotation = newRotation;
            yield return null;
        }
    }

    private void FixedUpdate()
    {
        if (_inAir)
        {
            //CheckCollision();
            _lastPosition = tip.position;
        }
    }

    /*private void CheckCollision()
    {
        if(Physics.Linecast(_lastPosition, tip.position, out RaycastHit hitInfo))
        {
            if(hitInfo.transform.gameObject.layer != 6 && hitInfo.transform.gameObject.layer != 3) //Capa de colisión del jugador y de los grabbable
            {
                if(hitInfo.transform.TryGetComponent(out Rigidbody body))
                {
                    _rigidBody.interpolation = RigidbodyInterpolation.None;
                    transform.parent = hitInfo.transform;
                    body.AddForce(_rigidBody.velocity, ForceMode.Impulse);
                }
                arrowSource.clip = hitClip;
                arrowSource.Play();
                Stop();
            }
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        Stop();
        arrowSource.clip = hitClip;
        arrowSource.Play();
        if (collision.gameObject.CompareTag("TownFloor")) collider.enabled = false;
    }

    private void Stop()
    {
        _inAir = false;
        SetPhysics(false);
    }

    private void SetPhysics(bool usePhysics)
    {
        _rigidBody.useGravity = usePhysics;
        _rigidBody.isKinematic = !usePhysics;
    }

    public void GrabArrow()
    {
        if (shot) //Si fue disparada ya se recoge y se añade al inventario de flechas
        {
            ArrowSpawner.AddArrow();
            Destroy(this.gameObject);
        }
    }
}
