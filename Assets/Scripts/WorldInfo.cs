using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WorldInfo {
    private static Vector2 SpawnPoint = new Vector2(-1,2);
    private static bool Pause = false;
    private static float Attempt = 0;

    public static void SetSpwan(Vector2 spawn)
    {
        SpawnPoint = spawn;
    }

    public static Vector2 GetSpawn()
    {
        return SpawnPoint;
    }

    public static void PauseGame()
    {
        Pause = !Pause;
    }

    public static bool IsPaused()
    {
        return Pause;
    }

    public static float GetAttempt() {
        return Attempt;
    }
    public static void IncrementAttempt() {
        Attempt = Attempt + 0.5f;
    }
}
