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
    public float groundCheckSideLength;
    public LayerMask ground;
    private Vector2 groundCheckBox;
    private bool lastGrounded;
    private Animator anim;

    public Canvas canvas;
    public GameObject gameOverScreen;
    public bool gameOver = false;

    public bool grounded;
    private int vertices;
    public int movement;
    private Vector3[] previewPositions;
    private Rigidbody2D rb;
    private float gravity;
    private Vector2 towardsCursor;
    private bool jump;
    public float jumpStrength = 8f; // The force to apply to the frog when it jumps

    private void Start()
    {
        vertices = segments + 1;
        lr.positionCount = vertices;
        previewPositions = new Vector3[vertices];
        movement = 3;
        text.text = movement.ToString();
        rb = GetComponent<Rigidbody2D>();
        gravity = -9.8f * rb.gravityScale;
        jump = false;
        groundCheckBox = new Vector2(groundCheckSideLength, 0.2f);
        lastGrounded = false;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.ResetTrigger("Land");

        // Test if the player is on the ground for animation-switching purposes
        grounded = Physics2D.OverlapBox(groundCheck.position, groundCheckBox, 0, ground) != null;

        // Get a normalized vector pointing towards the mouse cursor
        // **This can probably be used for movement as well (add a force to the RB in the direction specified by towardsCursor?)**
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, transform.position.y + 4.5f);
        towardsCursor = mousePos2D - new Vector2(transform.position.x, transform.position.y);
        towardsCursor.Normalize();
        towardsCursor = new Vector2(towardsCursor.x, Mathf.Abs(towardsCursor.y));

        // Update the dashed line based on cursor position
        UpdatePreviewLine(towardsCursor);

        // Movement 
        // since it's physics based, we handle the actual jump in FixedUpdate
        if (grounded && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(movement > 0)
            {
                Debug.Log("jumped!");
                lr.enabled = false;

                // Determine facing direction
                float angle = Mathf.Atan2(Mathf.Abs(towardsCursor.y), towardsCursor.x) * Mathf.Rad2Deg;
                Debug.Log(angle);
                if (angle > 100) // left
                {
                    anim.SetBool("IsForward", false);
                    anim.SetBool("IsLeft", true);
                    anim.SetBool("IsRight", false);
                }
                else if (angle < 80) // right
                {
                    anim.SetBool("IsForward", false);
                    anim.SetBool("IsLeft", false);
                    anim.SetBool("IsRight", true);
                }
                else // forward
                {
                    anim.SetBool("IsForward", true);
                    anim.SetBool("IsLeft", false);
                    anim.SetBool("IsRight", false);
                }

                anim.SetTrigger("Jump");
                movement--;
            }
            else
            {
                gameOver = true;
            }
            
        }

        // Check if we just landed and update anim
        if (!lastGrounded && grounded)
        {
            Debug.Log("landed!");
            anim.SetTrigger("Land");
            lr.enabled = true;
        }

        if(gameOver)
        {
            
            GameObject gos = Instantiate(gameOverScreen, canvas.transform.position, Quaternion.identity);
            gos.GetComponent<Transform>().SetParent(canvas.transform);
        }

        lastGrounded = grounded;
        text.text = movement.ToString();
    }

    void FixedUpdate()
    {
        // front: frame 12
        // left: 18
        // right: 18

        // Jump when there are moves left
        if (jump)
        {
            jump = false;
            rb.AddForce(towardsCursor * jumpStrength, ForceMode2D.Impulse);
            
        }

    }

    // Listens for events from the animation.
    public void Jump()
    {
        jump = true;
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
            // x = 1/2at^2 + v0t + x0
            // x0 is always 0, since we're starting at the frog's location
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
