using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HEALTH : MonoBehaviour
{
    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        playercontroller controller = other.GetComponent<playercontroller>();

        if (controller != null)
        {
            if(controller.health  < controller.maxHealth)
            {
                controller.ChangeHealth(1);
                Destroy(gameObject);

                controller.PlaySound(collectedClip);

                
            }
            
        }
    }
}