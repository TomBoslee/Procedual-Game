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

    private void Awake()
    {
        Obstacles.initialiseObstacle();
        ObstacleMax = Obstacles.Keys.Count;
    }
    void Start()
    {
        GeneratePlayer();
        Decoder(Obstacles.LoadObstacle(Obstacles.Keys[4]), 16, 1);

    }

    // Update is called once per frame
    void Update()
    {
        if (WorldInfo.IsPaused() == true)
        {

           
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
           WorldInfo.PauseGame();
        }
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
        box.position = new Vector2(posX + 0.5f, PosY + 0.5f);
        box.parent = ObstacleManager.transform;
    }

    private Transform GenerateSpike(float PosX, float PosY)
    {
        Transform spike = Instantiate(Spike);
        spike.name = "Spike";
        spike.position = new Vector2(PosX + 0.5f, PosY + 0.5f);
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
