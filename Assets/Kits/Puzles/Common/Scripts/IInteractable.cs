public interface IInteractable
{
    void Interact(PlayerCheckInteraction interactor);
    void ShowTextMessage(string newText);
    void HideTextMessage();
}
