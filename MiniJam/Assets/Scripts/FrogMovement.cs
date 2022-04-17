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
    public Transform groundCheck;
    public float groundCheckRadius = 0.07f;
    public LayerMask ground;

    public bool grounded;
    private int vertices;
    private int movement;
    private Vector3[] previewPositions;
    private Rigidbody2D rb;
    private float gravity;
    public float jumpStrength = 8f; 

    private void Start()
    {
        vertices = segments + 1;
        lr.positionCount = vertices;
        previewPositions = new Vector3[vertices];
        movement = 0;
        rb = GetComponent<Rigidbody2D>();
        gravity = -9.8f * rb.gravityScale;
    }

    private void Update()
    {
        // Test if the player is on the ground for animation-switching purposes
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, ground);

        // Get a normalized vector pointing towards the mouse cursor
        // **This can probably be used for movement as well (add a force to the RB in the direction specified by towardsCursor?)**
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        Vector2 towardsCursor = mousePos2D - new Vector2(transform.position.x, transform.position.y);
        towardsCursor.Normalize();

        // Update the dashed line based on cursor position
        UpdatePreviewLine(towardsCursor);

        // Movement 
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            rb.AddForce(towardsCursor * jumpStrength);
        }
    }

    private void Move(Vector3 direction)
    {
        transform.position += (direction * speed);
        movement++;
    }

    /*
     * Draws a dashed curve in the direction of the mouse cursor.
     * This allows the player to see the path the frog will follow before they jump, to help maximize their limited jump resources. 
     */
    private void UpdatePreviewLine(Vector2 dir)
    {
        float length = 3f;
        Vector2 towardsTargetPos = dir * jumpStrength;

        previewPositions[0] = transform.position;

        // Physics!
        for (int i = 1; i < previewPositions.Length; i++)
        {
            // This is the general equation for an object under constant acceleration applied to both x and y coords
            float t = length * i / vertices; // we discretize t because we're drawing a bunch of short line segments rather than a true curve
            // split the initial velocity into x and y components
            float vx = towardsTargetPos.x;
            float vy = towardsTargetPos.y;
            // calculate the x and y positions at time t
            float x = vx * t;
            float y = (0.5f * gravity * t * t) + (vy * t);
            // add the calculated position to the array
            previewPositions[i] = new Vector3(transform.position.x + x, transform.position.y + y, 0);
        }

        // More efficient than setting each position individually, according to the unity docs
        lr.SetPositions(previewPositions);
    }
}
