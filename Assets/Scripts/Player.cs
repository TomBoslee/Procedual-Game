using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float Jumpspeed = 9f;
    private float SuperJumpSpeed = 8f;
    private Rigidbody2D body;
    private bool grounded;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        body.gravityScale = 5;
        //Jump Input
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            jump(Jumpspeed);
        }
        
         
    }

    private void jump(float JSpeed)
    {
        //Adds force for jump
        body.AddForce(Vector3.up * (JSpeed * body.mass * body.gravityScale * 20f));
        grounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Stops player from jumping in the air
        if(collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Triggers Jump
        if (collision.gameObject.CompareTag("JumpPad")) {
            jump(SuperJumpSpeed);
        }
        //Triggers Goal State
        if (collision.gameObject.CompareTag("Goal")) {
            WorldInfo.GameFin = true;
        }
        if (collision.gameObject.CompareTag("CheckPoint")) {
            WorldInfo.DoUpdate = true;
        }
    }

}
