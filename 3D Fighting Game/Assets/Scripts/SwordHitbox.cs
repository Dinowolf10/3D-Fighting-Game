using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordHitbox : MonoBehaviour
{
    public PlayerAttack playerAttack;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && playerAttack.isSwingingSword && !playerAttack.hasHitSomething)
        {
            // Start the enemy damage coroutine
            StartCoroutine(other.GetComponent<EnemyAttack>().TakeDamage(1));

            playerAttack.hasHitSomething = true;
        }
    }
}
