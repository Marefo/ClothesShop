using UnityEngine;

namespace _Codebase.Audio
{
  [CreateAssetMenu(fileName = "SfxData", menuName = "StaticData/SfxData")]
  public class SfxData : ScriptableObject
  {
    public AudioClip OpenPanel;
    public AudioClip ClosePanel;
    public AudioClip SuccessfulPurchase;
    public AudioClip FailedPurchase;
    public AudioClip Click;
  }
}