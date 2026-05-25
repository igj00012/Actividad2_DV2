using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medikit : InteractableItem
{
    [Header("References")]
    [SerializeField] GameplayUIManager instance;

    [Header("Parameters")]
    [SerializeField] float healthRecovery = 0.2f;

    [Header("Audio")]
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
            if (life.Healing(healthRecovery)) { 
                source.PlayOneShot(healthRecoverSFX);
                StartCoroutine(SoundDelay());
                Destroy(gameObject);
            }
        }
    }

    IEnumerator SoundDelay()
    {
        yield return new WaitForSeconds(healthRecoverSFX.length);
    }
}
