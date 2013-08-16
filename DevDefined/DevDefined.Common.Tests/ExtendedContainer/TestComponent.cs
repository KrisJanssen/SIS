namespace DevDefined.Common.Tests.ExtendedContainer
{
  public class TestComponent
  {
    readonly string _value;

    public TestComponent(string value)
    {
      _value = value;
    }

    public string Value
    {
      get { return _value; }
    }
  }
}