using _Codebase.Hero.Data;
using NaughtyAttributes;
using UnityEngine;

namespace _Codebase.Hero
{
  public class HeroCustomisation : MonoBehaviour
  {
    [SerializeField] private HeroCustomisationData _customisationData;
    
    [Button()]
    public void NextSkin() => _customisationData.ChangePartDataToNext(CustomisationPartType.Body);
    
    [Button()]
    public void NextHair() => _customisationData.ChangePartDataToNext(CustomisationPartType.Hair);
    
    [Button()]
    public void NextPants() => _customisationData.ChangePartDataToNext(CustomisationPartType.Pants);
    
    [Button()]
    public void NextShoes() => _customisationData.ChangePartDataToNext(CustomisationPartType.Shoes);
    
    [Button()]
    public void NextShirt() => _customisationData.ChangePartDataToNext(CustomisationPartType.Shirt);
  }
}