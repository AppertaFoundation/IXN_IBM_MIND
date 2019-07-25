using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
// for the Websocket
using System;
using System.Text;
using System.Threading;
using System.Net.WebSockets;
using Random=UnityEngine.Random;

public class Done_GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text speedText;

    private bool gameOver;
    private bool restart;
    private int score;
    private int speed;
    


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
            turnOffREDLED();
            turnOffBLUELED();
        }
        catch (Exception e) { Debug.Log("woe " + e.Message); }
    }

    async void Connect2()
    {
        cws = new ClientWebSocket();
        try
        {
            await cws.ConnectAsync(u, CancellationToken.None);
            if (cws.State == WebSocketState.Open) Debug.Log("connected");
            // SayHello();
            GetStuff();
            EnemyShotSignal();
        }
        catch (Exception e) { Debug.Log("woe " + e.Message); }
    }

    async void Connect3()
    {
        cws = new ClientWebSocket();
        try
        {
            await cws.ConnectAsync(u, CancellationToken.None);
            if (cws.State == WebSocketState.Open) Debug.Log("connected");
           	introMessage();
        }
        catch (Exception e) { Debug.Log("woe " + e.Message); }
    }


    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        UpdateSpeed(10);
        StartCoroutine(SpawnWaves());
        Connect3();


    }

    void Update()
    {
        if (restart)
        {
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                // send a message to the WebSocket to turn off the RED LED
                Connect();
                break;
            }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
        // Show that an enemy or asteroid has been shot and activate YELLOW LED
        Connect2();

    }

    public void AddSpeed(int newSpeedValue){
        //speed += newSpeedValue;
        UpdateSpeed(newSpeedValue);

    }

    public void UpdateSpeed(int s){
        speedText.text = "Speed: " + s;
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
    }

    void  turnOffREDLED(){
        // Set the message used to determine that the RED LED will be activated on the breadboard
        ArraySegment<byte> b = new ArraySegment<byte>(Encoding.UTF8.GetBytes("Reset RED LED..."));
        cws.SendAsync(b, WebSocketMessageType.Text, true, CancellationToken.None);
        Debug.Log("send msg");

    }

    void  turnOffBLUELED(){
        // Set the message used to determine that the RED LED will be activated on the breadboard
        ArraySegment<byte> b = new ArraySegment<byte>(Encoding.UTF8.GetBytes("Reset BLUE LED..."));
        cws.SendAsync(b, WebSocketMessageType.Text, true, CancellationToken.None);
        Debug.Log("send msg");

    }

    void EnemyShotSignal(){
        // Set the message used to determine that the YELLOW LED will be activated on the breadboard
        ArraySegment<byte> b = new ArraySegment<byte>(Encoding.UTF8.GetBytes("Enemy Shot..."));
        cws.SendAsync(b, WebSocketMessageType.Text, true, CancellationToken.None);
        Debug.Log("send msg");
    }

    void introMessage(){
    	ArraySegment<byte> b = new ArraySegment<byte>(Encoding.UTF8.GetBytes("Intro Message..."));
        cws.SendAsync(b, WebSocketMessageType.Text, true, CancellationToken.None);
        Debug.Log("send msg");
    }

     async void GetStuff()
    {
        WebSocketReceiveResult r = await cws.ReceiveAsync(buf, CancellationToken.None);
        Debug.Log("Got: " + Encoding.UTF8.GetString(buf.Array, 0, r.Count));

         // Apply effect to the player
        Done_PlayerController playerController = GetComponent<Done_PlayerController>();
        if(Encoding.UTF8.GetString(buf.Array, 0, r.Count).Equals("Reducing Speed...")){
            Debug.Log("Reduce by 10");
            if(playerController != null){
                playerController.speed -= 10;
            }
            
        }
    }
}