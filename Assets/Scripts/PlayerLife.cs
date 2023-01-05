using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        WorldInfo.IncrementAttempt();
        transform.position = WorldInfo.GetSpawn();
    }
}
