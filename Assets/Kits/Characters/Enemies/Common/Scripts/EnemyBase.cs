using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    protected enum EnemyState
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Stun
    }

    [SerializeField] protected EnemyState currentState; //debug

    [Header("Target")]
    [SerializeField] protected Transform target;

    [Header("Combat parameters")]
    [SerializeField] float damage = 0.1f;
    [SerializeField] float attackDelay = 2f;
    float lastAttackTime = 0f;
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected float attackRadius = 3f;
    [SerializeField] public float stunTime = 5f;

    protected NavMeshAgent agent;
    protected Animator anim;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if (currentState != EnemyState.Stun)
        {

            switch (currentState)
            {
                case EnemyState.Idle:
                    break;
                case EnemyState.Patrol:
                    PatrolUpdate();
                    break;
                case EnemyState.Chase:
                    ChaseUpdate();
                    break;
                case EnemyState.Attack:
                    AttackUpdate();
                    break;
            }

            if (isAttacking)
            {
                anim.SetFloat("Velocity", 0);
            }
        }
    }

    protected virtual void PatrolUpdate() { }
    protected virtual void ChaseUpdate() { }
    protected void AttackUpdate()
    {
        if (isAttacking) return;

        if (Vector3.Distance(transform.position, target.position) > agent.stoppingDistance)
        {
            anim.ResetTrigger("Attacking");
            ChangeState(EnemyState.Chase);
        }
        else
        {
            TryAttack();
        }
    }

    protected void ChangeState(EnemyState enemyState)
    {
        currentState = enemyState;

        switch (currentState)
        {
            case EnemyState.Idle:
                agent.isStopped = true;
                break;
            case EnemyState.Patrol:
                agent.isStopped = false;
                break;
            case EnemyState.Chase:
                agent.isStopped = false;
                break;
            case EnemyState.Attack:
                agent.isStopped = true;
                break;
            case EnemyState.Stun:
                agent.isStopped = true;
                break;
        }
    }

    protected bool isAttacking = false;
    protected void TryAttack()
    {
        Debug.Log(Vector3.Distance(transform.position, target.position));

        if (Vector3.Distance(transform.position, target.position) <= agent.stoppingDistance 
            && Time.time >= lastAttackTime + attackDelay && !isAttacking)
        {
            isAttacking = true;
            lastAttackTime = Time.time;

            anim.SetFloat("Velocity", 0);
            agent.isStopped = true;

            Vector3 directionToTarget = (target.position - transform.position).normalized;
            directionToTarget.y = 0f;
            transform.rotation = Quaternion.LookRotation(directionToTarget);

            anim.SetTrigger("Attacking");
        }
    }

    private void Attack()
    {   
        Debug.Log("Enemigo atacando");

        Collider[] colliders = Physics.OverlapSphere(attackPoint.position, attackRadius);
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                Life playerLife = col.gameObject.GetComponent<Life>();
                if (playerLife != null)
                {
                    playerLife.TakeDamage(damage);
                }
            }
        }
    }

    private void EndAttack()
    {
        isAttacking = false;

        if (currentState != EnemyState.Attack)
        {
            agent.isStopped = false;
        }
    }

    public void Stunned()
    {
        ChangeState(EnemyState.Stun);

        StopAllCoroutines();

        anim.SetTrigger("Stunning");
        StartCoroutine(StunnedCoroutine());
    }

    public bool IsStunned()
    {
        return currentState == EnemyState.Stun;
    }

    IEnumerator StunnedCoroutine()
    {
        anim.SetBool("Stun", true);
        yield return new WaitForSeconds(stunTime);
        anim.SetBool("Stun", false);
    }

    protected virtual void EndStun() { }
}
