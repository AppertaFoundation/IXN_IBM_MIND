using UnityEngine;
using System.Collections;
using System;
using System.Text;
using System.Threading;
using System.Net.WebSockets;

// TODO: finish score system later, this script is not used now.
public class PlayerScore : MonoBehaviour {

    private int score = 0;
    private int killed = 0;
    // private string notification;

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
            PlayerScoredSignal();
        }
        catch (Exception e) { Debug.Log("woe " + e.Message); }
    }

    public void IncreaseKillCount() {
        killed++;
        Connect();
        switch (killed) {
        case 1:
            score += 10;
            // notification = null;
            break;
        case 2:
            score += 15;
            // notification = "Double Kill";
            break;
        case 3:
            score += 25;
            // notification = "Triple Kill";
            break;
        case 4:
            score += 40;
            // notification = "Killing Spring";
            break;
        default:
            score += 60;
            // notification = "God Like";
            break;
        }
    }

    public void AddScore(int newScore) {
        score += newScore;
    }

    void PlayerScoredSignal()
    {
        // Set the message used to determine that the RED LED will be activated on the breadboard
        ArraySegment<byte> b = new ArraySegment<byte>(Encoding.UTF8.GetBytes("Player get score..."));
        cws.SendAsync(b, WebSocketMessageType.Text, true, CancellationToken.None);
        Debug.Log("send msg");
    }


}
