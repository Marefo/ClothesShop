using _Codebase.Logic.Data;
using UnityEngine;

namespace _Codebase.CustomerCode
{
  [CreateAssetMenu(fileName = "SpawnerSettings", menuName = "Settings/Spawner")]
  public class SpawnerSettings : ScriptableObject
  {
    public int MaxOnSceneAmount;
    [Space(10)]
    public Range SpawnFrequency;
    [Space(10)]
    public GameObject Prefab;
  }
}