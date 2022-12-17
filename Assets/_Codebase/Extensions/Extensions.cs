using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace _Codebase.Extensions
{
  public static class Extensions
  {
    public static bool AddIfNotExists<T>(this List<T> list, T value)
    {
      if (list.Contains(value)) return false;
			
      list.Add(value);
      return true;
    }
  }
}