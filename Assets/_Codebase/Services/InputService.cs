using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Codebase.Services
{
  public class InputService : MonoBehaviour
  {
    public event Action InteractBtnPressed;
    
    private InputActions _inputActions;

    private void Awake() => _inputActions = new InputActions();

    private void OnEnable()
    {
      _inputActions.Enable();
      _inputActions.Player.Interact.performed += OnInteractBtnPress;
    }

    private void OnDisable()
    {
      _inputActions.Disable();
      _inputActions.Player.Interact.performed -= OnInteractBtnPress;
    }

    public Vector2 GetMovementInput() => _inputActions.Player.Movement.ReadValue<Vector2>();

    private void OnInteractBtnPress(InputAction.CallbackContext obj) => InteractBtnPressed?.Invoke();
  }
}