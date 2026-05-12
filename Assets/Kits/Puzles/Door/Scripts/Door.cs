using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] TextMeshPro interactionText;

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
            source.PlayOneShot(source.clip);
            anim.SetTrigger("Opening");
        }
    }

    public void ShowTextMessage(string newText)
    {
        interactionText.SetText(newText);
        interactionText.gameObject.SetActive(true);
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
