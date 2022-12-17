using System;
using System.Collections.Generic;
using System.Linq;
using _Codebase.Extensions;
using _Codebase.Infrastructure;
using NaughtyAttributes;
using UnityEngine;

namespace _Codebase.HeroCode.Data
{
  [CreateAssetMenu(fileName = "HeroCustomisationData", menuName = "StaticData/HeroCustomisation")]
  public class HeroCustomisationData : ScriptableObject
  {
    public event Action<CustomisationPartType, CustomisationPartData, CustomisationPartData> PartChanged;
    public event Action CurrentPartsChanged;
    
    public List<CustomisationMultiplePartsType> AllPartsTypesData;
    [Space(10)]
    public List<CustomisationMultiplePartsType> UnBoughtPartsTypesData;
    [Space(10)]
    public List<CustomisationMultiplePartsType> BoughtPartsTypesData;
    [Space(10)]
    public List<CustomisationSinglePartType> CurrentPartsData;
    [Space(10)] 
    public List<CustomisationPartType> ProductTypes;
    [Space(10)] 
    public CustomisationPartData EmptyPart;

    private List<CustomisationSinglePartType> BeforeFittingPartsData;
    
    public CustomisationSinglePartType GetCurrentPartData(CustomisationPartType partType) =>
      CurrentPartsData.First(partData => partData.Type == partType);
    
    [Button]
    public void SetToDefault()
    {
      var customisationPartTypes = Helpers.EnumToList<CustomisationPartType>();

      UnBoughtPartsTypesData.Clear();
      foreach (var multiplePartsByType in AllPartsTypesData)
      {
        if(IsProduct(multiplePartsByType.CustomisationPartType) == false) continue;

        List<CustomisationPartData> customisationPartsData = multiplePartsByType.CustomisationPartsData.ToList();
        var newPartsByType = new CustomisationMultiplePartsType(multiplePartsByType.CustomisationPartType,
          customisationPartsData);
        
        UnBoughtPartsTypesData.Add(newPartsByType);
      }

      CurrentPartsData.Clear();
      foreach (var type in customisationPartTypes)
      {
        var newPartData = new CustomisationSinglePartType(type, GetPartsByType(type, AllPartsTypesData)
          .First());
        CurrentPartsData.Add(newPartData);
      }
      
      BoughtPartsTypesData.Clear();
      foreach (var partsByType in UnBoughtPartsTypesData)
      {
        var parts = new List<CustomisationPartData>{ EmptyPart };
        var newPartData = new CustomisationMultiplePartsType(partsByType.CustomisationPartType, parts);
        BoughtPartsTypesData.Add(newPartData);
      }
    }

    public bool IsProduct(CustomisationPartType type) => ProductTypes.Contains(type);
    
    public void Buy(CustomisationPartType type, CustomisationPartData part)
    {
      var unBoughtPartsWithSameType = GetPartsByType(type, UnBoughtPartsTypesData);
      unBoughtPartsWithSameType.Remove(part);
      var boughtPartsWithSameType = GetPartsByType(type, BoughtPartsTypesData);
      boughtPartsWithSameType.Add(part);
    }

    public void OnFittingStart() => BeforeFittingPartsData = CopySinglePartsType(CurrentPartsData);
    
    public void OnFittingFinish()
    {
      CurrentPartsData = CopySinglePartsType(BeforeFittingPartsData);
      CurrentPartsChanged?.Invoke();
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
    
    private List<CustomisationSinglePartType> CopySinglePartsType(List<CustomisationSinglePartType> singlePartsType)
    {
      var copy = new List<CustomisationSinglePartType>();
      
      foreach (var partTypeData in singlePartsType)
      {
        var partData = new CustomisationSinglePartType(partTypeData.Type, partTypeData.CustomisationPartData);
        copy.Add(partData);
      }

      return copy;
    }
  }
}