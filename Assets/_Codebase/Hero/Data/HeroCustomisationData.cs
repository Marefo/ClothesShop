using System;
using System.Collections.Generic;
using System.Linq;
using _CodeBase.Logging;
using UnityEngine;

namespace _Codebase.Hero.Data
{
  [CreateAssetMenu(fileName = "HeroCustomisationData", menuName = "StaticData/HeroCustomisation")]
  public class HeroCustomisationData : ScriptableObject
  {
    public event Action<CustomisationPartType, CustomisationPartData, CustomisationPartData> PartChanged;
    
    public List<CustomisationMultiplePartsTypeData> AllPartsTypesData;
    public List<CustomisationSinglePartTypeData> CurrentPartsData;

    public CustomisationSinglePartTypeData GetCurrentPartData(CustomisationPartType partType) =>
      CurrentPartsData.First(partData => partData.Type == partType);

    public void ChangePartDataToNext(CustomisationPartType partType)
    {
      var currentPartTypeData = GetCurrentPartData(partType);
      int globalPartIndex = GetPartsByType(partType).IndexOf(currentPartTypeData.CustomisationPartData);
      var nextPart = GetNextPartData(partType, globalPartIndex);
      ChangeCurrentPartData(partType, nextPart);
    }

    private void ChangeCurrentPartData(CustomisationPartType partType, CustomisationPartData newPart)
    {
      var currentPartTypeData = GetCurrentPartData(partType);
      int indexInCurrentParts = CurrentPartsData.IndexOf(currentPartTypeData);
      PartChanged?.Invoke(partType, currentPartTypeData.CustomisationPartData, newPart);
      CurrentPartsData[indexInCurrentParts].CustomisationPartData = newPart;
    }

    private CustomisationPartData GetNextPartData(CustomisationPartType partType, int currentPartIndex)
    {
      List<CustomisationPartData> parts = GetPartsByType(partType);
      int nextPartIndex = currentPartIndex + 1;
      return nextPartIndex < parts.Count ? parts[nextPartIndex] : parts[0];
    }

    private List<CustomisationPartData> GetPartsByType(CustomisationPartType type) =>
      AllPartsTypesData.First(partTypeData => partTypeData.CustomisationPartType == type).CustomisationPartsData;
  }
}