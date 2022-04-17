using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    public GameObject gameOverScreen;
    public Canvas canvas;
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);


        if (other.CompareTag("Player"))
        {
            
            GameObject gos = Instantiate(gameOverScreen, canvas.transform.position, Quaternion.identity);
            gos.GetComponent<Transform>().SetParent(canvas.transform);
            // TODO kill the player
            // other.GetComponent<PlayerController>().Kill();
        }

        Destroy(other);

    }
}
