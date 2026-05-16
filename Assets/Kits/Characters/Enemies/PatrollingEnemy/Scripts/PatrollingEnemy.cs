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
    [SerializeField] float delayTimeBetweenPoints = 6f;

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
        if (Vector3.Distance(transform.position, target.position) < persecutionDistance)
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
        if (Vector3.Distance(transform.position, target.position) > persecutionDistance)
        {
            playerDetected = false;
        }
        else
        {
            anim.SetBool("Walking", true);
            agent.SetDestination(target.position);
        }
    }

    private void Patrolling()
    {
        if (Vector3.Distance(transform.position, patrolPoints[currentPointIndex].position) > agent.stoppingDistance)
        {
            anim.SetBool("Walking", true);
            agent.SetDestination(patrolPoints[currentPointIndex].position);
        }
        else
        {
            StartCoroutine(DelayWalk());

            currentPointIndex = Random.Range(0, patrolPoints.Length);
        }
    }

    IEnumerator DelayWalk()
    {
        anim.SetBool("Walking", false);
        yield return new WaitForSeconds(delayTimeBetweenPoints);
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
