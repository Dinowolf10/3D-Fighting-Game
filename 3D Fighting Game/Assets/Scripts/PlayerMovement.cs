using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    public Transform playerTransform;

    public float speed;

    public float jumpForce;

    public bool isWalking = false;

    public bool isJumping = false;

    public Animator playerAnimator;

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        // If the player presses jump and the player is currently not jumping
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            // Call the jump method
            Jump();
        }

        // Checks and updates the current actions the player is performing
        CheckMovement();
    }

    private void FixedUpdate()
    {
        // Moves player
        Move();
    }

    /// <summary>
    /// Moves the player based on user input
    /// </summary>
    private void Move()
    {
        // Gets input from the horizontal axis
        float horizontal = Input.GetAxisRaw("Horizontal");

        // Gets input from the vertical axis
        float vertical = Input.GetAxisRaw("Vertical");

        // If there is input on both the horizontal and vertical axis, half the player speed
        if (horizontal != 0 && vertical != 0)
        {
            // Move player based on input and speed
            playerTransform.Translate(new Vector3(horizontal / 2, 0, vertical / 2) * speed * Time.fixedDeltaTime);

            // Set is walking to true
            isWalking = true;

            // Set is walking to true in the player animator controller
            playerAnimator.SetBool("isWalking", true);
        }
        // If the player is not moving
        else if (horizontal == 0 && vertical == 0)
        {
            // Set is walking to true
            isWalking = false;

            // Set is walking to true in the player animator controller
            playerAnimator.SetBool("isWalking", false);
        }
        // Otherwise move the player normally
        else
        {
            // Move player based on input and speed
            playerTransform.Translate(new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime);

            isWalking = true;

            playerAnimator.SetBool("isWalking", true);
        }
    }

    /// <summary>
    /// Adds force to Y axis the player to simulate a jump
    /// </summary>
    private void Jump()
    {
        // Adds force to player on the Y axis
        rb.AddForce(0f, jumpForce, 0f, ForceMode.Impulse);

        // Set isJumping to true
        isJumping = true;
    }

    /// <summary>
    /// Checks and updates the actions the player is performings
    /// </summary>
    private void CheckMovement()
    {
        // Checks to see if the player is jumping
        if (rb.velocity.y != 0)
        {
            isJumping = true;
        }
        else
        {
            isJumping = false;
        }
    }
}
