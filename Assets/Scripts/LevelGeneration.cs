using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelGeneration : MonoBehaviour
{
    public GameObject ObstacleManager;
    public Transform Square;
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
    }
    void Start()
    {
        GeneratePlayer();
        GenerateRandomObstacles();

    }

    // Update is called once per frame
    void Update()
    {
        if (WorldInfo.IsPaused() == false)
        {
            if (Counter <= 0.0f) { GenerateRandomObstacles(); } else { Counter -= Time.deltaTime * Frequency; }


           ObstacleManager.transform.position -= Vector3.right * (scrollSpeed * Time.deltaTime);
            GameObject CurrentChild;
            for (int i = 0; i < ObstacleManager.transform.childCount; i++) {
                CurrentChild = ObstacleManager.transform.GetChild(i).gameObject;
                if (CurrentChild.transform.position.x < -15.0f) {
                    Destroy(CurrentChild);    
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
           WorldInfo.PauseGame();
        }
        
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

                X++;    

            }
            X = posX;
            Y++;
        }
        

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
        spike.position = new Vector2(PosX, PosY + 0.5f);
        spike.parent = ObstacleManager.transform;
        return spike;
    }


    private void RotateSpike(Transform Spike)
    {
        Spike.rotation = Quaternion.Euler(0, 0, 180);
    }
        
        private void GeneratePlayer()
    {
        Transform Player = Instantiate(player);
        Player.name = "Player";
        Player.position = WorldInfo.GetSpawn();
        Player.AddComponent<BoxCollider2D>();
    }

}
