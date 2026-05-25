using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameplayUIManager instance;

    [Header("Parameters")]
    [SerializeField] float maxHealthPoints;
    public float currentHP; //debug

    void Start()
    {
        currentHP = maxHealthPoints;
    }

    float stunTime;
    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        bool isPlayer = false;
        if (gameObject.GetComponent<PlayerController>() && instance != null)
        {
            instance.ChangeHP(currentHP / maxHealthPoints);
            isPlayer = true;
        }

        if (currentHP <= 0)
        {
            if (isPlayer)
            {
                Debug.Log("He muerto");
                instance.Defeat();
            }
            else
            {
                if (!gameObject.GetComponent<EnemyBase>().IsStunned())
                {
                    Debug.Log(gameObject.name + " aturdido");

                    gameObject.GetComponent<EnemyBase>().Stunned();
                    stunTime = gameObject.GetComponent<EnemyBase>().stunTime;

                    StartCoroutine(StunTimer());
                }
            }
        }
    }

    IEnumerator StunTimer()
    {
        yield return new WaitForSeconds(stunTime);
        currentHP = maxHealthPoints;
    }

    public bool Healing(float healthRecovery)
    {
        bool healing = false;

        if (currentHP < maxHealthPoints)
        {
            currentHP = Mathf.Min(currentHP + healthRecovery, maxHealthPoints);
            instance.ChangeHP(currentHP);
            healing = true;
        }

        return healing;
    }
}
