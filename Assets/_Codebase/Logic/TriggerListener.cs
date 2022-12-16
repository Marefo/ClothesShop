using System;
using UnityEngine;

namespace _Codebase.Logic
{
  [RequireComponent(typeof(Collider2D))]
  public class TriggerListener : MonoBehaviour
  {
    public event Action<Collider2D> Entered;
    public event Action<Collider2D> Canceled;

    private void OnTriggerEnter2D(Collider2D other) => Entered?.Invoke(other);
    private void OnTriggerExit2D(Collider2D other) => Canceled?.Invoke(other);
  }
}