using System;
using _Codebase.Services;
using UnityEngine;

namespace _Codebase.Hero
{
  public class HeroMovement : MonoBehaviour
  {
    [SerializeField] private InputService _inputService;
    [Space(10)]
    [SerializeField] private Rigidbody2D _rigidbody;
    [Space(10)] 
    [SerializeField] private HeroSettings _settings;

    private void FixedUpdate() => Move();

    private void Move()
    {
      Vector2 input = _inputService.GetMovementInput().normalized;
      _rigidbody.velocity = input * _settings.MoveSpeed;
    }
  }
}
