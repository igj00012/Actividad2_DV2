using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingEnemy : EnemyBase
{
    [Header("Movement")]
    [SerializeField] Transform[] patrolPoints;
    int currentPointIndex = 0;
    [SerializeField] float persecutionDistance = 5f;

    public bool drawGizmos = false;

    protected override void Update()
    {
        CheckPlayer();

        DecideMove();

        base.Update();
    }

    bool playerDetected = false;
    private void CheckPlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < persecutionDistance)
        {
            playerDetected = true;
        }
    }

    private void DecideMove()
    {
        if (playerDetected)
        {
            ChasePlayer();
        }
        else
        {
            Patrolling();
        }
    }

    private void ChasePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) > persecutionDistance) {
            playerDetected = false;
        }
        else
        {
            agent.SetDestination(player.position);
        }
    }

    private void Patrolling()
    {
        if (Vector3.Distance(transform.position, patrolPoints[currentPointIndex].position) > agent.stoppingDistance)
        {
            agent.SetDestination(patrolPoints[currentPointIndex].position);
        }
        else
        {
            if (currentPointIndex < patrolPoints.Length - 1)
            {
                currentPointIndex++;
            }
            else
            {
                currentPointIndex = 0;
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
