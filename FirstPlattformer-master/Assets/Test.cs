using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(10, gameObject.GetComponent<Rigidbody2D>().velocity.y);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
