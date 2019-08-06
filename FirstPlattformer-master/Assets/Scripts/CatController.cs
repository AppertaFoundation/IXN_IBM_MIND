using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatController : MonoBehaviour {

    public Transform target;//set target from inspector instead of looking in Update
    public float speed = 3f;
    private Animator anim;        // The animator that controls the characters animations



    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        //rotate to look at the player
        transform.LookAt(target.position);
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);//correcting the original rotation


        //move towards the player
        if (Vector3.Distance(transform.position, target.position) > 7f)
        {//move if distance from target is greater than 1
            anim.SetFloat("speed", 2);
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        else
        {
            anim.SetFloat("speed", 0);
        }

    }

}

