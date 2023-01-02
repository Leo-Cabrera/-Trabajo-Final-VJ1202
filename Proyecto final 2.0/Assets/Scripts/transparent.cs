using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transparent : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        enemy controller = other.GetComponent<enemy >();

        if (controller != null)
        {
            
            controller.ChangeDirection(-1);
        }
    }
}
