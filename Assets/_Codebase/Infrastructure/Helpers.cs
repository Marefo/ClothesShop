using System;
using System.Collections.Generic;
using System.Linq;
using _Codebase.Hero.Data;

namespace _Codebase.Infrastructure
{
  public static class Helpers
  {
    public static List<T> EnumToList<T>() => Enum.GetValues(typeof(T)).Cast<T>().ToList();
  }
}