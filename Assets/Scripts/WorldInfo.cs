using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldInfo {
    private static Vector2 SpawnPoint = new Vector2(-1,2);
    public static bool HasDied = false;

    public static void SetSpwan(Vector2 spawn)
    {
        SpawnPoint = spawn;
    }

    public static Vector2 GetSpawn()
    {
        return SpawnPoint;
    }
}
