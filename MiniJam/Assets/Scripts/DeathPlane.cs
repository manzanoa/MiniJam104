using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    public GameObject gameOverScreen;
    public Canvas canvas;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<FrogMovement>().gameOver = true;
        }
    }
}
