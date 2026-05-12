using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChasing : MonoBehaviour
{
    public event Action OnStartChasing;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnStartChasing?.Invoke();
        }
    }
}
