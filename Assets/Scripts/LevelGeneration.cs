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
        GenerateSpike(1);
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

        for (int y = -2; y > -6; y = y-1)
        {
            for (int z = x; z < x + 4; z++)
            {
                Terrain.SetTile(new Vector3Int(z, y, 0), tileBases[4]);
            }

        }
        
        
    }

    private void GenerateSpike(int Amount)
    {
        for(int i = 0; i < Amount; i++)
        {
        Transform spike = Instantiate(Spike);
        spike.name = "Spike" + i;
        spike.transform.position = new Vector2(12.5f + i, -0.5f);
        }
    }

    private void GeneratePlayer()
    {
        Transform Player = Instantiate(player);
        Player.name = "Player";
        Player.transform.position = WorldInfo.GetSpawn();
        Player.AddComponent<BoxCollider2D>();
    }
}
