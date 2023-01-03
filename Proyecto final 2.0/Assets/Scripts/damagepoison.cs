using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damagepoison : MonoBehaviour
{
    public AudioClip collectedClip;

    void OnTriggerEnter2D(Collider2D other)
    {
        playercontroller controller = other.GetComponent<playercontroller >();

        if (controller != null)
        {
            controller.ChangeHealth(-1);
            Destroy(gameObject);

            controller.PlaySound(collectedClip);
        }
    }
}

