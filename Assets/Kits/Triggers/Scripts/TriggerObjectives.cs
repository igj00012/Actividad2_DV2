using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerObjectives : MonoBehaviour
{
    [SerializeField] GameplayUIManager instance;

    [SerializeField] string newObjective;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            instance.ChangeObjective(newObjective);

            Destroy(gameObject);
        }
    }
}
