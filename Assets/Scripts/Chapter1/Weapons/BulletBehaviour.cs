using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private GameObject bulletHolePrefab;
    private RaycastHit hit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(this.gameObject, 10); //En caso de que no colisione con nada se destruye igual
    }

    private void OnEnable()
    {
        Vector3 origin = (transform.position += transform.forward/100);
        Physics.Raycast(transform.position , transform.forward, out hit);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject hole = Instantiate(bulletHolePrefab, hit.point, Quaternion.LookRotation(hit.normal));
        hole.transform.position += hole.transform.forward / 1000; //Para que no se solape con el objeto
        Destroy(this.gameObject);
    }
}
