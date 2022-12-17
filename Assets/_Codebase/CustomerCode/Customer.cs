using System;
using System.Collections;
using _Codebase.Extensions;
using _Codebase.Logic;
using _CodeBase.Points;
using _Codebase.UnitsCode;
using DG.Tweening;
using Pathfinding;
using UnityEngine;
using Range = _Codebase.Logic.Data.Range;

namespace _Codebase.CustomerCode
{
  public class Customer : MonoBehaviour
  {
    public event Action<Customer> Destroyed;
    
    public bool CanDie { get; private set; }
    
    [SerializeField] private Seeker _seeker;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private UnitAnimator _animator;
    [Space(10)] 
    [SerializeField] private CustomerSettings _settings;

    private DefaultPointsStorage _storePoints;
    private DefaultPointsStorage _deathZonePoints;
    private Point _currentTargetPoint;
    private Path _path;
    private int _currentWaypoint;
    private bool _reachedEndOfPath;
    private Vector2 _lastMovement;
    private Coroutine _targetPointCoroutine;
    private bool _initialized;

    private void FixedUpdate()
    {
      if(_initialized == false) return;
      
      if (_path != null)
      {
        if (IsPathComplete() == false)
        {
          Move(); 
          TrySetNextWaypoint();
        } 
      }
      
      UpdateMoveAnimations();
    }

    public void Initialize(DefaultPointsStorage storePoints, DefaultPointsStorage deathZonePoints)
    {
      _storePoints = storePoints;
      _deathZonePoints = deathZonePoints;
      _targetPointCoroutine = StartCoroutine(UpdateTargetPointCoroutine());
      StartCoroutine(UpdatePathCoroutine());
      DOVirtual.DelayedCall(_settings.LifeTime.GetRandomValue(), MoveToDeathZone).SetLink(gameObject);
      _initialized = true;
    }

    public void Destroy()
    {
      Destroyed?.Invoke(this);
      Destroy(gameObject);
    }
    
    private void MoveToDeathZone()
    {
      CanDie = true;
      StopCoroutine(_targetPointCoroutine);
      _currentTargetPoint = _deathZonePoints.GetRandomPoint();
      UpdatePath();
    }

    private IEnumerator UpdateTargetPointCoroutine()
    {
      while (true)
      {
        SetRandomTargetPoint();
        yield return new WaitForSeconds(_settings.UpdateTargetPointFrequency.GetRandomValue());
      }
    }

    private IEnumerator UpdatePathCoroutine()
    {
      while (true)
      {
        UpdatePath();
        yield return new WaitForSeconds(0.25f);
      }
    }

    private void SetRandomTargetPoint() => 
      _currentTargetPoint = _storePoints.GetRandomPoint();

    private void UpdatePath() => 
      _seeker.StartPath(transform.position, _currentTargetPoint.Position, OnPathRecalculate);

    private void OnPathRecalculate(Path path)
    {
      if(path.error) return;
      _path = path;
      _currentWaypoint = 0;
    }

    private void Move()
    {
      Vector2 difference = _path.vectorPath[_currentWaypoint] - transform.position;
      Vector2 direction = difference.normalized;

      _rigidbody.AddForce(direction * _settings.MoveSpeed);
      
    }

    private bool IsPathComplete()
    {
      bool isPathCompletePath = _currentWaypoint >= _path.vectorPath.Count;

      if (isPathCompletePath)
      {
        OnPathComplete();
        return true;
      }

      return false;
    }

    private void TrySetNextWaypoint()
    {
      float distanceToCurrentWaypoint = Vector2.Distance(_path.vectorPath[_currentWaypoint], transform.position);

      if (distanceToCurrentWaypoint < _settings.TargetDistanceToWaypoint)
        _currentWaypoint += 1;
    }

    private void OnPathComplete()
    {
      _lastMovement = Vector2.up;
      _rigidbody.velocity = Vector2.zero;
    }

    private void UpdateMoveAnimations()
    {
      Vector2 movement = _rigidbody.velocity.normalized;
      bool isMoving = movement.magnitude > _settings.WalkAnimationThreshold;

      if (isMoving)
        _lastMovement = movement;

      _animator.UpdateLastMovementX(_lastMovement.x);
      _animator.UpdateLastMovementY(_lastMovement.y);
      _animator.UpdateMovementX(movement.x);
      _animator.UpdateMovementY(movement.y);
      _animator.ChangeMovementState(isMoving);
    }
  }
}