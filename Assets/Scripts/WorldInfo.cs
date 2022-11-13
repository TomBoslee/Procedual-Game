using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldInfo {
    private static Vector2 SpawnPoint;
    private static bool PlayerDead = false;
    private static bool Pause = false;
    public static void SetSpwan(Vector2 spawn)
    {
        SpawnPoint = spawn;
    }

    public static Vector2 GetSpawn()
    {
        return SpawnPoint;
    }

    public static void SwitchDeath()
    {
        PlayerDead = !PlayerDead;
    }

    public static bool PlayerStatus()
    {
        return PlayerDead;
    }

    public static void PauseGame()
    {
        Pause = !Pause;
    }

    public static bool PauseStatus()
    {
        return Pause;
    }
}
