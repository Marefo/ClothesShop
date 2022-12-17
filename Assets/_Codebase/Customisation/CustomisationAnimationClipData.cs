using System;
using _Codebase.HeroCode.Data;
using _Codebase.UnitsCode;
using UnityEngine;

namespace _Codebase.Customisation
{
  [Serializable]
  public class CustomisationAnimationClipData
  {
    public UnitStateType StateType;
    public MoveDirectionType MoveDirectionType;
    public AnimationClip Clip;
  }
}