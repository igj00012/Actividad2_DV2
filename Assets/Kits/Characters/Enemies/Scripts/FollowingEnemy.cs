using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingEnemy : EnemyBase
{
    [SerializeField] float walkDistance = 7f;
    [SerializeField] float lowerSpeed = 1.5f;
    float normalSpeed;

    [SerializeField] TriggerChasing[] triggerChasings;

    private void OnEnable()
    {
        foreach (TriggerChasing triggerChasing in triggerChasings)
        {
            triggerChasing.OnStartChasing += StartChasing;
        }
    }

    private void Start()
    {
        normalSpeed = agent.speed;
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
        foreach (TriggerChasing triggerChasing in triggerChasings)
        {
            triggerChasing.OnStartChasing -= StartChasing;
        }
    }

    private void StartChasing()
    {
        if (!chasing) chasing = true;
    }

    private void Move()
    {
        if (Vector3.Distance(transform.position, player.position) <= walkDistance)
        {
            agent.speed = lowerSpeed;
        }
        else
        {
            agent.speed = normalSpeed;
        }

        agent.SetDestination(player.position);
    }
}
