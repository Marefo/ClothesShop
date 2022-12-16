using _Codebase.Camera;
using _Codebase.HeroCode.Data;
using _Codebase.UI;
using NaughtyAttributes;
using UnityEngine;

namespace _Codebase.HeroCode
{
  public class HeroCustomisation : MonoBehaviour
  {
    [SerializeField] private Panel _characterCustomisationPanel;
    [SerializeField] private CamerasStateController _camerasStateController;
    [Space(10)]
    [SerializeField] private HeroCustomisationData _customisationData;

    public void StartFittingClothes()
    {
      _camerasStateController.ZoomIn();
      _characterCustomisationPanel.Show();
    }

    public void FinishFittingClothes()
    {
      _camerasStateController.ZoomOut();
      _characterCustomisationPanel.Hide();
    }
    
    [Button()]
    public void TestNextSkin() => _customisationData.ChangePartDataToNext(CustomisationPartType.Body);
    
    [Button()]
    public void TestNextHair() => _customisationData.ChangePartDataToNext(CustomisationPartType.Hair);
    
    [Button()]
    public void TestNextPants() => _customisationData.ChangePartDataToNext(CustomisationPartType.Pants);
    
    [Button()]
    public void TestNextShoes() => _customisationData.ChangePartDataToNext(CustomisationPartType.Shoes);
    
    [Button()]
    public void TestNextShirt() => _customisationData.ChangePartDataToNext(CustomisationPartType.Shirt);
  }
}