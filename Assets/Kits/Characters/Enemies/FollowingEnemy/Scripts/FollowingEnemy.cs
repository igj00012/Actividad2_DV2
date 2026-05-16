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

        if (triggerChasings.Length == 0)
        {
            StartChasing();
        }
    }

    bool chasing = false;
    protected override void Update()
    {
        if (chasing)
        {
            Move();
            base.Update();
        }
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
        if (!chasing) chasing = true;
    }

    private void Move()
    {
        if (agent.remainingDistance <= walkDistance)
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
