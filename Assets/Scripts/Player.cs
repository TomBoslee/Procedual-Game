using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float Jumpspeed = 8f;
    private Rigidbody2D body;
    private bool grounded;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        body.gravityScale = 5;
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            jump();
        }
        
         
    }

    private void jump()
    {
        body.AddForce(Vector3.up * (Jumpspeed * body.mass * body.gravityScale * 20f));
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
