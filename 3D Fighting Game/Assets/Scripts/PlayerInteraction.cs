using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    /*public GameObject sword;

    public bool inSwordPickupRange = false;

    // Start is called before the first frame update
    void Start()
    {
        sword.SetActive(false);
    }

    /// <summary>
    /// Simulates the player picking up a sword they found and interact with
    /// </summary>
    public void PickSwordUp(GameObject swordToPickup)
    {
        swordToPickup.transform.parent.gameObject.SetActive(false);

        sword.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the player is in range of the pick up sword, set inSwordPickupRange to true
        if (other.tag == "SwordPickup")
        {
            inSwordPickupRange = true;

            Debug.Log("Player in pickup range!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If the player leaves the range of the pick up sword, set inSwordPickupRange to false
        if (other.tag == "SwordPickup")
        {
            inSwordPickupRange = false;

            Debug.Log("Player out of pickup range!");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("Interact") && inSwordPickupRange)
        {
            PickSwordUp(other.gameObject);

            Debug.Log("Player is picking up sword!");
        }
    }*/
}
