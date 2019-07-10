using System;
using System.Text;
using System.Threading;
using System.Net.WebSockets;
using UnityEngine;

public class Comms : MonoBehaviour
{
    Uri u = new Uri("ws://169.254.243.241:1880/ws/simple"); 
    // Uri u = new Uri("ws://redify.eu-gb.mybluemix.net/ws/simple");
    // Uri u = new Uri("ws://communicationibm.eu-gb.mybluemix.net/ws/simple");
   
    ClientWebSocket cws = null;
    ArraySegment<byte> buf = new ArraySegment<byte>(new byte[1024]);

    void Start() { Connect(); }

    async void Connect()
    {
        cws = new ClientWebSocket();
        try
        {
            await cws.ConnectAsync(u, CancellationToken.None);
            if (cws.State == WebSocketState.Open) Debug.Log("connected");
            SayHello();
            // GetStuff();
        }
        catch (Exception e) { Debug.Log("woe " + e.Message); }
    }

    async void SayHello()
    {
        ArraySegment<byte> b = new ArraySegment<byte>(Encoding.UTF8.GetBytes("Game Start."));
        await cws.SendAsync(b, WebSocketMessageType.Text, true, CancellationToken.None);
        Debug.Log("send msg");
    }

    async void GetStuff()
    {
        WebSocketReceiveResult r = await cws.ReceiveAsync(buf, CancellationToken.None);
        Debug.Log("Got: " + Encoding.UTF8.GetString(buf.Array, 0, r.Count));
        // GetStuff();
        

    }
}