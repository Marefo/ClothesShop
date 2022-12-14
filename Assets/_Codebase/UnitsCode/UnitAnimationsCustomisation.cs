using System;
using System.Collections.Generic;
using System.Linq;
using _Codebase.Customisation;
using _Codebase.HeroCode.Data;
using _Codebase.Infrastructure;
using _CodeBase.Logging;
using UnityEngine;

namespace _Codebase.UnitsCode
{
  public class UnitAnimationsCustomisation : MonoBehaviour
  {
    [SerializeField] private bool _setRandomPartsOnStart;
    [Space(10)]
    [SerializeField] private List<CustomisationPartGameObject> _customisationPartObjects;
    [Space(10)]
    [SerializeField] private Animator _animator;
    [Space(10)]
    [SerializeField] private CustomisationData _customisationData;
    
    private AnimatorOverrideController _animatorOverrideController;
    private List<KeyValuePair<AnimationClip,AnimationClip>> _currentClips;

    private void Awake()
    {
      InitializeAnimatorOverrideController();
      
      if(_setRandomPartsOnStart)
        SetRandomCustomisationParts();
      else
        UpdateCustomisationPartsByCurrentData();
    }

    private void OnEnable()
    {
      _customisationData.PartChanged += OnPartChange;
      _customisationData.CurrentPartsChanged += UpdateCustomisationPartsByCurrentData;
    }

    private void OnDisable()
    {
      _customisationData.PartChanged -= OnPartChange;
      _customisationData.CurrentPartsChanged -= UpdateCustomisationPartsByCurrentData;
    }

    private void OnPartChange(CustomisationPartType partType, CustomisationPartData currentPartData, CustomisationPartData nextPartData)
    {
      if (nextPartData.Empty)
      {
        ChangePartState(partType, false);
        return;
      }
      
      if (currentPartData.Empty) 
        ChangePartState(partType, true);

      foreach (var clipData in nextPartData.AnimationClips)
        ReplaceClip(partType, clipData);
    }

    private void InitializeAnimatorOverrideController()
    {
      _animatorOverrideController = new AnimatorOverrideController(_animator.runtimeAnimatorController);
      _animator.runtimeAnimatorController = _animatorOverrideController;

      _currentClips = new List<KeyValuePair<AnimationClip, AnimationClip>>();
      _animatorOverrideController.GetOverrides(_currentClips);
      _animatorOverrideController.ApplyOverrides(_currentClips);
    }

    private void SetRandomCustomisationParts() => UpdateCustomisationParts(_customisationData.GetRandomPartData);
    private void UpdateCustomisationPartsByCurrentData() => UpdateCustomisationParts(_customisationData.GetCurrentPartData);

    private void UpdateCustomisationParts(Func<CustomisationPartType, CustomisationPartData> getPartData)
    {
      var partTypes = Helpers.EnumToList<CustomisationPartType>();
      foreach (CustomisationPartType partType in partTypes)
      {
        var targetPartData = getPartData(partType);

        if (targetPartData.Empty)
        {
          ChangePartState(partType, false);
          continue;
        }
        
        var stateTypes = Helpers.EnumToList<UnitStateType>();
        foreach (UnitStateType stateType in stateTypes)
        {
          var directionTypes = Helpers.EnumToList<MoveDirectionType>();
          foreach (MoveDirectionType directionType in directionTypes)
          {
            string currentClipName = GetAnimationClipKey(partType, stateType, directionType);
            AnimationClip targetClip = targetPartData.GetClip(stateType, directionType);
            ReplaceClip(currentClipName, targetClip);
          }  
        }
      }
    }

    private void ChangePartState(CustomisationPartType type, bool enable) => 
      _customisationPartObjects.First(partObject => partObject.Type == type).GameObject.SetActive(enable);

    private void ReplaceClip(CustomisationPartType partType, CustomisationAnimationClipData clipData)
    {
      string keyName = GetAnimationClipKey(partType, clipData.StateType, clipData.MoveDirectionType);
      int changingClipIndex = _currentClips.FindIndex(pair => pair.Key.name.Equals(keyName));

      if(changingClipIndex == -1) return;
      
      _currentClips[changingClipIndex] = 
        new KeyValuePair<AnimationClip, AnimationClip>(_currentClips[changingClipIndex].Key, clipData.Clip);
      _animatorOverrideController.ApplyOverrides(_currentClips);
    }

    private void ReplaceClip(string from, AnimationClip to)
    {
      int changingClipIndex = _currentClips.FindIndex(pair => pair.Key.name.Equals(from));

      if(changingClipIndex == -1) return;
      
      _currentClips[changingClipIndex] = 
        new KeyValuePair<AnimationClip, AnimationClip>(_currentClips[changingClipIndex].Key, to);
      _animatorOverrideController.ApplyOverrides(_currentClips);
    }

    private string GetAnimationClipKey(CustomisationPartType partType, UnitStateType stateType,
      MoveDirectionType directionType)
    {
      string partTypeName = GetCustomisationPartName(partType);
      string stateTypeName = GetStateName(stateType);
      string directionTypeName = GetMovementDirectionName(directionType);
      string currentClipName = $"{partTypeName}_0_{stateTypeName}_{directionTypeName}";
      return currentClipName;
    }

    private string GetCustomisationPartName(CustomisationPartType type)
    {
      return type switch
      {
        CustomisationPartType.Body => "body",
        CustomisationPartType.Hair => "hair",
        CustomisationPartType.Pants => "pants",
        CustomisationPartType.Shoes => "shoes",
        CustomisationPartType.Shirt => "shirt",
        _ => ""
      };
    }
    
    private string GetStateName(UnitStateType type)
    {
      return type switch
      {
        UnitStateType.Idle => "idle",
        UnitStateType.Walk => "walk",
        _ => ""
      };
    }

    private string GetMovementDirectionName(MoveDirectionType type)
    {
      return type switch
      {
        MoveDirectionType.Down => "down",
        MoveDirectionType.Left => "left",
        MoveDirectionType.Top => "top",
        MoveDirectionType.Right => "right",
        _ => ""
      };
    }
  }
}