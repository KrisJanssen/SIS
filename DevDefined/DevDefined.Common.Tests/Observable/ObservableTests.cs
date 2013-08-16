using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using NUnit.Framework;

namespace DevDefined.Common.Observable.Tests
{
  [TestFixture]
  public class ObservableTests
  {
    [Test]
    public void ApplySkip()
    {
      var input = new[] {"A", "B", "C"};

      var firstObserver = new List<string>();
      var secondObserver = new List<string>();

      using (var sink = new ObservableSink<string>())
      {
        sink.Subscribe(firstObserver.Add);
        sink.Skip(1).Subscribe(secondObserver.Add);
        sink.Pump(input);
      }

      CollectionAssert.AreEqual(input, firstObserver);
      CollectionAssert.AreEqual(new[] {"B", "C"}, secondObserver);
    }

    [Test]
    public void ApplyTake()
    {
      var input = new[] {"A", "B", "C"};

      var firstObserver = new List<string>();
      var secondObserver = new List<string>();

      using (var sink = new ObservableSink<string>())
      {
        sink.Subscribe(firstObserver.Add);
        sink.Take(2).Subscribe(secondObserver.Add);
        sink.Pump(input);
      }

      CollectionAssert.AreEqual(input, firstObserver);
      CollectionAssert.AreEqual(new[] {"A", "B"}, secondObserver);
    }

    [Test]
    public void ApplyWhere()
    {
      var input = new[] {"A", "B", "C"};

      var firstObserver = new List<string>();
      var secondObserver = new List<string>();

      using (var sink = new ObservableSink<string>())
      {
        sink.Subscribe(firstObserver.Add);
        sink.Where(s => s == "B").Subscribe(secondObserver.Add);
        sink.Pump(input);
      }

      CollectionAssert.AreEqual(input, firstObserver);
      CollectionAssert.AreEqual(new[] {"B"}, secondObserver);
    }

    [Test]
    public void Change()
    {
      string output = null;

      using (var sink = new ObservableSink<string>())
      {
        sink.ChangesOnly().All().Subscribe(vals => output = string.Join("", vals.ToArray()));
        sink.Pump("L", "L", "I", "I", "N", "Q", "Q");
      }

      Assert.AreEqual("LINQ", output);
    }

    [Test]
    public void Concat()
    {
      var outputs = new List<string>();

      using (var sink1 = new ObservableSink<string>())
      {
        using (var sink2 = new ObservableSink<string>())
        {
          sink1.Concat(sink2).Subscribe(outputs.Add);

          sink1.Pump("A");
          sink2.Pump("B");
          sink1.Pump("C");
          sink2.Pump("D");
        }
      }

      CollectionAssert.AreEqual(new[] {"A", "B", "C", "D"}, outputs);
    }

    [Test]
    public void Last()
    {
      string result = null;

      using (var sink = new ObservableSink<string>())
      {
        sink.Last().Subscribe(val => result = val);
        sink.Pump("A", "B", "C");
        Assert.IsNull(result);
      }

      Assert.AreEqual("C", result);
    }

    [Test]
    public void LimitRate()
    {
      var observer = new List<string>();

      using (var sink = new ObservableSink<string>())
      {
        sink.LimitRate(50.Milliseconds()).Subscribe(observer.Add);
        sink.Pump("A");
        Thread.Sleep(30);
        sink.Pump("B");
        Thread.Sleep(30);
        sink.Pump("C");
        Thread.Sleep(30);
        sink.Pump("D");
      }

      CollectionAssert.AreEqual(new[] {"A", "C"}, observer);
    }

    [Test]
    public void ObserveItemsPushedToSinkAreOrderdAndMultiCastedCorrectly()
    {
      var input = new[] {"A", "B", "C"};

      var firstObserver = new List<string>();
      var secondObserver = new List<string>();

      using (var sink = new ObservableSink<string>())
      {
        sink.Subscribe(new ActionObserver<string>(firstObserver.Add));
        sink.Subscribe(new ActionObserver<string>(secondObserver.Add));
        sink.Pump(input);
      }

      CollectionAssert.AreEqual(input, firstObserver);
      CollectionAssert.AreEqual(input, secondObserver);
    }

    [Test]
    public void Select()
    {
      var results = new List<int>();

      using (var sink = new ObservableSink<string>())
      {
        sink.Select(arg => Convert.ToInt32(arg)).Subscribe(results.Add);
        sink.Pump("1", "2", "3");
      }

      CollectionAssert.AreEqual(new[] {1, 2, 3}, results);
    }
  }

  public class DemoControl
  {
    public event MouseEventHandler MouseMove;

    public void OnMoveMouse(int x, int y)
    {
      if (MouseMove != null) MouseMove(this, new MouseEventArgs(MouseButtons.None, 0, x, y, 0));
    }
  }
}