using UnityEngine;

namespace _Codebase.Hero.Data
{
  [CreateAssetMenu(fileName = "HeroSettings", menuName = "Settings/Hero")]
  public class HeroSettings : ScriptableObject
  {
    public float MoveSpeed;
    public float WalkAnimationThreshold;
  }
}