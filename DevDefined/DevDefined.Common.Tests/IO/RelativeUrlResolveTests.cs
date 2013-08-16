using System;
using DevDefined.Common.IO;
using NUnit.Framework;

namespace DevDefined.Common.Tests.IO
{
  [TestFixture]
  public class RelativeUrlResolveTests
  {
    [Test]
    public void DontResolveForNonRelative()
    {
      var resolver = new CssRelativeUrlResolver(@"c:\inetpub\wwwroot\", new Uri("http://localhost/myapp/"));
      string output = resolver.Resolve(@"c:\inetpub\wwwroot\content\css\main.css", ".icon { background-image: url(http://CDN/test.png) }");
      Assert.AreEqual(".icon { background-image: url(http://CDN/test.png) }", output);
    }

    [Test]
    public void ResolveForRelative()
    {
      var resolver = new CssRelativeUrlResolver(@"c:\inetpub\wwwroot\", new Uri("http://localhost/myapp/"));
      string output = resolver.Resolve(@"c:\inetpub\wwwroot\content\css\main.css", @".icon { background-image: url(../images/test.png) }");
      Assert.AreEqual(".icon { background-image: url(http://localhost/myapp/images/test.png) }", output);
    }

    [Test]
    public void ResolveForMultipleUrlsRelative()
    {
      var resolver = new CssRelativeUrlResolver(@"c:\inetpub\wwwroot\", new Uri("http://localhost/myapp/"));
      string output = resolver.Resolve(@"c:\inetpub\wwwroot\content\css\main.css", @".icon1 { background-image: url(../images/test1.png) }, .icon2 { background-image: url(../images/icons/test2.png) }");
      Assert.AreEqual(".icon1 { background-image: url(http://localhost/myapp/images/test1.png) }, .icon2 { background-image: url(http://localhost/myapp/images/icons/test2.png) }", output);
    }
  }
}