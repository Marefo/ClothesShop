using System;
using _Codebase.Customisation;
using _Codebase.HeroCode.Data;
using _CodeBase.Logging;
using _Codebase.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Codebase.UI
{
  public class CustomisationPartChanger : MonoBehaviour
  {
    [SerializeField] private CustomisationPartType _partType;
    [SerializeField] private bool _fromBought;
    [Space(10)] 
    [SerializeField] private AudioService _audioService;
    [SerializeField] private TextMeshProUGUI _currentPartNameField;
    [SerializeField] private Button _prevPartButton;
    [SerializeField] private Button _nextPartButton;
    [Space(10)] 
    [SerializeField] private CustomisationData _customisationData;

    private void OnEnable()
    {
      _prevPartButton.onClick.AddListener(OnPreviousPartButtonClick);
      _nextPartButton.onClick.AddListener(OnNextPartButtonClick);
      _customisationData.PartChanged += OnPartChange;
      _customisationData.CurrentPartsChanged += UpdateCurrentPartName;
    }

    private void OnDisable()
    {
      _prevPartButton.onClick.RemoveListener(OnPreviousPartButtonClick);
      _nextPartButton.onClick.RemoveListener(OnNextPartButtonClick);
      _customisationData.PartChanged -= OnPartChange;
      _customisationData.CurrentPartsChanged -= UpdateCurrentPartName;
    }

    private void Start() => UpdateCurrentPartName();

    private void OnPartChange(CustomisationPartType partType, CustomisationPartData previousPartData, 
      CustomisationPartData newPartData)
    {
      if(partType != _partType) return;
      SetCurrentPartName(newPartData.Name);
    }

    private void OnPreviousPartButtonClick()
    {
      _audioService.Play(_audioService.SfxData.Click);
      
      if(_fromBought)
        _customisationData.ChangePartDataToPreviousFromBought(_partType);
      else
        _customisationData.ChangePartDataToPreviousFromAll(_partType);
    }

    private void OnNextPartButtonClick()
    {
      _audioService.Play(_audioService.SfxData.Click);
      
      if(_fromBought)
        _customisationData.ChangePartDataToNextFromBought(_partType);
      else
        _customisationData.ChangePartDataToNextFromAll(_partType);
    }

    private void UpdateCurrentPartName()
    {
      var currentName = _customisationData.GetCurrentPartDataAsSingleType(_partType).CustomisationPartData.Name;
      SetCurrentPartName(currentName);
    }

    private void SetCurrentPartName(string currentName) => _currentPartNameField.text = currentName;
  }
}