using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBlock : MonoBehaviour
{
    private Animator animator;
    private Health health;

    [SerializeField] private GameObject itemToSpawn;
    public int amountToSpawn = 5;

    void Start()
    {
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Hit");
        }
    }
    public void FinishHitAnimation()
    {
        CoinExplosionSpawn coins = Instantiate(itemToSpawn, transform.position, transform.rotation).GetComponent<CoinExplosionSpawn>();
        coins.Spawn(amountToSpawn);
        health.Damage(99, Vector3.zero);
    }
}
