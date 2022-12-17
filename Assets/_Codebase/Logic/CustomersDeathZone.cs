using System;
using _Codebase.CustomerCode;
using UnityEngine;

namespace _Codebase.Logic
{
  public class CustomersDeathZone : MonoBehaviour
  {
    [SerializeField] private TriggerListener _zone;

    private void OnEnable() => _zone.Entered += OnDeathZoneEnter;
    private void OnDisable() => _zone.Entered -= OnDeathZoneEnter;

    private void OnDeathZoneEnter(Collider2D obj)
    {
      if(obj.TryGetComponent(out Customer customer) == false || customer.CanDie == false) return;
      customer.Destroy();
    }
  }
}