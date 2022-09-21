using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;

    private float MoveSpeed;
    private float JumpForce;
    private bool isJumping;
    private float MoveHorizontal;
    private bool DoubleJumpUsed;
    
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();

        MoveSpeed = 50f;
        JumpForce = 200f;
        isJumping = false;
        DoubleJumpUsed = false;
    }

    void Update()
    {
        MoveHorizontal = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        if(MoveHorizontal != 0)
        {
            rb2D.AddForce(new Vector2(MoveHorizontal * MoveSpeed, 0f), ForceMode2D.Impulse);
        }

        if(!isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            rb2D.AddForce(new Vector2(0f, 7*JumpForce), ForceMode2D.Impulse);
        }

        if(!DoubleJumpUsed && isJumping && Input.GetKeyDown(KeyCode.Space))
        {
            rb2D.AddForce(new Vector2(0f, 7*JumpForce), ForceMode2D.Impulse);
            DoubleJumpUsed = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            isJumping = false;
            DoubleJumpUsed = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            isJumping = true;
        }
    }

}
