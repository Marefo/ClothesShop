using System;
using System.Collections.Generic;
using System.Linq;
using _Codebase.Infrastructure;
using NaughtyAttributes;
using UnityEngine;

namespace _Codebase.HeroCode.Data
{
  [CreateAssetMenu(fileName = "HeroCustomisationData", menuName = "StaticData/HeroCustomisation")]
  public class HeroCustomisationData : ScriptableObject
  {
    public event Action<CustomisationPartType, CustomisationPartData, CustomisationPartData> PartChanged;
    
    public List<CustomisationMultiplePartsType> AllPartsTypesData;
    [Space(10)]
    public List<CustomisationMultiplePartsType> UnBoughtPartsTypesData;
    [Space(10)]
    public List<CustomisationMultiplePartsType> BoughtPartsTypesData;
    [Space(10)]
    public List<CustomisationSinglePartType> CurrentPartsData;

    public CustomisationSinglePartType GetCurrentPartData(CustomisationPartType partType) =>
      CurrentPartsData.First(partData => partData.Type == partType);
    
    [Button]
    public void SetToDefault()
    {
      UnBoughtPartsTypesData = new List<CustomisationMultiplePartsType>(AllPartsTypesData);

      CurrentPartsData.Clear();
      var customisationPartTypes = Helpers.EnumToList<CustomisationPartType>();
      foreach (var type in customisationPartTypes)
      {
        var newPartData = new CustomisationSinglePartType(type, GetPartsByType(type, AllPartsTypesData)
          .First());
        CurrentPartsData.Add(newPartData);
      }
    }
    
    public void Buy(CustomisationPartData part)
    {
      
    }

    public void ChangePartDataToNext(CustomisationPartType partType) => ChangeCurrentPartTo(true, partType);
    public void ChangePartDataToPrevious(CustomisationPartType partType) => ChangeCurrentPartTo(false, partType);

    private void ChangeCurrentPartTo(bool next, CustomisationPartType partType)
    {
      var currentPartTypeData = GetCurrentPartData(partType);
      int globalPartIndex = GetPartsByType(partType, AllPartsTypesData).IndexOf(currentPartTypeData.CustomisationPartData);
      CustomisationPartData newPart = next ? GetNextPartData(partType, globalPartIndex) : GetPreviousPartData(partType, globalPartIndex);
      ChangeCurrentPart(partType, newPart);
    }

    private void ChangeCurrentPart(CustomisationPartType partType, CustomisationPartData newPart)
    {
      var currentPartTypeData = GetCurrentPartData(partType);
      int indexInCurrentParts = CurrentPartsData.IndexOf(currentPartTypeData);
      PartChanged?.Invoke(partType, currentPartTypeData.CustomisationPartData, newPart);
      CurrentPartsData[indexInCurrentParts].CustomisationPartData = newPart;
    }
    
    private CustomisationPartData GetPreviousPartData(CustomisationPartType partType, int currentPartIndex)
    {
      List<CustomisationPartData> parts = GetPartsByType(partType, AllPartsTypesData);
      int previousPartIndex = currentPartIndex - 1;
      return previousPartIndex > 0 ? parts[previousPartIndex] : parts[parts.Count];
    }
    
    private CustomisationPartData GetNextPartData(CustomisationPartType partType, int currentPartIndex)
    {
      List<CustomisationPartData> parts = GetPartsByType(partType, AllPartsTypesData);
      int nextPartIndex = currentPartIndex + 1;
      return nextPartIndex < parts.Count ? parts[nextPartIndex] : parts[0];
    }

    private List<CustomisationPartData> GetPartsByType(CustomisationPartType type, 
      List<CustomisationMultiplePartsType> from)
    {
      return from.First(partTypeData => partTypeData.CustomisationPartType == type).CustomisationPartsData;
    }
  }
}