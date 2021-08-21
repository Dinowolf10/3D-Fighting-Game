using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftHand : MonoBehaviour
{
    public PlayerAttack playerAttack;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the object colliding is an enemy and the player has not hit something during their punch yet, damage the enemy
        if (other.tag == "Enemy" && playerAttack.isPunching && !playerAttack.hasHitSomething)
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
            playerAttack.hasHitSomething = true;
        }
    }
}
