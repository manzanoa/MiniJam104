using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoscrollController : MonoBehaviour
{
    // TODO make this const for safety once we decide on a value
    public float CAMERA_SPEED = 1f; // Camera speed in UU per second
    public BoxCollider2D left, right, top, deathPlaneColl2D;
    public GameObject frog;

    // Used to draw collision boxes in editor
    Color collColor = Color.blue;
    Color deathPlaneColor = Color.red;

    // Update is called once per frame
    void Update()
    {
        // Move the camera up slowly
        transform.position = new Vector3(transform.position.x, transform.position.y + (CAMERA_SPEED * Time.deltaTime), transform.position.z);

        if(frog.GetComponent<FrogMovement>().gameOver)
        {
            CAMERA_SPEED = 0;
        }
    }

    // Draw the OOB box in the editor
    void OnDrawGizmos()
    {
        Gizmos.color = collColor;
        Gizmos.DrawWireCube(left.transform.position, left.size);
        Gizmos.DrawWireCube(right.transform.position, right.size);
        Gizmos.DrawWireCube(top.transform.position, top.size);
        Gizmos.color = deathPlaneColor;
        Gizmos.DrawWireCube(deathPlaneColl2D.transform.position, deathPlaneColl2D.size);
    }

}
