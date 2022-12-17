using System.Collections.Generic;
using System.Linq;
using _Codebase.HeroCode.Data;
using _Codebase.UnitsCode;
using NaughtyAttributes;
using UnityEngine;

namespace _Codebase.Customisation
{
  [CreateAssetMenu(fileName = "CustomisationPartData", menuName = "StaticData/CustomisationPart")]
  public class CustomisationPartData : ScriptableObject
  {
    public bool Empty;
    [Space(10)]
    public string Name;
    [HideIf(nameof(Empty))] public Sprite Icon;
    [HideIf(nameof(Empty))] public int Cost;
    [HideIf(nameof(Empty))] public List<CustomisationAnimationClipData> AnimationClips;

    public AnimationClip GetClip(UnitStateType unitStateType, MoveDirectionType moveDirectionType)
    {
      return AnimationClips
        .First(clip => clip.StateType == unitStateType && clip.MoveDirectionType == moveDirectionType).Clip;
    }
  }
}