using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldInfo {
    private static Vector2 SpawnPoint;
    private static bool PlayerState= false;
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
        PlayerState = !PlayerState;
    }

    public static bool IsPlayerAlive()
    {
        return PlayerState;
    }

    public static void PauseGame()
    {
        Pause = !Pause;
    }

    public static bool IsPaused()
    {
        return Pause;
    }
}
