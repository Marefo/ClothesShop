using UnityEngine;

namespace _Codebase.UnitsCode
{
  public class UnitAnimator : MonoBehaviour
  {
    [SerializeField] private Animator _animator;
    
    private readonly int _isMoving = Animator.StringToHash("IsMoving");
    private readonly int _lastMovementX = Animator.StringToHash("LastMovementX");
    private readonly int _lastMovementY = Animator.StringToHash("LastMovementY");
    private readonly int _movementX = Animator.StringToHash("MovementX");
    private readonly int _movementY = Animator.StringToHash("MovementY");

    public void ChangeMovementState(bool to) => _animator.SetBool(_isMoving, to);
    public void UpdateLastMovementX(float value) => _animator.SetFloat(_lastMovementX, value);
    public void UpdateLastMovementY(float value) => _animator.SetFloat(_lastMovementY, value);
    public void UpdateMovementX(float value) => _animator.SetFloat(_movementX, value);
    public void UpdateMovementY(float value) => _animator.SetFloat(_movementY, value);
  }
}