using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public static class Obstacles
{
    private static Dictionary<string,string> ObstacleDict = new Dictionary<string,string>();
    public static List<string> Keys = new List<string>();
    public static List<Obstacle> ObsList = new List<Obstacle>();

    //Path of obstacle text file
    //private static string Path = "Assets/Resources/Obstacles.txt";
    private static string Path = Application.streamingAssetsPath + "/Obstacles.txt";

    public static void initialiseObstacle()
    {
        ObstacleDict.Clear();
        Keys.Clear();
        string line;
        string key;
        string code;
        StreamReader reader = new StreamReader(Path);
        while (reader.Peek() >= 0)
        {
            line = reader.ReadLine();
            //Set rules for reading eachline
            if (!line.StartsWith("/")) {
            key = line.Split('-')[0];
            code = line.Split('-')[1];
            Obstacle temp = new Obstacle(code);
            ObsList.Add(temp);
            ObstacleDict.Add(key, code);
            Keys.Add(key);
            }
        }
        
        
    }
    //Load array of obstacles
    public static string LoadObstacle(string key)
    {
        return ObstacleDict[key];
    }
}
