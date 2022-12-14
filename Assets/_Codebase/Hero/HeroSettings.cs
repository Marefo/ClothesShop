using UnityEngine;

namespace _Codebase.Hero
{
  [CreateAssetMenu(fileName = "Hero", menuName = "Settings/Hero")]
  public class HeroSettings : ScriptableObject
  {
    public float MoveSpeed;
  }
}