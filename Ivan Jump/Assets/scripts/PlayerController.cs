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
    private float MoveVertical;
    
    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();

        MoveSpeed = 50f;
        JumpForce = 200f;
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        MoveHorizontal = Input.GetAxisRaw("Horizontal");
        MoveVertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if(MoveHorizontal != 0)
        {
            rb2D.AddForce(new Vector2(MoveHorizontal * MoveSpeed, 0f), ForceMode2D.Impulse);
        }

        if(!isJumping && MoveVertical > 0f)
        {
            rb2D.AddForce(new Vector2(0f, MoveVertical * JumpForce), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            isJumping = false;
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
