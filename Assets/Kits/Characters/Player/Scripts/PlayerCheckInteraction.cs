using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCheckInteraction : MonoBehaviour
{
    [SerializeField] InputActionReference interactAction;

    IInteractable currentInteractableObject;

    bool isKeyPicked = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hola " + other.name);

        if (other.TryGetComponent(out IInteractable interactable) && !other.GetComponent<PlayerController>())
        {
            Debug.Log("Te elegí " + other.name);
            currentInteractableObject = interactable;
            ShowTextMessage();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Adiós " + other.name);

        if (other.TryGetComponent(out IInteractable interactable))
        {
            if (interactable == currentInteractableObject)
            {
                Debug.Log("Ya no podemos interactuar " + other.name);
                HideTextMessage();
                currentInteractableObject = null;
            }
        }
    }

    public void OnInteract()
    {
        if (currentInteractableObject != null)
        {
            currentInteractableObject.Interact(this);
        }
    }

    void ShowTextMessage()
    {
        if (currentInteractableObject != null)
        {
            currentInteractableObject.ShowTextMessage("[" + interactAction.action.GetBindingDisplayString() + "]");
        }
    }

    void HideTextMessage()
    {
        if (currentInteractableObject != null)
        {
            currentInteractableObject.HideTextMessage();
        }
    }

    public void SetKeyPicked(bool keyPicked)
    {
        isKeyPicked = keyPicked;
    }

    public bool HasKey()
    {
        return isKeyPicked;
    }
}
