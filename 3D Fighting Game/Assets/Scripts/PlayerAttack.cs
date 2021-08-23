using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Collider leftHandHitbox;

    public Collider rightHandHitbox;

    public bool isPunching = false;

    public bool hasHitSomething = false;

    public Animator playerAnimator;

    public PlayerMovement playerMovement;

    public GameObject sword;

    public GameObject restingSword;

    public bool isSwordActive = false;

    public bool isSwitchingSword = false;

    public bool isSwingingSword = false;

    // Start is called before the first frame update
    void Start()
    {
        // Disable the punch hitbox
        leftHandHitbox.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        // If player presses left click and is currently not punching
        if (Input.GetButtonDown("Fire1") && !isPunching && !isSwingingSword && !isSwitchingSword && !playerMovement.isJumping && playerMovement.isGrounded)
        {
            // If sword is currently not active, call the punch coroutine
            if (!isSwordActive)
            {
                StartCoroutine(Punch());
            }
            // Otherwise call the light slash coroutine
            else
            {
                StartCoroutine(LightSlash());
            }
        }
        // If the player presses "E"
        else if (Input.GetKeyDown(KeyCode.E) && !isSwingingSword && !isSwitchingSword && playerMovement.isGrounded)
        {
            // If the sword is not active, call the arm sword coroutine
            if (!isSwordActive)
            {
                StartCoroutine(ArmSword());
            }
            // Otherwise start the put put sword away coroutine
            else
            {
                StartCoroutine(PutSwordAway());
            }

            // Set isSwitchingSword to true
            isSwitchingSword = true;
        }
    }

    /// <summary>
    /// Simulates a punch from the player
    /// </summary>
    /// <returns></returns>
    private IEnumerator Punch()
    {
        // Enables the punch hitbox
        leftHandHitbox.enabled = true;

        // Set punching to true
        isPunching = true;

        // Set is punching to true in the player animator controller
        playerAnimator.SetBool("isPunching", true);

        Debug.Log("Punch");

        // Wait for 2 seconds
        yield return new WaitForSeconds(1.3f);

        // Disable the punch hitbox
        leftHandHitbox.enabled = false;

        // Set punching to false
        isPunching = false;

        // Set is punching to false in the player animator controller
        playerAnimator.SetBool("isPunching", false);

        // Sets hasHitSomething to false
        hasHitSomething = false;

        Debug.Log("Punch end");
    }

    /// <summary>
    /// Simulates a light slash from the player
    /// </summary>
    /// <returns></returns>
    private IEnumerator LightSlash()
    {
        isSwingingSword = true;

        Debug.Log("Swinging sword!");

        yield return new WaitForSeconds(1f);

        isSwingingSword = false;

        Debug.Log("Done swinging sword!");
    }

    private IEnumerator ArmSword()
    {
        Debug.Log("Arming sword");

        yield return new WaitForSeconds(1f);

        isSwordActive = true;

        isSwitchingSword = false;

        sword.SetActive(true);

        restingSword.SetActive(false);
    }

    private IEnumerator PutSwordAway()
    {
        Debug.Log("Putting sword away");

        yield return new WaitForSeconds(1f);

        isSwordActive = false;

        isSwitchingSword = false;

        sword.SetActive(false);

        restingSword.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        /*// If the object colliding is an enemy and the player has not hit something during their punch yet, damage the enemy
        if (other.tag == "Enemy" && isPunching && !hasHitSomething)
        {
            Debug.Log("Hit Enemy with punch");

            if (other.GetComponent<EnemyMovement>().isPlayerSpotted)
            {
                // Start the enemy damage coroutine
                StartCoroutine(other.GetComponent<EnemyAttack>().TakeDamage(1));
            }
            else
            {
                // Start the enemy damage coroutine
                StartCoroutine(other.GetComponent<EnemyAttack>().TakeDamage(3));
            }

            // Set hasHitSomething to true
            hasHitSomething = true;
        }*/
    }
}
