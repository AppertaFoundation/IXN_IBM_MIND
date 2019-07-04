using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// cont. https://www.youtube.com/watch?v=2KRLVRPdoKQ
public class DestroyPowerUp : MonoBehaviour
{
    public GameObject pickupEffect;
    public int new_speed = 2;

    void OnTriggerEnter(Collider other){
    	// Debug.Log (other.name);
    	// Boundary tag from unity
    	if (other.tag == "Boundary"){
    		return;
    	}
        // Instantiate(explosion, transform.position, transform.rotation);
        // If player is hit, increase the speed of the player
        if(other.tag == "Player"){
           PickUp(other);
           // other.speed = 54;

        }

    	// Destroy(other.gameObject);
    	// destroy powerup by marking them
         if (gameObject != null){
            Destroy(gameObject);
         }
    
    	

    }

    void PickUp(Collider player){

            Debug.Log("Player speed will increase now");
            
            // Spawn a cool effect
            Instantiate(pickupEffect, transform.position, transform.rotation);

            // Apply effect to the player
            Done_PlayerController playerController = player.GetComponent<Done_PlayerController>();
            playerController.speed *= new_speed;

        }
}
