using System;
using _Codebase.HeroCode;
using UnityEngine;

namespace _Codebase.Logic
{
  public class FittingRoom : MonoBehaviour
  {
    [SerializeField] private GameObject _fittingRoomUI;
    [SerializeField] private TriggerListener _roomZone;

    private void OnEnable() => _roomZone.Entered += OnRoomEnter;
    private void OnDisable() => _roomZone.Entered -= OnRoomEnter;

    private void OnRoomEnter(Collider2D obj)
    {
      if(obj.TryGetComponent(out Hero hero) == false) return;
      
    }
  }
}