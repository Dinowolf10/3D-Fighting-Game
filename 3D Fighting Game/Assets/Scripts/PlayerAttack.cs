using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Collider punchHitbox;

    public bool isPunching = false;

    public Animator playerAnimator;

    public PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        // Disable the punch hitbox
        punchHitbox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If player presses left click and is currently not punching
        if (Input.GetButtonDown("Fire1") && !isPunching && !playerMovement.isJumping && playerMovement.isGrounded)
        {
            // Call the punch coroutine
            StartCoroutine(Punch());
        }
    }

    /// <summary>
    /// Simulates a punch from the player
    /// </summary>
    /// <returns></returns>
    private IEnumerator Punch()
    {
        // Enables the punch hitbox
        punchHitbox.enabled = true;

        // Set punching to true
        isPunching = true;

        // Set is punching to true in the player animator controller
        playerAnimator.SetBool("isPunching", true);

        Debug.Log("Punch");

        // Wait for 2 seconds
        yield return new WaitForSeconds(1.3f);

        // Disable the punch hitbox
        punchHitbox.enabled = false;

        // Set punching to false
        isPunching = false;

        // Set is punching to false in the player animator controller
        playerAnimator.SetBool("isPunching", false);

        Debug.Log("Punch end");
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the object colliding is an enemy, damage the enemy
        if (other.tag == "Enemy")
        {
            Debug.Log("Hit Enemy with punch");

            // Start the enemy damage coroutine
            StartCoroutine(other.GetComponent<EnemyAttack>().TakeDamage(1));
        }
    }
}
