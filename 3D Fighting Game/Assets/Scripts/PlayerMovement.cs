using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    public Transform playerTransform;

    public Transform cameraTransform;

    public float speed;

    public float jumpForce;

    public float smoothTurnTime;

    public float smoothTurnVelocity;

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

        if (horizontal != 0 || vertical != 0)
        {
            // Creates direction vector using player input
            Vector3 moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

            // Calculates the angle the player should rotate to
            float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

            // Uses the SmoothDampAngle method to calculate a new angle based on the target angle 
            // and the time it takes for the player to look at that angle
            float smoothAngle = Mathf.SmoothDampAngle(playerTransform.eulerAngles.y, angle, ref smoothTurnVelocity, smoothTurnTime);

            // Sets the player's rotation
            playerTransform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            // Moves the player forward
            playerTransform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);

            // Set is walking to true
            isWalking = true;

            // Set is walking to true in the player animator controller
            playerAnimator.SetBool("isWalking", true);
        }
        // If the player is not moving
        else
        {
            // Set is walking to false
            isWalking = false;

            // Set is walking to false in the player animator controller
            playerAnimator.SetBool("isWalking", false);
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
