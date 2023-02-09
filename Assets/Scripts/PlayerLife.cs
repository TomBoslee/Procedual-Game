using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject ObstacleManager;
    private GameObject Player;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ObstacleManager = GameObject.Find("Obstacles");
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //kills player if pushed back by box
        if (rb.transform.position.x < WorldInfo.GetSpawn().x) { GameOver();}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")){
            GameOver();
        }
       
    }

    private void GameOver()
    {
     GameObject CurrentChild;
        //Destroys all obstacles on the map.
        for (int i = 0; i < ObstacleManager.transform.childCount; i++)
        {
            CurrentChild = ObstacleManager.transform.GetChild(i).gameObject;
            Player.transform.position = WorldInfo.GetSpawn();
            Destroy(CurrentChild);
        }
        WorldInfo.HasDied= true;
    }
}
