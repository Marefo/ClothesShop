using DG.Tweening;
using UnityEngine;

namespace _Codebase.Logic
{
  public class Showable : MonoBehaviour
  {
    [SerializeField] private Transform _visual;

    private const float ScaleTime = 0.25f;

    public void Show()
    {
      _visual.DOKill();
      _visual.localScale = Vector3.zero;
      _visual.DOScale(Vector3.one, ScaleTime).SetLink(gameObject);
    }

    public void Hide()
    {
      _visual.DOKill();
      _visual.DOScale(Vector3.zero, ScaleTime).SetLink(gameObject);
    }
  }
}