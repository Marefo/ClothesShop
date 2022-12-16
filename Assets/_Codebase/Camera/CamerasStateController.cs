using UnityEngine;

namespace _Codebase.Camera
{
  public class CamerasStateController : MonoBehaviour
  {
    [SerializeField] private Animator _animator;
    
    private readonly int _zoomHash = Animator.StringToHash("Zoom");

    public void ZoomIn() => _animator.SetBool(_zoomHash, true);
    public void ZoomOut() => _animator.SetBool(_zoomHash, false);
  }
}