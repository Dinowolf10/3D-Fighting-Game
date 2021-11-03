using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;

    public Transform playerTransform;

    public Transform cameraTransform;

    public SphereCollider groundChecker;

    private Vector3 moveDirection;

    private float horizontal;

    private float vertical;

    public float speed;

    public float sprintSpeed;

    public float jumpForce;

    public float slideForce;

    public float smoothTurnTime;

    public float smoothTurnVelocity;

    public bool isWalking = false;

    public bool isJumping = false;

    public bool isSliding = false;

    public bool isGrounded = true;

    public bool isSprinting = false;

    public Animator playerAnimator;

    public PlayerAttack playerAttack;

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        // Gets and stores player input on horizontal and vertical axis
        GetMovementInput();

        // If the player presses jump and the player is currently not jumping
        if (Input.GetButtonDown("Jump") && !isJumping && isGrounded && !playerAttack.isPunching && !playerAttack.isSwingingSword && !playerAttack.isSwitchingSword)
        {
            // Call the jump method
            Jump();
        }
        else if (Input.GetButtonDown("Slide") && !isJumping && !isSliding && isGrounded && !playerAttack.isPunching && !playerAttack.isSwingingSword && !playerAttack.isSwitchingSword)
        {
            StartCoroutine(Slide());
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
    /// Stores player input on horizontal and vertical axis
    /// </summary>
    private void GetMovementInput()
    {
        // Gets input from the horizontal axis
        horizontal = Input.GetAxisRaw("Horizontal");

        // Gets input from the vertical axis
        vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }
    }

    /// <summary>
    /// Moves the player based on user input
    /// </summary>
    private void Move()
    {
        // If the player is moving and not punching
        if ((horizontal != 0 || vertical != 0) && !playerAttack.isPunching && !isSliding && !playerAttack.isSwingingSword && !playerAttack.isSwitchingSword)
        {
            // Creates direction vector using player input
            moveDirection = new Vector3(horizontal, 0f, vertical).normalized;

            // Calculates the angle the player should rotate to
            float angle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg + cameraTransform.eulerAngles.y;

            // Uses the SmoothDampAngle method to calculate a new angle based on the target angle 
            // and the time it takes for the player to look at that angle
            float smoothAngle = Mathf.SmoothDampAngle(playerTransform.eulerAngles.y, angle, ref smoothTurnVelocity, smoothTurnTime);

            // Sets the player's rotation
            playerTransform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

            if (playerAttack.isSwordActive)
            {
                // Moves the player forward
                playerTransform.Translate(Vector3.forward * (speed / 2) * Time.fixedDeltaTime);
            }
            // If the player is sprinting, use the sprint speed
            else if (isSprinting)
            {
                // Moves the player forward
                playerTransform.Translate(Vector3.forward * sprintSpeed * Time.fixedDeltaTime);
            }
            // Otherwise use speed
            else
            {
                // Moves the player forward
                playerTransform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
            }

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
        // If player is sliding, stop sliding
        StopCoroutine(Slide());

        // Set isSliding to false
        isSliding = false;

        // Set isSliding to false in the animator
        playerAnimator.SetBool("isSliding", false);

        // Reset the rigidbody velocity to 0
        rb.velocity = new Vector3(0, 0, 0);

        // Adds force to player on the Y axis
        rb.AddForce(0f, jumpForce, 0f, ForceMode.Impulse);

        // Set isJumping to true
        isJumping = true;

        // Set isGrounded to false
        isGrounded = false;

        // Set is jumping to true in the player animator controller
        playerAnimator.SetBool("isJumping", true);
    }

    /// <summary>
    /// Adds force to player in the forward direction to simulate sliding
    /// </summary>
    private IEnumerator Slide()
    {
        // Adds force in the forward direction
        rb.AddForce(playerTransform.forward * slideForce, ForceMode.Impulse);

        // Set isSliding to true
        isSliding = true;

        // Set isSliding to ture in the animator
        playerAnimator.SetBool("isSliding", true);

        // Wait for 1.38 seconds
        yield return new WaitForSeconds(1.38f);

        // If the player has input in the horizontal and/or vertical axis
        if ((horizontal != 0 || vertical != 0))
        {
            // Set is walking to true in the player animator controller
            playerAnimator.SetBool("isWalking", true);
        }

        // Set isSliding to false
        isSliding = false;

        // Set isSliding to false in the animator
        playerAnimator.SetBool("isSliding", false);
    }

    /// <summary>
    /// Checks and updates the actions the player is performings
    /// </summary>
    private void CheckMovement()
    {
        // Checks to see if the player is jumping
        if (!isGrounded)
        {
            // Set isJumping to true
            isJumping = true;

            // Set is jumping to true in the player animator controller
            playerAnimator.SetBool("isJumping", true);
        }
        else
        {
            // Set isJumping to false
            isJumping = false;

            // Set is jumping to false in the player animator controller
            playerAnimator.SetBool("isJumping", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the player is touching the ground set isGrounded to true
        if (other.tag == "Ground")
        {
            isGrounded = true;
        }
    }
}
