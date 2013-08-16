using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevDefined.Common.Reflection;
using NUnit.Framework;

namespace DevDefined.Common.Tests.Reflection
{
  public static class StaticValueHolder
  {
    public static readonly string TheValue = "Success";
  }

  [TestFixture]
  public class StaticReflectionHelperTests
  {
    [Test]
    public void GetStaticValue_FromStaticClassInAssembly()
    {
      Assert.AreEqual(StaticValueHolder.TheValue,
                      StaticReflectionHelper.GetStaticValue<string>("DevDefined.Common.Tests.Reflection.StaticValueHolder.TheValue, DevDefined.Common.Tests"));
    }
  }
}