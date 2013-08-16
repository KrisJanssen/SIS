using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DevDefined.Common.Reflection
{
  public static class StaticReflectionHelper
  {
    /// <summary>
    /// Retrieves a static value from a class, given it's path i.e.
    /// assuming the type is "DevDefined.Common.ExampleClass" in the
    /// assembly "DevDefined.Common" and with the static field called
    /// "SomeValue" the value passed should be:
    /// 
    /// DevDefined.Common.ExampleClass.SomeValue, DevDefined.Common
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value"></param>
    /// <returns></returns>
    public static T GetStaticValue<T>(string value)
    {
      string assembly = null;
      string type = null;

      if (value.Contains(","))
      {
        int lastCommaIndex = value.LastIndexOf(",");
        assembly = value.Substring(lastCommaIndex);
        type = value.Substring(0, lastCommaIndex);
      }
      else
      {
        assembly = "";
        type = value;
      }

      int lastIndex = type.LastIndexOf(".");
      string typeName = type.Substring(0, lastIndex);
      string field = type.Substring(lastIndex + 1);
      Type ownerType = Type.GetType(typeName + assembly);
      FieldInfo valueField = ownerType.GetField(field);
      return (T)valueField.GetValue(null);
    }    
  }
}
