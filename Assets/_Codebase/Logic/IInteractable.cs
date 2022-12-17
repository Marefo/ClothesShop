namespace _Codebase.Logic
{
  public interface IInteractable
  {
    void OnInteractionZoneEnter();
    void OnInteractionZoneCancel();
    void Interact();
  }
}