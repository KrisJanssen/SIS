using System;
using System.Collections.Generic;
using DevDefined.Common.Dsl;
using NUnit.Framework;

namespace DevDefined.Common.Tests.Dsl
{
    [TestFixture]
    public class StandardDslTests
    {
        [Test]
        public void CreateDslWithForEach()
        {
            var headers = new List<string> {"header1", "header2", "header3"};

            var innerDsl = new StandardDsl();
            innerDsl.Add(
                headers.ForEach(text =>
                                td => innerDsl.Text(text)
                    )
                );

            var dsl = new StandardDsl();

            dsl.Add(
                table => dsl.As(
                             tr => dsl.As(innerDsl)));


            Console.WriteLine(DslToXml.ToXml(dsl));
        }

        [Test]
        public void CreateDslWithInnerDsl()
        {
            var innerDsl = new StandardDsl();
            innerDsl.Add
                (
                innerDsl.Text("header1")
                );

            var dsl = new StandardDsl();

            dsl.Add(
                table => dsl.As(
                             tr => dsl.As(
                                       td => innerDsl,
                                       td => dsl.Text("header2"))));

            string expected = @"<?xml version=""1.0"" encoding=""utf-16""?><table><tr><td>header1</td><td>header2</td></tr></table>";

            Assert.AreEqual(expected, DslToXml.ToXml(dsl));
        }

        [Test]
        public void CreateSimpleDsl()
        {
            var dsl = new StandardDsl();

            dsl.Add
                (
                table => dsl.As
                             (
                             tr => dsl.As
                                       (
                                       td => dsl.Text("header1"),
                                       td => dsl.Text("header2")
                                       )
                             )
                );

            string expected = @"<?xml version=""1.0"" encoding=""utf-16""?><table><tr><td>header1</td><td>header2</td></tr></table>";

            Assert.AreEqual(expected, DslToXml.ToXml(dsl));
        }
    }
}