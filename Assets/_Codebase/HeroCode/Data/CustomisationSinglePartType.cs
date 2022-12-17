using System;

namespace _Codebase.HeroCode.Data
{
  [Serializable]
  public class CustomisationSinglePartType
  {
    public CustomisationPartType Type;
    public CustomisationPartData CustomisationPartData;

    public CustomisationSinglePartType(CustomisationPartType type, CustomisationPartData customisationPartData)
    {
      Type = type;
      CustomisationPartData = customisationPartData;
    }
  }
}