using _Codebase.Audio;
using UnityEngine;

namespace _Codebase.Services
{
  public class AudioService : MonoBehaviour
  {
    [field: SerializeField] public SfxData SfxData { get; private set; }
    [field: Space(10)] 
    [SerializeField] private AudioSource _audioSource;

    public void Play(AudioClip clip) => _audioSource.PlayOneShot(clip);
  }
}