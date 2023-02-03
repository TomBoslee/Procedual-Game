using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldInfo {
    private static Vector2 SpawnPoint = new Vector2(-1,2);
    private static string Seed;
    public static bool HasDied = false;
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
}
