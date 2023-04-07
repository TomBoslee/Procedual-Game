using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Obstacle{
    public string code{get; set;}
    public int x { get; set; }
    public int y { get; set; }

    public Obstacle(string code, int x,int y) {
        this.code = code;
        this.x = x;
        this.y = y;
    }

    public Obstacle(string code) {
        this.code=code;
        int maxX = 0;
        int maxY = 0;
        foreach (string codeline in code.Split(","))
        {
            if (maxX < codeline.Length) { maxX = codeline.Length; }
            maxY++;
        }
        this.x = maxX;
        this.y = maxY;
    }

}
