using UnityEngine;

namespace _Codebase.HeroCode.Data
{
  [CreateAssetMenu(fileName = "HeroSettings", menuName = "Settings/Hero")]
  public class HeroSettings : ScriptableObject
  {
    public float MoveSpeed;
    public float WalkAnimationThreshold;
  }
}