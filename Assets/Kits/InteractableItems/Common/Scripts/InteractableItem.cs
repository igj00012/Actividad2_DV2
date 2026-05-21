using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] TextMeshPro pickUpText;
    [SerializeField] protected Camera cam;

    protected virtual void Update()
    {
        // Rotar texto para que mnire a cámara
        if (pickUpText.gameObject.activeSelf)
        {
            pickUpText.transform.forward = -(cam.transform.position - pickUpText.transform.position).normalized;
        }
    }

    public void HideTextMessage()
    {
        pickUpText.gameObject.SetActive(false);
    }

    public virtual void Interact(PlayerCheckInteraction interactor)
    {
        
    }

    public void ShowTextMessage(string newText)
    {
        pickUpText.SetText(newText);
        pickUpText.gameObject.SetActive(true);
    }
}
