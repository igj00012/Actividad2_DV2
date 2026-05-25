using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : InteractableItem
{
    [Header("References")]
    [SerializeField] AudioClip pickBatterySFX;
    [SerializeField] FlashLight flashLight;

    [Header("Parameters")]
    [SerializeField] float recoveryPercentage = 20;

    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public override void Interact(PlayerCheckInteraction interactor)
    {
        if (flashLight.pickedUp)
        {
            if (flashLight.currentBattery < flashLight.maxBattery)
            {
                flashLight.RecoverBattery(recoveryPercentage);
                source.PlayOneShot(pickBatterySFX);
                StartCoroutine(SoundDelay());
                Destroy(gameObject);
            }
        }
    }

    float offset = 5;
    IEnumerator SoundDelay()
    {
        yield return new WaitForSeconds(pickBatterySFX.length + offset);
    }
}
