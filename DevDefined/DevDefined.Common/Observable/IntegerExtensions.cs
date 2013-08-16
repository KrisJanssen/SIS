using System;

namespace DevDefined.Common.Observable
{
  public static class IntegerExtensions
  {
    public static TimeSpan Milliseconds(this int milliseconds)
    {
      return new TimeSpan(0, 0, 0, 0, milliseconds);
    }
  }
}