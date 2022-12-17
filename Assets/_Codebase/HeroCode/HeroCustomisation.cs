using _Codebase.Camera;
using _Codebase.Customisation;
using _Codebase.HeroCode.Data;
using _Codebase.Logic;
using _Codebase.UI;
using NaughtyAttributes;
using UnityEngine;

namespace _Codebase.HeroCode
{
  public class HeroCustomisation : MonoBehaviour
  {
    [SerializeField] private Showable _characterClotheChangingPanel;
    [SerializeField] private Showable _characterClotheFittingPanel;
    [SerializeField] private CamerasStateController _camerasStateController;
    [Space(10)]
    [SerializeField] private CustomisationData _customisationData;

    public void StartFittingClothes()
    {
      _customisationData.OnFittingStart();
      _camerasStateController.ZoomIn();
      _characterClotheFittingPanel.Show();
    }

    public void FinishFittingClothes()
    {
      _camerasStateController.ZoomOut();
      _characterClotheFittingPanel.Hide();
      _customisationData.OnFittingFinish();
    }

    public void StartChangingClothes()
    {
      _camerasStateController.ZoomIn();
      _characterClotheChangingPanel.Show();
    }

    public void FinishChangingClothes()
    {
      _camerasStateController.ZoomOut();
      _characterClotheChangingPanel.Hide();
    }
    
    [Button()]
    public void TestNextSkin() => _customisationData.ChangePartDataToNextFromAll(CustomisationPartType.Body);
    
    [Button()]
    public void TestNextHair() => _customisationData.ChangePartDataToNextFromAll(CustomisationPartType.Hair);
    
    [Button()]
    public void TestNextPants() => _customisationData.ChangePartDataToNextFromAll(CustomisationPartType.Pants);
    
    [Button()]
    public void TestNextShoes() => _customisationData.ChangePartDataToNextFromAll(CustomisationPartType.Shoes);
    
    [Button()]
    public void TestNextShirt() => _customisationData.ChangePartDataToNextFromAll(CustomisationPartType.Shirt);
  }
}