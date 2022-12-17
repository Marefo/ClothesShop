using System;

namespace _Codebase.Logic.Data
{
  [Serializable]
  public class Range
  {
    public float Min;
    public float Max;

    public Range(float min, float max)
    {
      Min = min;
      Max = max;
    }
  }
}