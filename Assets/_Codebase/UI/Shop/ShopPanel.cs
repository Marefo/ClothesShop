using System;
using System.Collections.Generic;
using _Codebase.Customisation;
using _Codebase.Services;
using UnityEngine;

namespace _Codebase.UI.Shop
{
  public class ShopPanel : MonoBehaviour
  {
    [SerializeField] private Transform _buyItemsContainer;
    [SerializeField] private BuyShopItem _buyItemPrefab;
    [Space(10)]
    [SerializeField] private Transform _sellItemsContainer;
    [SerializeField] private SellShopItem _sellItemPrefab;
    [Space(10)]
    [SerializeField] private AudioService _audioService;
    [Space(10)]
    [SerializeField] private CustomisationData _customisationData;

    private void OnEnable()
    {
      _customisationData.Bought += OnBuy;
      _customisationData.Sold += OnSell;
    }

    private void OnDisable()
    {
      _customisationData.Bought -= OnBuy;
      _customisationData.Sold -= OnSell;
    }

    private void OnBuy(CustomisationPartType type, CustomisationPartData data) => CreateSellItem(type, data);

    private void OnSell(CustomisationPartType type, CustomisationPartData data) => CreateBuyItem(type, data);

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
          CreateBuyItem(currentPartsType, partData);
        }
      }
    }

    private void CreateBuyItem(CustomisationPartType type, CustomisationPartData data)
    {
      BuyShopItem buyItem = Instantiate(_buyItemPrefab, _buyItemsContainer);
      buyItem.Initialize(type, data, _audioService);
    }

    private void CreateSellItem(CustomisationPartType type, CustomisationPartData data)
    {
      SellShopItem sellItem = Instantiate(_sellItemPrefab, _sellItemsContainer);
      sellItem.Initialize(type, data, _audioService);
    }
  }
}