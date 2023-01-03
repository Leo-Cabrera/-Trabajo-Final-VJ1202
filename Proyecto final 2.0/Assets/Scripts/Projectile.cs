using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rg2d;
    public float speed;

    void Start()
    {
        rg2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rg2d.velocity = new Vector2 (+ speed, 0 );
        Destroy(gameObject, 2.5f);
    
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        enemy e = other.collider.GetComponent<enemy>();
        if (e != null)
        {
            e.ByeRobot();
        }
    
        Destroy(gameObject);
    }
}