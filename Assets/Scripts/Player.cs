using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float Jumpspeed = 8f;
    private float TravelSpeed = 3f;
    private Rigidbody2D body;
    private bool grounded;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        body.angularVelocity = 0;
    }

    private void Update()
    {

        if(WorldInfo.IsPaused() == false)
        {
            body.gravityScale = 1;
            body.velocity = new Vector2(TravelSpeed, body.velocity.y);
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
        body.velocity = new Vector2(body.velocity.x,Jumpspeed * 1.05f);
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

}
