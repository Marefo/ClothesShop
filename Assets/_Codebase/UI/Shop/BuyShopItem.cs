using _Codebase.Customisation;
using _Codebase.Money;
using _Codebase.Services;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Codebase.UI.Shop
{
  public class BuyShopItem : MonoBehaviour
  {
    [SerializeField] private Color _failedBtnColor;
    [Space(10)] 
    [SerializeField] private Button _buyButton;
    [SerializeField] private Image _buyButtonImage;
    [SerializeField] private TextMeshProUGUI _nameField;
    [SerializeField] private TextMeshProUGUI _costField;
    [SerializeField] private Image _icon;
    [Space(10)] 
    [SerializeField] private CustomisationData _customisationData;
    [SerializeField] private MoneyData _moneyData;

    private string _itemName => $"{_type} {_data.Name}";
    
    private CustomisationPartType _type;
    private CustomisationPartData _data;
    private int _buyCost;
    private Tween _resetButtonColorTween;
    private AudioService _audioService;
    
    private void OnDestroy() => _buyButton.onClick.RemoveListener(TryBuy);

    public void Initialize(CustomisationPartType partType, CustomisationPartData customisationPartData,
      AudioService audioService)
    {
      _type = partType;
      _data = customisationPartData;
      _buyCost = _data.BuyCost;
      _audioService = audioService;
      SetNameText(_itemName);
      SetIcon(_data.Icon);
      SetCostText(_buyCost);
      _buyButton.onClick.AddListener(TryBuy);
    }

    private void TryBuy()
    {
      if (_buyCost > _moneyData.Amount)
        OnBuyFailed();
      else
        Buy();
    }

    private void OnBuyFailed()
    {
      _resetButtonColorTween?.Kill();
      _buyButtonImage.color = _failedBtnColor;
      _resetButtonColorTween = DOVirtual.DelayedCall(0.2f, () => _buyButtonImage.color = Color.white);
      _audioService.Play(_audioService.SfxData.FailedPurchase);
    }

    private void Buy()
    {
      _moneyData.Remove(_buyCost);
      _customisationData.Buy(_type, _data);
      _audioService.Play(_audioService.SfxData.SuccessfulPurchase);
      Destroy(gameObject);
    }
    
    private void SetNameText(string nameText) => _nameField.text = nameText;
    private void SetIcon(Sprite icon) => _icon.sprite = icon;
    private void SetCostText(int cost) => _costField.text = $"-{cost}$";
  }
}