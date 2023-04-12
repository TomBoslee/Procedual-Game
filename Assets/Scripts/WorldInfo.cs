using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldInfo {
    //Set variables about the world that are required in multiple classes
    private static Vector2 SpawnPoint = new Vector2(-1,2);
    private static string Seed = "Hello";
    private static int Difficulty = 0;
    public static bool HasDied = false;
    public static bool GameFin = false;
    //Set false to test for mission level
    public static bool Endless = false;

    public static void SetSpwan(Vector2 spawn)
    {
        SpawnPoint = spawn;
    }

    public static Vector2 GetSpawn()
    {
        return SpawnPoint;
    }

    public static string GetSeed() { return Seed; }
    public static void SetSeed(string seed) { Seed = seed; }

    public static int GetDifficulty() {  return Difficulty; }

    public static void SetDifficulty(int difficulty) { Difficulty = difficulty; }
}
