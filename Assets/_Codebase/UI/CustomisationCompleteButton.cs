using _Codebase.Camera;
using _Codebase.Logic;
using _Codebase.Services;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace _Codebase.UI
{
  public class CustomisationCompleteButton : MonoBehaviour
  {
    [SerializeField] private bool _enableInput;
    [SerializeField, ShowIf(nameof(_enableInput))] private InputService _inputService;
    [SerializeField] private Button _button;
    [SerializeField] private Showable _panel;
    [SerializeField] private CamerasStateController _camerasStateController;

    private void OnEnable() => _button.onClick.AddListener(OnButtonClick);
    private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);

    private void OnButtonClick()
    {
      if(_enableInput)
        _inputService.Enable();
      
      _camerasStateController.ZoomOut();
      _panel.Hide();
    }
  }
}