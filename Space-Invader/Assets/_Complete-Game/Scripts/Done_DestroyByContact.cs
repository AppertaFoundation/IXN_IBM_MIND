using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Threading;
using System.Net.WebSockets;

public class Done_DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private Done_GameController gameController;

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
            PlayerDeadSignal();
        }
        catch (Exception e) { Debug.Log("woe " + e.Message); }
    }

    // async void Connect2()
    // {
    //     cws = new ClientWebSocket();
    //     try
    //     {
    //         await cws.ConnectAsync(u, CancellationToken.None);
    //         if (cws.State == WebSocketState.Open) Debug.Log("connected");
    //         // SayHello();
    //         // GetStuff();
    //         EnemyShotSignal();
    //     }
    //     catch (Exception e) { Debug.Log("woe " + e.Message); }
    // }


	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <Done_GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Boundary" || other.tag == "Enemy")
		{
			return;
		}

		if (explosion != null)
		{
			Instantiate(explosion, transform.position, transform.rotation);
		}

		if (other.tag == "Player")
		{
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			// Enter websocket code to send a message that the player has been hit 
			Connect();
			gameController.GameOver();
		}
		
		gameController.AddScore(scoreValue);
		Destroy (other.gameObject);
		Destroy (gameObject);
	}

	void PlayerDeadSignal(){
		// Set the message used to determine that the RED LED will be activated on the breadboard
		ArraySegment<byte> b = new ArraySegment<byte>(Encoding.UTF8.GetBytes("Player Dead. Ending Game..."));
        cws.SendAsync(b, WebSocketMessageType.Text, true, CancellationToken.None);
        Debug.Log("send msg");
	}


    // void EnemyShotSignal(){
    //     // Set the message used to determine that the YELLOW LED will be activated on the breadboard
    //     ArraySegment<byte> b = new ArraySegment<byte>(Encoding.UTF8.GetBytes("Enemy Shot..."));
    //     cws.SendAsync(b, WebSocketMessageType.Text, true, CancellationToken.None);
    //     Debug.Log("send msg");
    // }


}