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
    public int ObstacleMax;
    public float scrollSpeed = 7.0f;
    public float Frequency = 0.5f;
    private float Counter = 0.0f;
    private int CurrentSeed;

    private int index = 0;

    private void Awake()
    {
        Obstacles.initialiseObstacle();
        ObstacleMax = Obstacles.Keys.Count;
        //Reset Values after Game
        Time.timeScale= 1.0f;
        WorldInfo.GameFin = false;
    }
    void Start()
    {
        //Generates the intial Scene
        GeneratePlayer();
        StartUI.SetActive(true);
        CurrentSeed = WorldInfo.GetSeed().GetHashCode();
        Random.InitState(CurrentSeed);
        //Generate for Enless Mode
        if (WorldInfo.Endless == true) {
            MissionUI.SetActive(false);
            EndlessUI.SetActive(true);
            GenerateRandomObstacles(); }
        //Generate for Mission Mode
        if(WorldInfo.Endless == false)
        {
            MissionUI.SetActive(true);
            EndlessUI.SetActive(false);
            Debug.Log(WorldInfo.GetSeed());
        }

    }

    // Update is called once per frame
    void Update()
    {
        //Stops updating if games finished
        if (WorldInfo.GameFin == false)
        {
            //Coditional Generation Depending on the mode the game is in.
            if (WorldInfo.Endless == true) { EndlessUpdate(); } else { MissionGeneration(); }
            //Causes the screen to scroll
            ObstacleManager.transform.position -= Vector3.right * (scrollSpeed * Time.deltaTime);
            GameObject CurrentChild;
            //Deletes obstacles if they are off screen.
            for (int i = 0; i < ObstacleManager.transform.childCount; i++){
                CurrentChild = ObstacleManager.transform.GetChild(i).gameObject;
                if (CurrentChild.transform.position.x < -15.0f){
                    Destroy(CurrentChild);
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

    private void MissionGeneration() {
        //TODO: Add Mission generation
        //Decoder(Obstacles.LoadObstacle(Obstacles.Keys[2]), 16, 1);
        if (Counter <= 0.0f) { 
            Decoder(Obstacles.LoadObstacle(Obstacles.Keys[index]), 16, 1);
            if (index != ObstacleMax - 1) {index++; } else { index = 0; }
            Counter= 1.0f;
        } else { Counter -= Time.deltaTime * Frequency; }
    }

    private void EndlessUpdate()
    {
        //Randomizes selectionn of obstacle according to counter
       if (Counter <= 0.0f) { GenerateRandomObstacles(); } else { Counter -= Time.deltaTime * Frequency; }
    }

    private void GenerateRandomObstacles() {
        int ran = Random.Range(0, ObstacleMax);
        Decoder(Obstacles.LoadObstacle(Obstacles.Keys[ran]), 16, 1);
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
                    RotateSpike(spike);
                }
                else if (codeline[n] == '4')
                {
                    GenerateJumpPad(X, Y);
                }
                else if (codeline[n] == 'G')
                { 
                    GenerateGoal();
                }

                X++;    

            }
            X = posX;
            Y++;
        }
        

    }

    //Generate goal game object
    private void GenerateGoal() {
        Transform goal = Instantiate(Goal);
        goal.name = "FinishLine";
        goal.position = new Vector2(16,3.75f);
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
    private void RotateSpike(Transform Spike)
    {
        Spike.rotation = Quaternion.Euler(0, 0, 180);
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
