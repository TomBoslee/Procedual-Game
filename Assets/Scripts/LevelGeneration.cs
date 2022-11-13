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
    }

    // Update is called once per frame
    void Update()
    {
        if (WorldInfo.IsPaused() == true)
        {

            if (WorldInfo.IsPlayerAlive() == false)
            {
                PlayerDeath();
            }
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

    private void GeneratePlayer()
    {
        Transform Player = Instantiate(player);
        Player.name = "Player";
        Player.transform.position = WorldInfo.GetSpawn();
        Player.AddComponent<BoxCollider2D>();
        WorldInfo.SwitchDeath();
    }

    private void PlayerDeath()
    {
        Destroy(GameObject.Find("Player"));
        GeneratePlayer();
    }
}
