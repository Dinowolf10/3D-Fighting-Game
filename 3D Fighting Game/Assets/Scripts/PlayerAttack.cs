using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Collider punchHitbox;

    private bool punching = false;

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
        if (Input.GetButtonDown("Fire1") && !punching)
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
        punching = true;

        Debug.Log("Punch");

        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Disable the punch hitbox
        punchHitbox.enabled = false;

        // Set punching to false
        punching = false;

        Debug.Log("Punch end");
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the object colliding is an enemy
        if (other.tag == "Enemy")
        {
            Debug.Log("Hit Enemy with punch");
        }
    }
}
