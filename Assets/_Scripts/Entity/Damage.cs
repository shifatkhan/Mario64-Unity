using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Flexible damaging system that can be attached to any game object.
/// 
/// @author ShifatKhan
/// </summary>
public class Damage : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float force = 5f;

    [SerializeField] private bool damagePlayer = false;

    [SerializeField] private bool explode = false;

    private void OnTriggerEnter(Collider other)
    {
        // ENEMY attacking PLAYER
        if (damagePlayer && other.CompareTag("Player"))
        {
            // Calculate knock back direction.
            Vector3 knockBack = other.transform.position - transform.position;
            knockBack = knockBack.normalized * force;

            other.GetComponent<Health>().Damage(damage, knockBack);

            if (explode)
            {
                GetComponent<Health>().Damage(999, Vector3.zero);
            }
        }

        // PLAYER attacking ENEMY
        else if (other.CompareTag("Enemy") || other.CompareTag("Crate"))
        {
            // Calculate knock back direction.
            Vector3 knockBack = other.transform.position - transform.position;
            knockBack = knockBack.normalized * force;

            other.GetComponent<Health>().Damage(damage, knockBack);
        }
    }
}
