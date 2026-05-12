using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class KeyHandler : MonoBehaviour, IInteractable
{
    [SerializeField] TextMeshPro text;

    AudioSource source;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void Interact(PlayerCheckInteraction interactor)
    {
        source.PlayOneShot(source.clip);
        interactor.SetKeyPicked(true);

        StartCoroutine(SoundDelay());
        Destroy(gameObject);
    }

    IEnumerator SoundDelay()
    {
        yield return new WaitForSeconds(source.clip.length);
    }

    public void ShowTextMessage(string newText)
    {
        text.SetText(newText);
        text.gameObject.SetActive(true);
    }

    public void HideTextMessage()
    {
        text.gameObject.SetActive(false);
    }
}
