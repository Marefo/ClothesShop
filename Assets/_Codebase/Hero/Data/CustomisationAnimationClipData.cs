using System;
using UnityEngine;

namespace _Codebase.Hero.Data
{
  [Serializable]
  public class CustomisationAnimationClipData
  {
    public HeroStateType StateType;
    public MoveDirectionType MoveDirectionType;
    public AnimationClip Clip;
  }
}