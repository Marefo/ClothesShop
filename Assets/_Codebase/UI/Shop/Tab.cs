using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Codebase.UI.Shop
{
  public class Tab : MonoBehaviour
  {
    public event Action<Tab> Clicked;
    
    [SerializeField] private Color _activeColor;
    [SerializeField] private GameObject _content;
    [SerializeField] private Image _background;
    [SerializeField] private Button _button;

    private void OnEnable() => _button.onClick.AddListener(OnButtonClick);
    private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);

    public void Open()
    {
      _background.color = _activeColor;
      _content.SetActive(true);
    }

    public void Close()
    {
      _background.color = Color.white;
      _content.SetActive(false);
    }

    private void OnButtonClick() => Clicked?.Invoke(this);
  }
}