using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class KeyHandler : InteractableItem
{
    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public override void Interact(PlayerCheckInteraction interactor)
    {
        source.PlayOneShot(source.clip);
        interactor.SetKeyPicked(true);

        StartCoroutine(SoundDelay());

        Destroy(gameObject);
    }


    float offset = 4;
    IEnumerator SoundDelay()
    {
        yield return new WaitForSeconds(source.clip.length + offset);
    }
}
