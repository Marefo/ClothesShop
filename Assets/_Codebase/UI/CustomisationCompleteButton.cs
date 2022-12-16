using _Codebase.Camera;
using UnityEngine;
using UnityEngine.UI;

namespace _Codebase.UI
{
  public class CustomisationCompleteButton : MonoBehaviour
  {
    [SerializeField] private Button _button;
    [SerializeField] private GameObject _panel;
    [SerializeField] private CamerasStateController _camerasStateController;

    private void OnEnable() => _button.onClick.AddListener(OnButtonClick);
    private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);

    private void OnButtonClick()
    {
      _camerasStateController.ZoomOut();
      _panel.SetActive(false);
    }
  }
}