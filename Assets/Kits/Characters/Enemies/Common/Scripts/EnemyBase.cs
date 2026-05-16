using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] protected Transform target;

    [Header("Combat parameters")]
    [SerializeField] float damage = 10f;
    [SerializeField] float attackDelay = 2f;

    protected NavMeshAgent agent;
    protected Animator anim;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        TryAttack();
    }

    private void TryAttack()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            print("Hola amego");
            Vector3 directionToTarget = (target.position - transform.position).normalized;
            directionToTarget.y = 0f;
            transform.rotation = Quaternion.LookRotation(directionToTarget);

            anim.SetBool("Walking", false);
            agent.isStopped = true;
            anim.SetBool("Attacking", true);
        }
    }

    private void Attack()
    {        
        Debug.Log("Atacando al player");
    }

    private void EndAttack()
    {
        agent.isStopped = false;
        anim.SetBool("Attacking", false);
    }
}
