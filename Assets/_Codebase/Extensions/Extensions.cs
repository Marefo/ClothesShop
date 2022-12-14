using System.Collections.Generic;
using _Codebase.Logic.Data;
using Random = UnityEngine.Random;
using SystemRandom = System.Random;

namespace _Codebase.Extensions
{
  public static class Extensions
  {
    private static readonly SystemRandom rng = new SystemRandom(); 
    
    public static float GetRandomValue(this Range range) => Random.Range(range.Min, range.Max + 1);
    
    public static bool AddIfNotExists<T>(this List<T> list, T value)
    {
      if (list.Contains(value)) return false;
			
      list.Add(value);
      return true;
    }
    
    public static T GetRandomValue<T>(this List<T> list) => list[Random.Range(0, list.Count)];
		
    public static List<T> Shuffle<T>(this List<T> list)
    {
      List<T> result = new List<T>(list);
      int n = result.Count;  
      while (n > 1) {  
        n--;  
        int k = rng.Next(n + 1);
        (result[k], result[n]) = (result[n], result[k]);
      }
      return result;
    }
  }
}