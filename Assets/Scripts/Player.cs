using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    private Rigidbody2D body;
    private bool grounded;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

        if(WorldInfo.PauseStatus() == false)
        {
            body.gravityScale = 1;
            body.velocity = new Vector2(speed, body.velocity.y);
            if (Input.GetKeyDown(KeyCode.Space) && grounded)
            {
                jump();
            }
        }
        else
        {
            body.velocity = Vector2.zero;
            body.gravityScale = 0;
        }
        
         
    }

    private void jump()
    {
        body.velocity = new Vector2(body.velocity.x,speed);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }

}
