using UnityEngine;

namespace _Codebase.Logic
{
  public class Seller : MonoBehaviour, IInteractable
  {
    [SerializeField] private Showable _shopPanel;
    [SerializeField] private Showable _hint;
    
    public void OnInteractionZoneEnter() => _hint.Show();
    public void OnInteractionZoneCancel() => _hint.Hide();
    public void Interact() => _shopPanel.Show();
  }
}