using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Button : InteractableItem
{
    [Header("Parameters")]
    [SerializeField] int id;
    bool isPressed;
    [SerializeField] float scaleXFactor = 2f;

    [Header("References")]
    [SerializeField] Light pointLight;

    public event Action<int> OnPressed;

    public override void Interact(PlayerCheckInteraction interactor)
    {
        // Los botones solo se pueden pulsar una vez
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
