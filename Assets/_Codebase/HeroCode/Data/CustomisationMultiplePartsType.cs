using System;
using System.Collections.Generic;

namespace _Codebase.HeroCode.Data
{
  [Serializable]
  public class CustomisationMultiplePartsType
  {
    public CustomisationPartType CustomisationPartType;
    public List<CustomisationPartData> CustomisationPartsData;
  }
}