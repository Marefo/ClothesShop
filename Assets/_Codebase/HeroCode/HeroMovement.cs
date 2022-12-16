using _Codebase.HeroCode.Data;
using _Codebase.Services;
using UnityEngine;

namespace _Codebase.HeroCode
{
  public class HeroMovement : MonoBehaviour
  {
    [SerializeField] private InputService _inputService;
    [Space(10)]
    [SerializeField] private HeroAnimator _heroAnimator;
    [SerializeField] private Rigidbody2D _rigidbody;
    [Space(10)] 
    [SerializeField] private HeroSettings _settings;

    private Vector2 _lastMovement;
    
    private void FixedUpdate()
    {
      Move();
      UpdateMoveAnimations();
    }

    private void Move()
    {
      Vector2 input = _inputService.GetMovementInput().normalized;
      _rigidbody.velocity = input * _settings.MoveSpeed;
    }

    private void UpdateMoveAnimations()
    {
      Vector2 movement = _rigidbody.velocity.normalized;
      bool isMoving = movement.magnitude > _settings.WalkAnimationThreshold;

      if (isMoving)
        _lastMovement = movement;

      _heroAnimator.UpdateLastMovementX(_lastMovement.x);
      _heroAnimator.UpdateLastMovementY(_lastMovement.y);
      _heroAnimator.UpdateMovementX(movement.x);
      _heroAnimator.UpdateMovementY(movement.y);
      _heroAnimator.ChangeMovementState(isMoving);
    }
  }
}
