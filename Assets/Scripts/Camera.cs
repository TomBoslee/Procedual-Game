using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;

    //Follow player on Y axis.
    void Update()
    {
        if (WorldInfo.GameFin == false)
        {
            player = GameObject.Find("Player").transform;
        }
        
        //transform.position = player.transform.position + new Vector3(3, 2, -5);
    }
}
