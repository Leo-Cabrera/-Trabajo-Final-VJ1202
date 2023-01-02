using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    public float runSpeed = 2;
    public float jumpSpeed = 3;
    public float doublejumpSpeed = 2.5f;
    public float timeInvincible;
    public float invincibleTimer;
    private bool isInvincible;
    private bool canDoubleJump;
    public int maxHealth = 5;
    public GameObject projectilePrefab;
    
    public int health { get { return currentHealth; }}
    int currentHealth;

    public SpriteRenderer SpriteRenderer;
    public Animator animator;
    Vector2 lookDirection = new Vector2(1,0);

    Rigidbody2D rg2d;

    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;
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

        if (checkGround.isGrounded==false)
        {
            animator.SetBool("Run", false);
            animator.SetBool("Jump", true);
            
        }



        if (checkGround.isGrounded == true)
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

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
       
       if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
    }

   public void ChangeHealth(int amount)
    {

    if (amount < 0)
        {
            if (isInvincible)
                return;
            
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
    
   void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rg2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);

        animator.SetTrigger("Launch");
    }
}