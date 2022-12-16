using System;
using UnityEngine;

namespace _Codebase.HeroCode.Data
{
  [Serializable]
  public class CustomisationAnimationClipData
  {
    public HeroStateType StateType;
    public MoveDirectionType MoveDirectionType;
    public AnimationClip Clip;
  }
}