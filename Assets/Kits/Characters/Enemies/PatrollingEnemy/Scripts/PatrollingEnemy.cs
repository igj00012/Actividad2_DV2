using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


// Añadir deteccion por visión
public class PatrollingEnemy : EnemyBase
{
    [Header("Movement")]
    [SerializeField] Transform[] patrolPoints;
    int currentPointIndex;
    [SerializeField] float persecutionDistance = 5f;
    [SerializeField] float delayTimeBetweenPoints = 6f;

    public bool drawGizmos = false;

    private void Start()
    {
        currentPointIndex = Random.Range(0, patrolPoints.Length);
        ChangeState(EnemyState.Patrol);
    }

    protected override void Update()
    {
        CheckPlayer();

        base.Update();
    }

    bool playerDetected = false;
    private void CheckPlayer()
    {
        if (Vector3.Distance(transform.position, target.position) < persecutionDistance)
        {
            playerDetected = true;
        }
        else
        {
            playerDetected = false;
        }
    }

    bool isWaiting = false;
    protected override void PatrolUpdate()
    {
        if (playerDetected)
        {
            ChangeState(EnemyState.Chase);
        }
        else
        {
            if (!isWaiting)
            {
                if (Vector3.Distance(transform.position, patrolPoints[currentPointIndex].position) > agent.stoppingDistance)
                {
                    anim.SetFloat("Velocity", 1);
                    agent.SetDestination(patrolPoints[currentPointIndex].position);
                }
                else
                {
                    StartCoroutine(DelayWalk());
                }
            }
        }
    }

    IEnumerator DelayWalk()
    {
        isWaiting = true;

        ChangeState(EnemyState.Idle);

        anim.SetFloat("Velocity", 0);

        yield return new WaitForSeconds(delayTimeBetweenPoints);

        currentPointIndex = Random.Range(0, patrolPoints.Length);
        isWaiting = false;
        
        ChangeState(EnemyState.Patrol);
    }

    protected override void ChaseUpdate()
    {
        if (!playerDetected)
        {
            ChangeState(EnemyState.Patrol);
        }
        else
        {
            if (Vector3.Distance(transform.position, target.position) <= agent.stoppingDistance)
            {
                ChangeState(EnemyState.Attack);
            }
            else
            {
                anim.SetFloat("Velocity", 1);
                agent.SetDestination(target.position);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawSphere(transform.position, persecutionDistance);
        }
    }
}
