using System.Collections.Generic;
using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace _Codebase.Hero.Data
{
  [CreateAssetMenu(fileName = "CustomisationPartData", menuName = "StaticData/CustomisationPart")]
  public class CustomisationPartData : ScriptableObject
  {
    public bool Empty;
    [HideIf(nameof(Empty))] public List<CustomisationAnimationClipData> AnimationClips;

    public AnimationClip GetClip(HeroStateType heroStateType, MoveDirectionType moveDirectionType)
    {
      return AnimationClips
        .First(clip => clip.StateType == heroStateType && clip.MoveDirectionType == moveDirectionType)?.Clip;
    }
  }
}