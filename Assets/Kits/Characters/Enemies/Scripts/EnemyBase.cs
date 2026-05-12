using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] protected Transform player;
    [SerializeField] float damage = 10f;

    protected NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        TryAttack();
    }

    private void TryAttack()
    {
        if (Vector3.Distance(transform.position, player.position) <= agent.stoppingDistance)
        {
            Attack();
        }
    }

    protected void Attack()
    {
        //animación
        //comprobar golpe
        //cambiar vida player
        Debug.Log("Atacando al player");
    }

}
