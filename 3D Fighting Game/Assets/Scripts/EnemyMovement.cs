using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Rigidbody rb;

    public Transform enemyTransform;

    public Animator enemyAnimator;

    public float speed;

    public float maxViewDistance;

    public bool isPlayerInRange = false;

    public bool isPlayerSpotted = false;

    public float smoothTurnTime;

    public float smoothTurnVelocity;

    public GameObject player;

    // Start is called before the first frame update
    private void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {

    }

    private void FixedUpdate()
    {
        // If player is in range, call the FollowPlayer method
        if (isPlayerInRange)
        {
            FollowPlayer();
        }
    }

    /// <summary>
    /// Shoots a raycast to check if the enemy can actually see the player,
    /// and if the enemy can see the player, it follows the player
    /// </summary>
    private void FollowPlayer()
    {
        // Direction vector from this enemy to the player
        Vector3 directionToPlayer = (player.transform.position - enemyTransform.position).normalized;

        // Adds a small offset in the y direction so the raycast is not shot in the ground
        Vector3 enemyHeightOffset = new Vector3(enemyTransform.position.x, enemyTransform.position.y + 1, enemyTransform.position.z);

        // Will store the collider hit
        RaycastHit hit;

        // Shoots a raycast from this enemy towards the direction of the player.
        // Shoots out at a set max distance and stores the collider hit in the "hit" variable
        if (Physics.Raycast(enemyHeightOffset, directionToPlayer, out hit, maxViewDistance))
        {
            // If there is a collider hit
            if (hit.collider != null)
            {
                // Draw the raycast as a green debug line
                Debug.DrawLine(enemyHeightOffset, hit.point, Color.green);

                Debug.Log(hit.collider.gameObject.name);

                // If the collider hit belongs to the player
                if (hit.collider.gameObject.tag == "Player")
                {
                    // Set isPlayerSpotted to true
                    isPlayerSpotted = true;

                    // Set isPlayerSpotted to true in the animator
                    enemyAnimator.SetBool("isPlayerSpotted", true);

                    Debug.Log("Can see player!");

                    // Rotate this enemy to look at the player position
                    enemyTransform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z), Vector3.up);

                    // Move this enemy towards the player
                    enemyTransform.Translate(Vector3.forward * speed * Time.deltaTime);
                }
                // Otherwise this enemy cannot see the player
                else
                {
                    // Set isPlayerSpotted to false
                    isPlayerSpotted = false;

                    // Set isPlayerSpotted to false in the animator
                    enemyAnimator.SetBool("isPlayerSpotted", false);

                    Debug.Log("Can't see player!");
                }
            }
        }
    }
}
