using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medikit : InteractableItem
{
    [SerializeField] float healthRecovery = 0.2f;

    [SerializeField] AudioClip healthRecoverSFX;

    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public override void Interact(PlayerCheckInteraction interactor)
    {
        Life life = interactor.GetComponentInParent<Life>();
        if (life != null)
        {
            if (life.currentHP < life.maxHealthPoints)
            {
                life.currentHP = Mathf.Min(life.currentHP + healthRecovery, life.maxHealthPoints);
                source.PlayOneShot(healthRecoverSFX);
                Destroy(gameObject);
            }
        }
    }
}
