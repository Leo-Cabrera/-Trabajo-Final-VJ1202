using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePoison : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        playercontroller controller = other.GetComponent<playercontroller >();

        if (controller != null)
        {
            controller.ChangeHealth(-1);
            Destroy(gameObject);
        }
    }
}

