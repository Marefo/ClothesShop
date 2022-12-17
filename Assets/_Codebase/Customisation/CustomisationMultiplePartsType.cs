using System;
using System.Collections.Generic;

namespace _Codebase.Customisation
{
  [Serializable]
  public class CustomisationMultiplePartsType
  {
    public CustomisationPartType CustomisationPartType;
    public List<CustomisationPartData> CustomisationPartsData;
    
    public CustomisationMultiplePartsType(CustomisationPartType customisationPartType, List<CustomisationPartData> customisationPartsData)
    {
      CustomisationPartType = customisationPartType;
      CustomisationPartsData = customisationPartsData;
    }
  }
}