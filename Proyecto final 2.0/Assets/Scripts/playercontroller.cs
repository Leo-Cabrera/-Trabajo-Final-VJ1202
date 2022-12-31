using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    public float runSpeed = 2;
    public float jumpSpeed = 3;
    public float doublejumpSpeed = 2.5f;
    private bool canDoubleJump;

    public SpriteRenderer SpriteRenderer;
    public Animator animator;

    Rigidbody2D rg2d;

    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
         // jump
        if( Input.GetKey("w"))
        {
            if (checkGround.isGrounded)
            {
                canDoubleJump = true;
                rg2d.velocity = new Vector2 (rg2d.velocity.x , jumpSpeed);
            }
            else 
            {
                if(Input.GetKeyDown("w"))
                {
                    if(canDoubleJump)
                    {
                        animator.SetBool("DoubleJump",true);
                        rg2d.velocity = new Vector2 (rg2d.velocity.x , doublejumpSpeed);
                        canDoubleJump = false;
                    }
                }
            }
        }

        if (checkGround.isGrounded==false && rg2d.velocity.y > 0)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Jump", true);
            animator.SetBool("Fall", false );
        }

        else if (checkGround.isGrounded==false && rg2d.velocity.y < 0)
        {
             animator.SetBool("Run", false);
            animator.SetBool("Jump", false);
            animator.SetBool("Fall", true);
        }

        if (checkGround.isGrounded==true)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("DoubleJump", false);
            animator.SetBool("Fall", false);
        
        }

        if (rg2d.velocity.y < 0)
        {
            animator.SetBool("Fall", true );
        }
        else if (rg2d.velocity.y > 0);
        {
            animator.SetBool("Fall", false );
        }

    }
    void FixedUpdate()
    {

        //right and left move

        if(Input.GetKey("d")|| Input.GetKey("right"))
        {
            rg2d.velocity = new Vector2(runSpeed, rg2d.velocity.y);
            SpriteRenderer.flipX = false;
            animator.SetBool("Run", true);
            animator.SetBool("Duck",false);
            

        }

        else if(Input.GetKey("a")|| Input.GetKey("left"))
        {
            rg2d.velocity = new Vector2(-runSpeed, rg2d.velocity.y);
            SpriteRenderer.flipX = true;
            animator.SetBool("Run", true);
            animator.SetBool("Duck",false);
            
        }

        else
        {
            rg2d.velocity = new Vector2 (0,rg2d.velocity.y);
            animator.SetBool("Run", false);
            animator.SetBool("Duck",false);
            
        }

        //duck move
        if (Input.GetKey("s") || Input.GetKey("down"))
        {
            animator.SetBool("Duck",true);
        }

       
       
    }
}