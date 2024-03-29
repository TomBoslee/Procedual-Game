using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using System;

public static class Obstacles
{
    public static List<Obstacle> ObsList = new List<Obstacle>();

    //Path of obstacle text file
    //private static string Path = "Assets/Resources/Obstacles.txt";
    private static string Path = Application.streamingAssetsPath + "/Obstacles.txt";

    public static void initialiseObstacle()
    {
        ObsList.Clear();
        string line;
        string key;
        string code;
        string difficultyString;
        StreamReader reader = new StreamReader(Path);
        while (reader.Peek() >= 0)
        {
            line = reader.ReadLine();
            //Set rules for reading eachline
            if (!line.StartsWith("/")) {
            key = line.Split('-')[0];
            code = line.Split('-')[1];
            difficultyString = line.Split("-")[2];
            Obstacle temp = new Obstacle(code, Int32.Parse(difficultyString)) ;
            ObsList.Add(temp);
            }
        }
        
        
    }
}
