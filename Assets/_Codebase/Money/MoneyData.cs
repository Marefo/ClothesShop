using System;
using NaughtyAttributes;
using UnityEngine;

namespace _Codebase.Money
{
  [CreateAssetMenu(fileName = "MoneyData", menuName = "StaticData/Money")]
  public class MoneyData : ScriptableObject
  {
    public event Action<int> AmountChanged;

    public int Amount;
    public int StartAmount;

    public void Add(int amount)
    {
      Amount += amount;
      AmountChanged?.Invoke(Amount);
    }

    public int Remove(int amount)
    {
      int shortage = Amount - amount;
			
      Amount = Mathf.Clamp(Amount - amount, 0, int.MaxValue);
      AmountChanged?.Invoke(Amount);

      return shortage < 0 ? Mathf.Abs(shortage) : 0;
    }

    [Button()]
    public void Reset()
    {
      Amount = StartAmount;
      AmountChanged?.Invoke(Amount);
    }
  }
}