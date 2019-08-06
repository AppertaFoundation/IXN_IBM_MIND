using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for the Websocket
using System;
using System.Text;
using System.Threading;
using System.Net.WebSockets;
using Random=UnityEngine.Random;
using System.Timers;

// cont. https://www.youtube.com/watch?v=2KRLVRPdoKQ
public class DestroyPowerUp : MonoBehaviour
{
    public GameObject pickupEffect;
    private Done_GameController gameController;
    private static System.Timers.Timer aTimer;

    // WebSocket configuration
    Uri u = new Uri("ws://169.254.243.241:1880/ws/simple"); 
    ClientWebSocket cws = null;
    ArraySegment<byte> buf = new ArraySegment<byte>(new byte[1024]);

    async void Connect()
    {
        cws = new ClientWebSocket();
        try
        {
            await cws.ConnectAsync(u, CancellationToken.None);
            if (cws.State == WebSocketState.Open) Debug.Log("connected");
            // SayHello();
            // GetStuff();
            SpeedUpSignal();
        }
        catch (Exception e) { Debug.Log("woe " + e.Message); }
    }


    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<Done_GameController>();
        }
        else
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }


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
            // Connect to the WebSocket and activate the blue LED
            Connect();
            
            // Spawn a cool effect
            Instantiate(pickupEffect, transform.position, transform.rotation);

            // Apply effect to the player
            Done_PlayerController playerController = player.GetComponent<Done_PlayerController>();
            playerController.speed += 10;

            gameController.AddSpeed((int)playerController.speed);
            Debug.Log((int)playerController.speed);


            // Done_GameController gameController = player.GetComponent<Done_GameController>();
            // gameController.UpdateSpeed((int)playerController.speed);

            if(playerController.speed > 100){
            	// System.Threading.Thread.Sleep(5000);
            	// yield new WaitForSeconds(5);
            	// aTimer = new System.Timers.Timer(5000);
            	// Debug.Log("aTimer Output: " + aTimer);
                Debug.Log("Player Score > 100");
            	playerController.speed -= 10;
                gameController.UpdateSpeed((int)playerController.speed);

            }

        }

    void SpeedUpSignal(){
        ArraySegment<byte> b = new ArraySegment<byte>(Encoding.UTF8.GetBytes("Speed Up Collected..."));
        cws.SendAsync(b, WebSocketMessageType.Text, true, CancellationToken.None);
        Debug.Log("send msg");
    }


}
