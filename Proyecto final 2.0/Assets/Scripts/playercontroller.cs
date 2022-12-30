using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    public float runSpeed = 2;
    public float jumpSpeed = 3;

    Rigidbody2D rg2d;

    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();

    }
    void FixedUpdate()
    {

        //right and left move

        if(Input.GetKey("d")|| Input.GetKey("right"))
        {
            rg2d.velocity = new Vector2(runSpeed, rg2d.velocity.y);

        }

        else if(Input.GetKey("a")|| Input.GetKey("left"))
        {
            rg2d.velocity = new Vector2(-runSpeed, rg2d.velocity.y);
            
        }

        else
        {
            rg2d.velocity = new Vector2 (0,rg2d.velocity.y);
        }

        // jump
        if( Input.GetKey("w") && checkGround.isGrounded)
        {
            rg2d.velocity = new Vector2 (rg2d.velocity.x , jumpSpeed);
        }
       
    }
}