using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playercontroller : MonoBehaviour
{
    public float runSpeed = 2;
    public float jumpSpeed = 3;
    public float doublejumpSpeed = 2.5f;
    private bool canDoubleJump;

    public float timeInvincible = 2.0f;
    private float invincibleTimer;
    private bool isInvincible;


    public GameObject projectilePrefab;
    public Transform ProjectileController;

    public AudioClip throwSound;
    public AudioClip hitSound;
    AudioSource audioSource;
    
    
    public int health { get { return currentHealth; }}
    int currentHealth;
    public int maxHealth = 5;

    public SpriteRenderer SpriteRenderer;
    public Animator animator;
    Vector2 lookDirection = new Vector2(1,0);

    Rigidbody2D rg2d;
    

    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        currentHealth = maxHealth;

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Vector2 move = new Vector2(rg2d.velocity.x, rg2d.velocity.y);
        
        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

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
                        if (rg2d.velocity.y < 0)
                            {
                                animator.SetBool("Fall", true );
                                animator.SetBool("Jump", false);
                            }
                            else if (rg2d.velocity.y > 0);
                            {
                                animator.SetBool("Fall", false );
                                animator.SetBool("Jump", true);
                                animator.SetBool("DoubleJump",true);
                                rg2d.velocity = new Vector2 (rg2d.velocity.x , doublejumpSpeed);
                                canDoubleJump = false;
                            }
                            animator.SetBool("Run", false);
                        
                    }
                }
            }
        }

        if (checkGround.isGrounded==false)
        {
            if (rg2d.velocity.y < 0)
            {
                animator.SetBool("Fall", true );
                animator.SetBool("Jump", false);
            }
            else if (rg2d.velocity.y > 0);
            {
                animator.SetBool("Fall", false );
                animator.SetBool("Jump", true);
            }
            animator.SetBool("Run", false);
                
            
        }



        if (checkGround.isGrounded == true)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("DoubleJump", false);
            animator.SetBool("Fall", false);
        
        }

        

    }
    void FixedUpdate()
    {
        if (rg2d.velocity.y > 0)
        {
            animator.SetBool("Fall", true );
        }
        else if (rg2d.velocity.y < 0);
        {
            animator.SetBool("Fall", false );
        }

        //right and left move

        if(Input.GetKey("d")|| Input.GetKey("right"))
        {
            rg2d.velocity = new Vector2(runSpeed, rg2d.velocity.y);
            SpriteRenderer.flipX = false;
            animator.SetBool("Run", true);
            animator.SetBool("Duck",false);
            animator.SetFloat("Move X", 1);
            animator.SetFloat("Move Y", 0);
            animator.SetBool("Shoot",false);
            animator.SetBool("damage",false);
            

        }

        else if(Input.GetKey("a")|| Input.GetKey("left"))
        {
            rg2d.velocity = new Vector2(-runSpeed, rg2d.velocity.y);
            SpriteRenderer.flipX = true;
            animator.SetBool("Run", true);
            animator.SetBool("Duck",false);
            animator.SetFloat("Move X", -1);
            animator.SetFloat("Move Y", 0);
            animator.SetBool("Shoot",false);
            animator.SetBool("damage",false);
            
        }

        else
        {
            rg2d.velocity = new Vector2 (0,rg2d.velocity.y);
            animator.SetBool("Run", false);
            animator.SetBool("Jump", false);
            animator.SetBool("DobleJump", false);
            animator.SetBool("Duck",false);
            animator.SetBool("Shoot",false);
            animator.SetBool("Fall", true);
            
        }

        //duck move
        if (Input.GetKey("s") || Input.GetKey("down"))
        {
            animator.SetBool("Duck",true);
            animator.SetBool("Shoot",false);
            animator.SetBool("damage",false);
        }

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
       
       if(Input.GetKeyDown(KeyCode.C))
        {
            PlaySound(throwSound);

            Instantiate(projectilePrefab, ProjectileController.position, Quaternion.identity);
            
            
            animator.SetBool("Shoot",true );
            if( rg2d.velocity.x < 0)
            {
                SpriteRenderer.flipX = true;

            }
            
             animator.SetTrigger("Launch");

             
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

            PlaySound(hitSound);
            animator.SetBool("damage",true);
            animator.SetBool("Jump", false);
            animator.SetBool("DoubleJump", false);
            animator.SetBool("Fall", false);
            animator.SetBool("Run", false);
            animator.SetBool("Duck",false);
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }
    

  

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
 