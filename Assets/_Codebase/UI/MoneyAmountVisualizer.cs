using _Codebase.Money;
using TMPro;
using UnityEngine;

namespace _Codebase.UI
{
  public class MoneyAmountVisualizer : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI _field;
    [SerializeField] private MoneyData _moneyData;

    private void OnEnable() => _moneyData.AmountChanged += SetFieldValue;
    private void OnDisable() => _moneyData.AmountChanged -= SetFieldValue;

    private void Start() => SetFieldValue(_moneyData.Amount);
    private void SetFieldValue(int value) => _field.text = value.ToString();
  }
}