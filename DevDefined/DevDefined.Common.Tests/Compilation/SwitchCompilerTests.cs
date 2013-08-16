using System;
using System.Collections.Generic;
using DevDefined.Common.Compilation;
using NUnit.Framework;

namespace DevDefined.Common.Tests.Compilation
{
    [TestFixture]
    public class SwitchCompilerTests
    {
        [Test]
        public void SimpleTestFromJomoFishersSite()
        {
            var dict = new Dictionary<string, int>
                           {
                               {"happy", 9},
                               {"sneezy", 2},
                               {"doc", 7},
                               {"sleepy", 8},
                               {"dopey", 9},
                               {"grumpy", 2},
                               {"bashful", 6}
                           };

            Func<string, int> Lookup = SwitchCompiler.CreateSwitch(dict);

            Assert.AreEqual(7, Lookup("doc"));
        }
    }
}