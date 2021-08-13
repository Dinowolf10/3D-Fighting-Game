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

        // If enemy is being stealth attacked, wait for 3.5 seconds
        if (!enemyMovement.isEnemySpotted)
        {
            Debug.Log("Enemy has been stunned!");

            // Wait for 3.5 seconds
            yield return new WaitForSeconds(3.5f);

            Debug.Log("Enemy is now dead!");
        }
        // If enemy is taking hit, wait for 1.6 seconds
        else
        {
            Debug.Log("Enemy is taking hit");

            // Wait for 1.6 seconds
            yield return new WaitForSeconds(1.6f);

            Debug.Log("Enemy has taken hit");
        }

        // Set isTakingHit to false
        isTakingHit = false;

        // Set isTakingHit to false in animator
        enemyAnimator.SetBool("isTakingHit", false);

        // Check if enemy is dead
        if (health <= 0 || !enemyMovement.isEnemySpotted)
        {
            Debug.Log("Enemy is dead");

            Destroy(this.gameObject);
        }
    }
}
