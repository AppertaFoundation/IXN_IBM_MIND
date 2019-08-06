using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaceScript : MonoBehaviour {

    private float StartingPositionX;
    private float StartingPositionY;
    private bool goDown;
    public float moveSpeedDown;
    public float moveSpeedUp;

	// Use this for initialization
	void Start () {
        StartingPositionX = gameObject.transform.position.x;
        StartingPositionY = gameObject.transform.position.y;
    }
	
	// Update is called once per frame
	void Update () {
		if (goDown == true)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x,  -moveSpeedDown);
        }
        else
        {
            if(gameObject.transform.position.y > StartingPositionY)
            {
                goDown = true;
            }
            else
            {
                
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, moveSpeedUp);
            }
        }

	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // goUp after hitting something
        goDown = false;
    }
}
