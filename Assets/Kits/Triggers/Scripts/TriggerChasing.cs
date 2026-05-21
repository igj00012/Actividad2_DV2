using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChasing : MonoBehaviour
{
    public event Action OnStartChasing;

    // Trigger que activa el movimeinto del enemigo que lo persigue
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnStartChasing?.Invoke();
        }
    }
}
