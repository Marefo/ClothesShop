using System;
using _Codebase.Customisation;
using _Codebase.HeroCode.Data;
using UnityEngine;

namespace _Codebase.UI.Shop
{
  public class ShopPanel : MonoBehaviour
  {
    [SerializeField] private Transform _itemsContainer;
    [SerializeField] private ShopItem _shopItemPrefab;
    [Space(10)]
    [SerializeField] private CustomisationData _customisationData;

    private void Start() => GenerateShopItems();

    private void GenerateShopItems()
    {
      foreach (var partsTypeData in _customisationData.UnBoughtPartsTypesData)
      {
        if(_customisationData.IsProduct(partsTypeData.CustomisationPartType) == false) continue;
        var currentPartsType = partsTypeData.CustomisationPartType;
        
        foreach (var partData in partsTypeData.CustomisationPartsData)
        {
          if(partData.Empty) continue;
          ShopItem shopItem = Instantiate(_shopItemPrefab, _itemsContainer);
          shopItem.Initialize(currentPartsType, partData);
        }
      }
    }
  }
}