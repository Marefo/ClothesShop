using System;
using NaughtyAttributes;
using UnityEngine;

namespace _CodeBase.Points
{
  public class Point : MonoBehaviour
  {
    public event Action PositionChanged;
    public event Action<Point> Released;
		
    public bool Available { get; protected set; } = true;
    public Vector3 Position => transform.position;
    public Quaternion TargetQuaternionRotation { get; private set; }

    [field: SerializeField, ShowIf("_hasTargetRotation")] public Vector3 TargetRotation;
    [SerializeField] private bool _hasTargetRotation;
		
    public virtual void Take() => Available = false;

    public virtual void SetTargetRotation(Quaternion targetRotation) => 
      TargetQuaternionRotation = targetRotation;

    public virtual void Release()
    {
      Available = true;
      Released?.Invoke(this);
    }

    public void OnPositionChange() => PositionChanged?.Invoke();
  }
}