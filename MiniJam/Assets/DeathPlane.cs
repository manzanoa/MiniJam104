using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);

        if (other.CompareTag("Player"))
        {
            // TODO kill the player
            // other.GetComponent<PlayerController>().Kill();
        }
    }
}
