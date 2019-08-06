using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenScript : MonoBehaviour {

    private float time;

    private float animationTime;

    private bool collided;

    //Ninja
    public GameObject ninja;

	// Use this for initialization
	void Start () {
        collided = false;
        Physics2D.IgnoreCollision(ninja.GetComponent<Transform>().GetComponent<Collider2D>(), GetComponent<Collider2D>());
        
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;

        if (collided)
        {
            animationTime += Time.deltaTime;
            GetComponent<Animator>().speed -= (animationTime/80);
        }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ninja")
        {
        
            Physics2D.IgnoreCollision(ninja.GetComponent<Transform>().GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        collided = true;
        if (time > 5)
        {
            Destroy(gameObject);
        }
    }

    
   
}
