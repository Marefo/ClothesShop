using _Codebase.HeroCode.Data;
using _Codebase.Money;
using UnityEngine;

namespace _Codebase.Logic
{
  public class LevelPreparer : MonoBehaviour
  {
    [SerializeField] private MoneyData _moneyData;
    [SerializeField] private HeroCustomisationData _customisationData;

    private void Awake()
    {
      _moneyData.Reset();
      _customisationData.SetToDefault();
    }
  }
}