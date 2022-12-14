using System;
using UnityEngine;

namespace _Codebase.Services
{
  public class InputService : MonoBehaviour
  {
    private InputActions _inputActions;

    private void Awake() => _inputActions = new InputActions();

    private void OnEnable() => _inputActions.Enable();
    private void OnDisable() => _inputActions.Disable();

    public Vector2 GetMovementInput() => _inputActions.Player.Movement.ReadValue<Vector2>();
  }
}