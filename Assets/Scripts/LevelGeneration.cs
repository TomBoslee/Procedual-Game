using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGeneration : MonoBehaviour
{
    public GameObject ObstacleManager;
    public GameObject MissionUI;
    public GameObject EndlessUI;
    public Transform Square;
    public Transform JumpPad;
    public Transform player;
    public Transform Spike;
    public int ObstacleMax;
    public float scrollSpeed = 5.0f;
    public float Frequency = 0.5f;
    private float Counter = 0.0f;

    private void Awake()
    {
        Obstacles.initialiseObstacle();
        ObstacleMax = Obstacles.Keys.Count;
        Time.timeScale= 1.0f;
    }
    void Start()
    {
        GeneratePlayer();
        if (WorldInfo.Endless == true) {
            MissionUI.SetActive(false);
            EndlessUI.SetActive(true);
            GenerateRandomObstacles(); }
        if(WorldInfo.Endless == false)
        {
            MissionUI.SetActive(true);
            EndlessUI.SetActive(false);
            Debug.Log(WorldInfo.GetSeed());
            MissionGeneration();
        }

    }

    // Update is called once per frame
    void Update()
    {
       if (WorldInfo.Endless == true) { EndlessUpdate(); } //else { MissionGeneration(); }
       ObstacleManager.transform.position -= Vector3.right * (scrollSpeed * Time.deltaTime);
       GameObject CurrentChild;
       for (int i = 0; i < ObstacleManager.transform.childCount; i++) {
            CurrentChild = ObstacleManager.transform.GetChild(i).gameObject;
            if (CurrentChild.transform.position.x < -15.0f) {
                    Destroy(CurrentChild);    
             }
        }
        
    }

    private void MissionGeneration() {
        Decoder(Obstacles.LoadObstacle(Obstacles.Keys[2]), 16, 1);
    }

    private void EndlessUpdate()
    {
       if (Counter <= 0.0f) { GenerateRandomObstacles(); } else { Counter -= Time.deltaTime * Frequency; }
    }

    private void GenerateRandomObstacles() {
        int ran = Random.Range(0, ObstacleMax);
        Decoder(Obstacles.LoadObstacle(Obstacles.Keys[ran]), 16, 1);
        Counter = 1.0f;
    }

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

                X++;    

            }
            X = posX;
            Y++;
        }
        

    }

    private void GenerateJumpPad(int posX, int posY)
    {
        Transform pad = Instantiate(JumpPad);
        pad.name = "JumpPad";
        pad.position = new Vector2(posX, posY);
        pad.parent = ObstacleManager.transform;
    }

    private void GenerateSquare(int posX, int PosY)
    {
        Transform box = Instantiate(Square);
        box.name = "Square";
        box.position = new Vector2(posX, PosY + 0.5f);
        box.parent = ObstacleManager.transform;
    }

    private Transform GenerateSpike(float PosX, float PosY)
    {
        Transform spike = Instantiate(Spike);
        spike.name = "Spike";
        spike.position = new Vector2(PosX, PosY + 0.4f);
        spike.parent = ObstacleManager.transform;
        return spike;
    }


    private void RotateSpike(Transform Spike)
    {
        Spike.rotation = Quaternion.Euler(0, 0, 180);
        Spike.Translate(0,-0.15f,0);
    }
        
        private void GeneratePlayer()
    {
        Transform Player = Instantiate(player);
        Player.name = "Player";
        Player.position = WorldInfo.GetSpawn();
        Player.AddComponent<BoxCollider2D>();
    }
}
