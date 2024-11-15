using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Property usage example
    public float moveSpeed { get; set; } = 5f;

    public float jumpForce = 10f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;

    // Static variable demonstration
    private static int instanceCount = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Increment static field
        instanceCount++;
        Debug.Log("Instance count: " + instanceCount);
    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Check if grounded using Physics2D.OverlapCircle
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    // Event usage example
    // This event is triggered when the player jumps
    public event System.Action OnJump;

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        OnJump?.Invoke(); // Invoke the OnJump event
    }
}
