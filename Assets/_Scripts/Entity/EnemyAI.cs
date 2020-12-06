using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;

    [SerializeField] private GameObject target;
    [SerializeField] private float triggerRadius;

    void Start()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Player");

        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

        agent.destination = target.transform.position;
        agent.isStopped = true;
    }

    void Update()
    {
        if(Vector3.Distance(target.transform.position, transform.position) <= triggerRadius)
        {
            agent.isStopped = false;
            agent.destination = target.transform.position;
        }
        else
        {
            agent.isStopped = true;
        }
    }

    private void OnDrawGizmos()
    {
        // Show trigger size for debugging / balancing
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, triggerRadius);
    }
}
