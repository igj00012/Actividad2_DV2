using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingEnemy : EnemyBase
{
    [Header("Movement")]
    [SerializeField] float walkDistance = 7f;
    [SerializeField] float agentMinSpeed = 1.5f;
    float agentMaxSpeed;

    [Header("Triggers")]
    [SerializeField] TriggerChasing[] triggerChasings;

    private void OnEnable()
    {
        if (triggerChasings.Length > 0)
        {
            foreach (TriggerChasing triggerChasing in triggerChasings)
            {
                triggerChasing.OnStartChasing += StartChasing;
            }
        }
    }

    private void Start()
    {
        agentMaxSpeed = agent.speed;

        ChangeState(EnemyState.Idle);

        if (triggerChasings.Length == 0)
        {
            StartChasing();
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnDisable()
    {
        if (triggerChasings.Length > 0)
        {
            foreach (TriggerChasing triggerChasing in triggerChasings)
            {
                triggerChasing.OnStartChasing -= StartChasing;
            }
        }
    }

    private void StartChasing()
    {
        ChangeState(EnemyState.Chase);
    }

    protected override void ChaseUpdate()
    {
        if (Vector3.Distance(transform.position, target.position) <= agent.stoppingDistance)
        {
            ChangeState(EnemyState.Attack);
        }
        else
        {
            Move();
        }
    }

    private void Move()
    {
        if (Vector3.Distance(transform.position, target.position) <= walkDistance)
        {
            agent.speed = agentMinSpeed;
        }
        else
        {
            agent.speed = agentMaxSpeed;
        }

        anim.SetFloat("Velocity", agent.speed / agentMaxSpeed);

        agent.SetDestination(target.position);
    }
}
