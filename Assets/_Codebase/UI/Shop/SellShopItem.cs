using _Codebase.Customisation;
using _Codebase.Money;
using _Codebase.Services;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Codebase.UI.Shop
{
  public class SellShopItem : MonoBehaviour
  {
    [SerializeField] private Button _sellButton;
    [SerializeField] private TextMeshProUGUI _nameField;
    [SerializeField] private TextMeshProUGUI _costField;
    [SerializeField] private Image _icon;
    [Space(10)] 
    [SerializeField] private CustomisationData _customisationData;
    [SerializeField] private MoneyData _moneyData;

    private string _itemName => $"{_type} {_data.Name}";
    
    private CustomisationPartType _type;
    private CustomisationPartData _data;
    private int _sellCost;
    private Tween _resetButtonColorTween;
    private AudioService _audioService;
    
    private void OnDestroy() => _sellButton.onClick.RemoveListener(Sell);

    public void Initialize(CustomisationPartType partType, CustomisationPartData customisationPartData,
      AudioService audioService)
    {
      _type = partType;
      _data = customisationPartData;
      _sellCost = _data.SellCost;
      _audioService = audioService;
      SetNameText(_itemName);
      SetIcon(_data.Icon);
      SetCostText(_sellCost);
      _sellButton.onClick.AddListener(Sell);
    }

    private void Sell()
    {
      _moneyData.Add(_sellCost);
      _customisationData.Sell(_type, _data);
      _audioService.Play(_audioService.SfxData.SuccessfulPurchase);
      Destroy(gameObject);
    }
    
    private void SetNameText(string nameText) => _nameField.text = nameText;
    private void SetIcon(Sprite icon) => _icon.sprite = icon;
    private void SetCostText(int cost) => _costField.text = $"+{cost}$";
  }
}