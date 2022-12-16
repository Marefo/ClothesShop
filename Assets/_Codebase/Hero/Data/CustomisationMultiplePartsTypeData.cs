using System;
using System.Collections.Generic;

namespace _Codebase.Hero.Data
{
  [Serializable]
  public class CustomisationMultiplePartsTypeData
  {
    public CustomisationPartType CustomisationPartType;
    public List<CustomisationPartData> CustomisationPartsData;
  }
}