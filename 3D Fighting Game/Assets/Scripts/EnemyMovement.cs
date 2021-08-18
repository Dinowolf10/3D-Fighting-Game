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

    public bool isEnemySpotted = false;

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
        if (isEnemySpotted)
        {
            FollowPlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isEnemySpotted = true;

            enemyAnimator.SetBool("isEnemySpotted", true);

            Debug.Log("Player spotted!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isEnemySpotted = false;

            enemyAnimator.SetBool("isEnemySpotted", false);

            Debug.Log("Player lost!");
        }
    }

    private void FollowPlayer()
    {
        Vector3 directionToPlayer = (player.transform.position - enemyTransform.position).normalized;

        Vector3 enemyHeightOffset = new Vector3(enemyTransform.position.x, enemyTransform.position.y + 1, enemyTransform.position.z);

        RaycastHit hit;

        if (Physics.Raycast(enemyHeightOffset, directionToPlayer, out hit, maxViewDistance))
        {
            if (hit.collider != null)
            {
                Debug.DrawLine(enemyHeightOffset, hit.point, Color.green);

                Debug.Log(hit.collider.gameObject.name);

                if (hit.collider.gameObject.tag == "Player")
                {
                    Debug.Log("Can see player!");

                    enemyTransform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z), Vector3.up);

                    enemyTransform.Translate(Vector3.forward * speed * Time.deltaTime);
                }
            }
        }
    }
}
