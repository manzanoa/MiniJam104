using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeReducer : MonoBehaviour
{
    public TrashGenerator trashGenerator;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            trashGenerator.time -= 5;
        }
    }
}
