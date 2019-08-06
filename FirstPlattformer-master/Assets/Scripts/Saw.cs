using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour {

    private float PosX1;
    private float PosY1;
    public float PosX2;
    public float PosY2;

    public bool moveToPos2;
    public bool moveToPos1;

    public float movSpeed;


    // Use this for initialization
    void Start () {
        PosX1 = transform.position.x;
        PosY1 = transform.position.y;

        moveToPos2 = true;
        moveToPos1 = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Debug.Log("Pos X1: " + PosX1);
        Debug.Log("Pos X2: " + PosX2);
        Debug.Log("Bool MoveToPos: " + moveToPos2);
        if (moveToPos2 == true)
        {
            //moove to Pos2
            GetComponent<Rigidbody2D>().velocity = new Vector2(movSpeed, GetComponent<Rigidbody2D>().velocity.y);
            if(gameObject.transform.position.x > PosX2)
            {
                moveToPos2 = false;
                moveToPos1 = true;
                
            }
        }
        if(moveToPos1 == true)
        {
            //moove to Pos1
            GetComponent<Rigidbody2D>().velocity = new Vector2(-movSpeed, GetComponent<Rigidbody2D>().velocity.y);
            if (gameObject.transform.position.x < PosX1)
            {
                moveToPos2 = true;
                moveToPos1 = false;
            }

        }
		
    }
}
