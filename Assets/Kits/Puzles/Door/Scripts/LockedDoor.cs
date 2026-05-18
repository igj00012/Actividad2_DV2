using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LockedDoor : MonoBehaviour, IInteractable
{
    [SerializeField] TextMeshPro interactionText;

    [SerializeField] AudioClip openDoor;
    [SerializeField] AudioClip closedDoor;

    Animator anim;
    AudioSource source;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    public void Interact(PlayerCheckInteraction interactor)
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

    public void ShowTextMessage(string newText)
    {
        if (anim.enabled)
        {
            interactionText.SetText(newText);
            interactionText.gameObject.SetActive(true);
        }
    }

    public void HideTextMessage()
    {
        interactionText.gameObject.SetActive(false);
    }

    public void StopAnimator()
    {
        anim.enabled = false;
    }
}
