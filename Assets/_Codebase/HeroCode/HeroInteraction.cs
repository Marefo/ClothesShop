using System;
using System.Collections.Generic;
using _Codebase.Extensions;
using _CodeBase.Logging;
using _Codebase.Logic;
using _Codebase.Services;
using UnityEngine;

namespace _Codebase.HeroCode
{
  public class HeroInteraction : MonoBehaviour
  {
    [SerializeField] private TriggerListener _interactionZone;
    [SerializeField] private InputService _inputService;

    private List<IInteractable> _interactablesInZone = new List<IInteractable>();

    private void OnEnable()
    {
      _interactionZone.Entered += OnInteractionZoneEnter;
      _interactionZone.Canceled += OnInteractionZoneCancel;
      _inputService.InteractBtnPressed += OnInteractionBtnPress;
    }

    private void OnDisable()
    {
      _interactionZone.Entered -= OnInteractionZoneEnter;
      _interactionZone.Canceled -= OnInteractionZoneCancel;
      _inputService.InteractBtnPressed -= OnInteractionBtnPress;
    }

    private void OnInteractionZoneEnter(Collider2D obj)
    {
      if(obj.TryGetComponent(out IInteractable interactable) == false) return;
      interactable.OnInteractionZoneEnter();
      _interactablesInZone.AddIfNotExists(interactable);
    }

    private void OnInteractionZoneCancel(Collider2D obj)
    {
      if(obj.TryGetComponent(out IInteractable interactable) == false) return;
      interactable.OnInteractionZoneCancel();
      _interactablesInZone.Remove(interactable);
    }

    private void OnInteractionBtnPress() => _interactablesInZone.ForEach(interactable => interactable.Interact());
  }
}