using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Codebase.Services
{
  public class InputService : MonoBehaviour
  {
    public event Action InteractBtnPressed;

    public bool Enabled { get; private set; }
    
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

    public void Enable() => Enabled = true;
    public void Disable() => Enabled = false;

    public Vector2 GetMovementInput() => Enabled ? _inputActions.Player.Movement.ReadValue<Vector2>() : Vector2.zero;

    private void OnInteractBtnPress(InputAction.CallbackContext obj)
    {
      if(Enabled == false) return;
      InteractBtnPressed?.Invoke();
    }
  }
}