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
        /*RaycastHit hit;

        if (Physics.Raycast(enemyTransform.position, player.transform.position, out hit, maxViewDistance))
        {
            if (hit.transform.position == player.transform.position)
            {
                Vector3 moveDirection = new Vector3(player.transform.position.x, 0f, player.transform.position.z).normalized;

                float angle = Mathf.Atan2(moveDirection.x, moveDirection.z);

                float smoothAngle = Mathf.SmoothDampAngle(enemyTransform.eulerAngles.y, angle, ref smoothTurnVelocity, smoothTurnTime);

                enemyTransform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

                enemyTransform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
            }
        }*/

        /*Vector3 moveDirection = player.transform.position - enemyTransform.position;

        float angle = Mathf.Atan2(moveDirection.z, moveDirection.x) * Mathf.Rad2Deg;

        float smoothAngle = Mathf.SmoothDampAngle(enemyTransform.eulerAngles.y, angle, ref smoothTurnVelocity, smoothTurnTime);

        enemyTransform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

        enemyTransform.Translate(Vector3.forward * speed * Time.fixedDeltaTime);
        */

        enemyTransform.LookAt(player.transform.position);

        enemyTransform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
