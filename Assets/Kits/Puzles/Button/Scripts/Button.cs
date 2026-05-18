using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Button : MonoBehaviour, IInteractable
{
    [SerializeField] int id;
    bool isPressed;

    [SerializeField] float scaleXFactor = 2f;

    [SerializeField] Light pointLight;
    [SerializeField] TextMeshPro text;

    public event Action<int> OnPressed;

    public void ShowTextMessage(string newText)
    {
        text.SetText(newText);
        text.gameObject.SetActive(true);
    }

    public void HideTextMessage()
    {
        text.gameObject.SetActive(false);
    }

    public void Interact(PlayerCheckInteraction interactor)
    {
        if (!isPressed)
        {
            isPressed = true;
            transform.localScale = new Vector3(transform.localScale.x * scaleXFactor, transform.localScale.y, transform.localScale.z);
            pointLight.enabled = true;
            OnPressed?.Invoke(id);
        }
    }

    public void ResetButton()
    {
        isPressed = false;
        transform.localScale = new Vector3(transform.localScale.x / scaleXFactor, transform.localScale.y, transform.localScale.z);
        pointLight.enabled = false;
    }

    public int GetId()
    {
        return id;
    }
}
