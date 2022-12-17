using _CodeBase.Logging;
using _Codebase.Services;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace _Codebase.Logic
{
  public class Showable : MonoBehaviour
  {
    public bool Visible => new Vector2(_visual.localScale.x, _visual.localScale.y) != Vector2.zero;
    
    [SerializeField] private Transform _visual;
    [SerializeField] private bool _withAudio;
    [SerializeField, ShowIf(nameof(_withAudio))] private AudioService _audioService;

    private const float ScaleTime = 0.25f;

    public void Show()
    {
      if(Visible) return;
      
      _visual.DOKill();
      _visual.localScale = Vector3.zero;
      _visual.DOScale(Vector3.one, ScaleTime).SetLink(gameObject);
      
      if(_withAudio)
        _audioService.Play(_audioService.SfxData.OpenPanel);
    }

    public void Hide()
    {
      if(Visible == false) return;
      
      _visual.DOKill();
      _visual.DOScale(Vector3.zero, ScaleTime).SetLink(gameObject);
      
      if(_withAudio)
        _audioService.Play(_audioService.SfxData.ClosePanel);
    }
  }
}