using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform playerTransform;

    public float speed;

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
        // Moves player
        Move();
    }

    /// <summary>
    /// Moves the player based on user input
    /// </summary>
    private void Move()
    {
        // Gets input from the horizontal axis
        float horizontal = Input.GetAxisRaw("Horizontal");

        // Gets input from the vertical axis
        float vertical = Input.GetAxisRaw("Vertical");

        // If there is input on both the horizontal and vertical axis, half the player speed
        if (horizontal != 0 && vertical != 0)
        {
            playerTransform.Translate(new Vector3(horizontal / 2, 0, vertical / 2) * speed * Time.fixedDeltaTime);
        }
        // Otherwise move the player normally
        else
        {
            playerTransform.Translate(new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime);
        }
    }
}
