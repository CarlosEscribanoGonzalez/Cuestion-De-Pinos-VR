using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineManager : MonoBehaviour
{
    [SerializeField] private int numBullets = 12;
    [SerializeField] private GameObject bullet;
    public bool isSimpleShot = true;

    public void DecreaseBullet()
    {
        numBullets--;
    }

    public int GetBullets()
    {
        return numBullets;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Base Model")
        {
            if (isSimpleShot)
            {
                SimpleShoot sShoot = other.gameObject.GetComponent<SimpleShoot>();
                if (sShoot) sShoot.SetMagazine(this);
            }
            else
            {
                SniperShoot sShoot = other.gameObject.GetComponent<SniperShoot>();
                if (sShoot) sShoot.SetMagazine(this);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.name == "Base Model")
        {
            if (isSimpleShot)
            {
                SimpleShoot sShoot = other.gameObject.GetComponent<SimpleShoot>();
                if (sShoot)
                {
                    sShoot.RemoveMagazine();
                    other.enabled = false; //A veces se detecta dos veces, por lo que el collider se activa cuando el socket lo detecta y se desactiva aquí
                    if (numBullets == 0) bullet.SetActive(false);
                }
            }
            else
            {
                SniperShoot sShoot = other.gameObject.GetComponent<SniperShoot>();
                if (sShoot)
                {
                    sShoot.RemoveMagazine();
                    other.enabled = false; //A veces se detecta dos veces, por lo que el collider se activa cuando el socket lo detecta y se desactiva aquí
                }
            }
        }
    }
}
