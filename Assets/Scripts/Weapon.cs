using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firepointright;
    public Transform firepointleft;
    public GameObject bulletPrefab;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")){
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firepointright.position, firepointright.rotation);
        Instantiate(bulletPrefab, firepointleft.position, firepointleft.rotation);
    }
}
