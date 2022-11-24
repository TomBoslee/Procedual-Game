using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.VFX;

public class LevelGeneration : MonoBehaviour
{
    public Transform player;
    public Tilemap Terrain;
    public TileBase[] tileBases;
    public Transform Spike;
    public int ChunkMax;

    private void Awake()
    {
        Terrain.AddComponent<TilemapCollider2D>();
        Terrain.tag = "Ground";
    }
    void Start()
    {
        int index = -8;
        for (int i = 0; i < ChunkMax; i++)
        {
            GenerateChunk(index);
            index = index + 4;
        }
        GeneratePlayer();
        //GenerateSpike(1,12.5f,1.5f);
        //GenerateStairs(0,16, 1);
        GenerateSpikeDuck(16, 1);

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

    private void GenerateChunk(int x)
    {

        for (int y = 0; y > -4; y = y-1)
        {
            for (int z = x; z < x + 4; z++)
            {
                Terrain.SetTile(new Vector3Int(z, y, 0), tileBases[4]);
            }

        }
        
        
    }

    private Transform GenerateSpike(float Posx, float Posy)
    {
        Transform spike = Instantiate(Spike);
        spike.name = "Spike";
        spike.position = new Vector2(Posx, Posy);
        return spike;
    }

    private void GenerateStairs(int Amount, int Posx, int Posy)
    {
        Terrain.SetTile(new Vector3Int(Posx, Posy, 0), tileBases[4]);
        Terrain.SetTile(new Vector3Int(Posx + 5, Posy, 0), tileBases[4]);
        Terrain.SetTile(new Vector3Int(Posx + 5, Posy + 1,0), tileBases[4]);
    }

    private void GenerateSpikeDuck(int Posx, int Posy)
    {
        Terrain.SetTile(new Vector3Int(Posx, Posy + 2, 0), tileBases[4]);
        Transform spike = GenerateSpike(Posx + 0.5f, Posy + 1.5f);
        RotateSpike(spike);
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
