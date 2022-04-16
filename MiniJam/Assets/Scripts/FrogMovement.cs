using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrogMovement : MonoBehaviour
{
    public float speed;
    public Text text;
    public LineRenderer lr;
    public int segments;
    private int vertices;
    private int movement;
    private Vector3[] previewPositions;

    private void Start()
    {
        vertices = segments + 1;
        lr.positionCount = vertices;
        previewPositions = new Vector3[vertices];
        movement = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(Vector3.up);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Vector3.down);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector3.right);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector3.left);
        }
        text.text = movement.ToString();

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        UpdatePreviewLine(new Vector3(mousePos.x, mousePos.y, 0));
    }

    private void Move(Vector3 direction)
    {
        transform.position += (direction * speed);
        movement++;
    }

    /*
     * Draws a dashed curve in the direction of the mouse cursor.
     * This allows the player to see the path the frog will follow before they jump, to help maximize their limited jump resources.
     * 
     */
    private void UpdatePreviewLine(Vector3 targetPos)
    {
        float jumpStrength = 1.5f; // TODO Make this reflective of the real physics force being added to the frog
        float length = 3f;

        previewPositions[0] = transform.position;

        // Physics!
        for (int i = 1; i < previewPositions.Length; i++)
        {
            float t = length * i / vertices;
            Vector3 towardsTargetPos = targetPos - transform.position;
            Vector3.Normalize(towardsTargetPos);
            towardsTargetPos = towardsTargetPos * jumpStrength;
            float vx = towardsTargetPos.x;
            float vy = towardsTargetPos.y;
            float x = vx * t;
            float y = (0.5f * -9.8f * t * t) + (vy * t) + 0f; 
            previewPositions[i] = new Vector3(transform.position.x + x, transform.position.y + y, 0);
        }

        lr.SetPositions(previewPositions);
    }
}
