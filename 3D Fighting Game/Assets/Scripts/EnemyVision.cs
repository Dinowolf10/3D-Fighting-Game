using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    public EnemyMovement enemyMovement;

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
        // If the player is within the vision collider
        if (other.tag == "Player")
        {
            // Set isPlayerInRange to true
            enemyMovement.isPlayerInRange = true;

            Debug.Log("Player in range!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If the player leaves the vision collider
        if (other.tag == "Player")
        {
            // Set isPlayerInRange to false
            enemyMovement.isPlayerInRange = false;

            // Set isPlayerSpotted to false
            enemyMovement.isPlayerSpotted = false;

            // Set isPlayerSpotted to false in the animator
            enemyMovement.enemyAnimator.SetBool("isPlayerSpotted", false);

            Debug.Log("Player out of range!");
        }
    }
}
