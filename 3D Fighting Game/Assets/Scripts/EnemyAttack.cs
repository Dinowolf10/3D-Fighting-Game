using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private int health;

    private bool isTakingHit = false;

    public Animator enemyAnimator;

    public EnemyMovement enemyMovement;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize health
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Enemy takes damage
    /// </summary>
    public IEnumerator TakeDamage(int damage)
    {
        // Damage the enemy
        health -= damage;

        // Set isTakingHit to true
        isTakingHit = true;

        // Set isTakingHit to true in animator
        enemyAnimator.SetBool("isTakingHit", true);

        // If enemy can see player, wait for 1.6 seconds
        if (enemyMovement.isPlayerSpotted)
        {
            Debug.Log("Enemy is taking hit");

            // Wait for 1.6 seconds
            yield return new WaitForSeconds(1.6f);

            Debug.Log("Enemy has taken hit");
        }
        // If enemy doesn't see the player, they are being stealth attacked so wait for 3.5 seconds
        else
        {
            Debug.Log("Enemy has been stunned!");

            // Wait for 3.5 seconds
            yield return new WaitForSeconds(3.5f);

            Debug.Log("Enemy is now dead!");
        }

        // Set isTakingHit to false
        isTakingHit = false;

        // Set isTakingHit to false in animator
        enemyAnimator.SetBool("isTakingHit", false);

        // Check if enemy is dead
        if (health <= 0)
        {
            Debug.Log("Enemy is dead");

            // Deactivates enemy if they drop to or below 0 health
            this.gameObject.SetActive(false);
        }
    }
}
