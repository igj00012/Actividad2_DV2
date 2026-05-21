using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LockedDoor : InteractableItem
{
    [Header("SFX")]
    [SerializeField] AudioClip openDoor;
    [SerializeField] AudioClip closedDoor;

    Animator anim;
    AudioSource source;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    public override void Interact(PlayerCheckInteraction interactor)
    {
        if (interactor.HasKey())
        {
            interactor.SetKeyPicked(false);
            source.PlayOneShot(openDoor);
            anim.SetTrigger("Opening");
        }
        else
        {
            source.PlayOneShot(closedDoor);
        }
    }

    public void StopAnimator()
    {
        anim.enabled = false;
    }
}
