using _Codebase.HeroCode;
using UnityEngine;

namespace _Codebase.Logic
{
  public class ChangingRoom : MonoBehaviour
  {
    [SerializeField] private TriggerListener _roomZone;

    private void OnEnable()
    {
      _roomZone.Entered += OnRoomEnter;
      _roomZone.Canceled += OnRoomCancel;
    }

    private void OnDisable()
    {
      _roomZone.Entered -= OnRoomEnter;
      _roomZone.Canceled -= OnRoomCancel;
    }

    private void OnRoomEnter(Collider2D obj)
    {
      if(obj.TryGetComponent(out HeroCustomisation heroCustomisation) == false) return;
      heroCustomisation.StartChangingClothes();
    }

    private void OnRoomCancel(Collider2D obj)
    {
      if(obj.TryGetComponent(out HeroCustomisation heroCustomisation) == false) return;
      heroCustomisation.FinishChangingClothes();
    }
  }
}