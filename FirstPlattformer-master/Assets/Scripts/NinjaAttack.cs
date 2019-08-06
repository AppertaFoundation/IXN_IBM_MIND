using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaAttack : MonoBehaviour {

    public GameObject shuriken;


    public Transform targetPlayer;
    public float AttackRange;
    public float AttackSpeed;
    public float shurikenSpeed;

    //SpriteRenderer to flip x
    private SpriteRenderer spriteRenderer;
    

    private bool inAttackRange;
    private float lastTimeSinceAttacked;

  
    // Use this for initialization
    void Start () {
        inAttackRange = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

        lastTimeSinceAttacked += Time.deltaTime;

        //Rotate ninja towards player
        if(targetPlayer.transform.position.x > transform.position.x && (spriteRenderer.flipX != true))
        {
            spriteRenderer.flipX = true;
        }
        else if(targetPlayer.transform.position.x < transform.position.x && (spriteRenderer.flipX == true))
        {
            spriteRenderer.flipX = false;
        }

        //Set/Check attackRange etc -> then attack 
        
        if(Vector2.Distance(targetPlayer.transform.position, gameObject.transform.position) < AttackRange)
        {
            inAttackRange = true;
        }
        else
        {
            inAttackRange = false;
        }

        if (inAttackRange && lastTimeSinceAttacked > AttackSpeed)
        {
            attack();
        }

	}

    void attack()
    {
        lastTimeSinceAttacked = 0;
        
        //newShuriken = Instantiate(shuriken) as GameObject;
        //newShuriken.transform.position = new Vector3(gameObject.transform.position.x - 10, gameObject.transform.position.y) ;
        //rb.AddRelativeForce(gameObject.transform.forward*shurikenSpeed);

        GameObject newShuriken = Instantiate(shuriken, transform.position + 1.0f * gameObject.transform.forward, gameObject.transform.rotation) as GameObject;
        Rigidbody2D rb = newShuriken.GetComponent<Rigidbody2D>();

        //better: not always at players position
        rb.velocity = (targetPlayer.transform.position - transform.position).normalized * shurikenSpeed;
    }

}
