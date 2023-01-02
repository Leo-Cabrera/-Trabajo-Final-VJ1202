using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
   public float speed = 3.0f;
    public bool vertical;
    public float changeTime = 3.0f;

    Rigidbody2D rigidbody2D;
    float timer;
    public int direction = 1;
    bool destroyed = false;
    
    Animator animator;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(destroyed)
        {
            return;
        }
        
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
        
        
        Vector2 position = rigidbody2D.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
 
        rigidbody2D.MovePosition(position);
    }

    public void ChangeDirection(int amount)
    {

        if (amount < 0)
        {
              direction = direction * amount;
        }
    }
    
    
    void OnCollisionEnter2D(Collision2D other)
    {
        playercontroller player = other.gameObject.GetComponent<playercontroller >();

        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }


    public void Fix()
    {
        destroyed = true;
        rigidbody2D.simulated = false;
    }
        
}
