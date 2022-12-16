using System;
using _Codebase.HeroCode.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Codebase.UI
{
  public class CustomisationPartChanger : MonoBehaviour
  {
    [SerializeField] private CustomisationPartType _partType;
    [Space(10)] 
    [SerializeField] private TextMeshProUGUI _currentPartNameField;
    [SerializeField] private Button _prevPartButton;
    [SerializeField] private Button _nextPartButton;
    [Space(10)] 
    [SerializeField] private HeroCustomisationData _customisationData;

    private void Awake() => InitializeCurrentPartName();

    private void OnEnable()
    {
      _prevPartButton.onClick.AddListener(OnPreviousPartButtonClick);
      _nextPartButton.onClick.AddListener(OnNextPartButtonClick);
      _customisationData.PartChanged += OnPartChange;
    }

    private void OnDisable()
    {
      _prevPartButton.onClick.RemoveListener(OnPreviousPartButtonClick);
      _nextPartButton.onClick.RemoveListener(OnNextPartButtonClick);
      _customisationData.PartChanged -= OnPartChange;
    }

    private void OnPartChange(CustomisationPartType partType, CustomisationPartData previousPartData, 
      CustomisationPartData newPartData)
    {
      if(partType != _partType) return;
      SetCurrentPartName(newPartData.Name);
    }

    private void OnPreviousPartButtonClick() => _customisationData.ChangePartDataToPrevious(_partType);

    private void OnNextPartButtonClick() => _customisationData.ChangePartDataToNext(_partType);

    private void InitializeCurrentPartName()
    {
      var currentName = _customisationData.GetCurrentPartData(_partType).CustomisationPartData.Name;
      SetCurrentPartName(currentName);
    }

    private void SetCurrentPartName(string currentName) => _currentPartNameField.text = currentName;
  }
}