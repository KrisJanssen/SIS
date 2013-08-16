using System.Collections.Generic;
using System.Linq;
using DevDefined.Common.Collections;
using NUnit.Framework;

namespace DevDefined.Common.Tests.Collections
{
  [TestFixture]
  public class RingBufferTests
  {
    [Test]
    public void AddItemsToBufferOfSetSize_IncreasesSizeUntilFull()
    {
      var buffer = new RingBuffer<string>(2);

      Assert.AreEqual(0, buffer.Count);

      buffer.Add("1");

      Assert.AreEqual(1, buffer.Count);

      buffer.Add("2");

      Assert.AreEqual(2, buffer.Count);

      buffer.Add("3");

      Assert.AreEqual(2, buffer.Count);
    }

    [Test]
    public void AddItemsToBufferOfSetSize_RemovesItemsFirstAddedToBufferToFitInMoreItems()
    {
      var buffer = new RingBuffer<string>(2);
      buffer.Add("1");
      buffer.Add("2");

      List<string> items = buffer.ToList();
      Assert.AreEqual("1", items[0]);
      Assert.AreEqual("2", items[1]);

      buffer.Add("3");
      items = buffer.ToList();
      Assert.AreEqual("2", items[0]);
      Assert.AreEqual("3", items[1]);

      buffer.Add("4");
      items = buffer.ToList();
      Assert.AreEqual("3", items[0]);
      Assert.AreEqual("4", items[1]);

      buffer.Add("5");
      items = buffer.ToList();
      Assert.AreEqual("4", items[0]);
      Assert.AreEqual("5", items[1]);
    }
  }
}