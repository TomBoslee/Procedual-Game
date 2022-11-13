using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;

    // Update is called once per frame
    void Update()
    {
        player = GameObject.Find("Player").transform;
        transform.position = player.transform.position + new Vector3(0, 1, -5);
    }
}
