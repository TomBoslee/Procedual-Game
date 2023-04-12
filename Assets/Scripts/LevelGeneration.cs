using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGeneration : MonoBehaviour
{
    public GameObject ObstacleManager;
    public GameObject MissionUI;
    public GameObject EndlessUI;
    public GameObject GameFinUI;
    public GameObject StartUI;
    public Transform Goal;
    public Transform Square;
    public Transform JumpPad;
    public Transform player;
    public Transform Spike;
    private int ObstacleMax;
    //5 - 7 scroll speed seems good (Requiresa more testing)
    private float scrollSpeed = 7.0f;
    //0.5 - 1f is a good frequency
    private float Frequency = 0.5f;
    private float Counter = 0.0f;
    private int CurrentSeed;
    //Level Length
    private int length = 15;
    private List<int> level = new List<int>();
    private Boolean start = false;
    private void Awake()
    {
        Obstacles.initialiseObstacle();
        ObstacleMax = Obstacles.ObsList.Count;
        //Reset Values after Game
        Time.timeScale= 1.0f;
        WorldInfo.GameFin = false;
    }
    void Start()
    {
        if (WorldInfo.GetDifficulty() == 0) { length = 5; }
        else if (WorldInfo.GetDifficulty() == 1) { length = 15; }
        else if (WorldInfo.GetDifficulty() == 2) { length = 30; }

        //Generates the intial Scene
        GeneratePlayer();
        StartUI.SetActive(true);
        CurrentSeed = WorldInfo.GetSeed().GetHashCode();
        UnityEngine.Random.InitState(CurrentSeed);
        //Generate for Enless Mode
        if (WorldInfo.Endless == true) {
            MissionUI.SetActive(false);
            EndlessUI.SetActive(true);
            start = true;
            GenerateRandomObstacles(); }
        //Generate for Mission Mode
        if(WorldInfo.Endless == false)
        {
            MissionUI.SetActive(true);
            EndlessUI.SetActive(false);
            levelCreator();
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Stops updating if games finished
        if (WorldInfo.GameFin == false)
        {
            if (start)
            {
                if (WorldInfo.HasDied == true) { MissionGeneration(); WorldInfo.HasDied = false; }
                //Coditional Generation Depending on the mode the game is in.
                if (WorldInfo.Endless == true) { EndlessUpdate(); }
                //Causes the screen to scroll
                ObstacleManager.transform.position -= Vector3.right * (scrollSpeed * Time.deltaTime);
                GameObject CurrentChild;
                //Deletes obstacles if they are off screen.
               for (int i = 0; i < ObstacleManager.transform.childCount; i++)
                    {
                    CurrentChild = ObstacleManager.transform.GetChild(i).gameObject;
                    if (CurrentChild.transform.position.x < -15.0f)
                    {
                        Destroy(CurrentChild);
                    }
                }
            }
        }
        //Game Finished
        else {GameComplete(); }
        
    }

    private void GameComplete() {
        //Change UI to complete Screen
        StartUI.SetActive(false);
        EndlessUI.SetActive(false);
        MissionUI.SetActive(false);
        GameFinUI.SetActive(true);
        Time.timeScale = 0;
        Destroy(ObstacleManager.gameObject);
        Destroy(GameObject.Find("Player"));
    }

    private void levelCreator() {
        for (int n = 0; n < length; n++)
        {
            int ran = UnityEngine.Random.Range(0, ObstacleMax);
            level.Add(ran);
        }
        level.Add(-1);
        level.Add(-2);
        MissionGeneration();
    }

    private void MissionGeneration()
    {
        int posX = 16;
        int posY = 1;
        for (int n = 0; n < level.Count - 1; n++) {
        if (level[n] > -1)
        {
            Decoder(Obstacles.ObsList[level[n]].code, posX, posY);
            posX = posX + Obstacles.ObsList[level[n]].x;
        }
        else if (level[n] == -1) { GenerateGoal(posX); }
        posX = posX + 5;
        }
        start = true;
    }

    private void EndlessUpdate()
    {
        //Randomizes selectionn of obstacle according to counter
       if (Counter <= 0.0f) { GenerateRandomObstacles(); } else { Counter -= Time.deltaTime * Frequency;}
    }

    private void GenerateRandomObstacles() {
        int ran = UnityEngine.Random.Range(0, ObstacleMax);
        Decoder(Obstacles.ObsList[ran].code, 16, 1);
        Counter = 1.0f;
    }
    
    //Generates obstacles
    private void Decoder(string code, int posX, int posY)
    {
        int X = posX;
        int Y = posY;

        foreach (string codeline in code.Split(","))
        {

            for (int n = 0; n < codeline.Length; n++)
            {

                if (codeline[n] == '1')
                {
                    GenerateSquare(X, Y);
                }
                else if (codeline[n] == '2')
                {
                    GenerateSpike(X, Y);
                }
                else if (codeline[n] == '3')
                {
                    Transform spike = GenerateSpike(X, Y);
                    RotateSpike(spike, 180);
                }
                else if (codeline[n] == '4')
                {
                    GenerateJumpPad(X, Y);
                }
                else if (codeline[n] == '5') 
                {
                    Transform spike = GenerateSpike(X, Y);
                    RotateSpike(spike, 90);
                }

                X++;    

            }
            X = posX;
            Y++;
        }
        

    }

    //Generate goal game object
    private void GenerateGoal(int x) {
        Transform goal = Instantiate(Goal);
        goal.name = "FinishLine";
        goal.position = new Vector2(x,3.75f);
        goal.parent = ObstacleManager.transform;
    }

    //Generate Jump pad game object
    private void GenerateJumpPad(int posX, int posY)
    {
        Transform pad = Instantiate(JumpPad);
        pad.name = "JumpPad";
        pad.position = new Vector2(posX, posY);
        pad.parent = ObstacleManager.transform;
    }

    //Generate block game object
    private void GenerateSquare(int posX, int PosY)
    {
        Transform box = Instantiate(Square);
        box.name = "Square";
        box.position = new Vector2(posX, PosY + 0.5f);
        box.parent = ObstacleManager.transform;
    }

    //Generate spike game object
    private Transform GenerateSpike(float PosX, float PosY)
    {
        Transform spike = Instantiate(Spike);
        spike.name = "Spike";
        spike.position = new Vector2(PosX, PosY + 0.4f);
        spike.parent = ObstacleManager.transform;
        return spike;
    }

    //Rotates existing spike 180
    private void RotateSpike(Transform Spike,float rotation)
    {
        Spike.rotation = Quaternion.Euler(0, 0, rotation);
        Spike.Translate(0,-0.15f,0);
    }

    //Generate player game object
    private void GeneratePlayer()
    {
        Transform Player = Instantiate(player);
        Player.name = "Player";
        Player.position = WorldInfo.GetSpawn();
        player.gameObject.SetActive(true);
        Player.AddComponent<BoxCollider2D>();
    }
}
