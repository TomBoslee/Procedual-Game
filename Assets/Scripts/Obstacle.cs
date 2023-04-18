using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Obstacle{
    public string code{get; set;}
    public int x { get; set; }
    public int y { get; set; }

    public int diff { get; set; }

    //new obstacle able to set all values
    public Obstacle(string code, int x,int y,int d) {
        this.code = code;
        this.x = x;
        this.y = y;
        this.diff = d;
    }

    //creates obstacle from base written document
    public Obstacle(string code, int d) {
        this.code=code;
        int maxX = 0;
        int maxY = 0;
        foreach (string codeline in code.Split(","))
        {
            if (maxX < codeline.Length) { maxX = codeline.Length; }
            maxY++;
        }
        this.diff = d;
        this.x = maxX;
        this.y = maxY;
    }

}
