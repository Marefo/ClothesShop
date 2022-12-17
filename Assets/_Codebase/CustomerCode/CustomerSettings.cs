using _Codebase.Logic.Data;
using UnityEngine;

namespace _Codebase.CustomerCode
{
  [CreateAssetMenu(fileName = "CustomerSettings", menuName = "Settings/Customer")]
  public class CustomerSettings : ScriptableObject
  {
    public Range LifeTime;
    public Range UpdateTargetPointFrequency;
    [Space(10)]
    public float MoveSpeed;
    public float WalkAnimationThreshold;
    public float TargetDistanceToWaypoint;
  }
}