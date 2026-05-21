using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    [SerializeField] float maxHealthPoints;
    public float currentHP; //debug

    [SerializeField] Image healthBar;

    void Start()
    {
        currentHP = maxHealthPoints;
    }

    float stunTime;
    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        bool isPlayer = false;
        if (healthBar != null && gameObject.GetComponent<PlayerController>())
        {
            healthBar.fillAmount = currentHP / maxHealthPoints;
            isPlayer = true;
        }

        if (currentHP <= 0)
        {
            if (isPlayer)
            {
                Debug.Log("He muerto");
                // Pantalla de derrota
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
}
