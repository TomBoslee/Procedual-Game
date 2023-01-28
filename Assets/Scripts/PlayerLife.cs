using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private GameObject ObstacleManager;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ObstacleManager = GameObject.Find("Obstacles");
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.transform.position.x < WorldInfo.GetSpawn().x) { GameOver(); }
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
        for (int i = 0; i < ObstacleManager.transform.childCount; i++)
        {
            CurrentChild = ObstacleManager.transform.GetChild(i).gameObject;
            Destroy(CurrentChild);
        }
        WorldInfo.IncrementAttempt();
    }
}
