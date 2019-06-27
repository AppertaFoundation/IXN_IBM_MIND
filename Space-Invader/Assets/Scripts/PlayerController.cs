using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    [Header("Player Params")]
    public float speed;
    public float tilt;
    public Boundary boundary;

    [Header("Projectile Params")]
    public GameObject projectile;
    public Transform projectileSpawnPoint;
    public float fireDelta = 0.5F;
    private float nextFire = 0.5F;
    //private GameObject newProjectile;
    private float myTime = 0.0F;


    void Update()
    {
        //Fire Projectile
        myTime += Time.deltaTime;

        if (Input.GetButton("Fire1") && myTime > nextFire)
        {
            nextFire = myTime + fireDelta;
            //newProjectile = 
            Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);// as GameObject;

            // create code here that animates the newProjectile

            nextFire -= myTime;
            myTime = 0.0F;
            GetComponent<AudioSource>().Play();
        }
    }
    private void FixedUpdate()
    {
        //Move Player
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        GetComponent<Rigidbody>().velocity = movement * speed;

        GetComponent<Rigidbody>().position = new Vector3
            (
                Mathf.Clamp(GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
            );

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
    }
}
