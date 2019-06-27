using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Projectile Params")]
    public GameObject projectile;
    public Transform projectileSpawnPoint;
    public float fireRate;
    public float delay;

    void Start()
    {
        InvokeRepeating("Fire", delay, fireRate);
    }

    void Fire()
    {
            Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);// as GameObject;
            GetComponent<AudioSource>().Play();
    }
}
