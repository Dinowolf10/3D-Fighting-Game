using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public GameObject player;

    public Transform cameraTransform;

    public float xOffset;

    public float yOffset;

    public float zOffset;

    private void FixedUpdate()
    {
        // Updates camera position and rotation
        FollowPlayer();
    }

    /// <summary>
    /// Follows player position and rotation in the third person
    /// </summary>
    private void FollowPlayer()
    {
        // Updates camera position
        cameraTransform.position = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, player.transform.position.z +zOffset);

        // Updates camera rotation
        cameraTransform.rotation = Quaternion.Euler(player.transform.rotation.eulerAngles);
    }
}
