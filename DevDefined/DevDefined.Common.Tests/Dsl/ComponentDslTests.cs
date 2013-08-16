using System;
using System.Collections.Generic;
using DevDefined.Common.Dsl;
using NUnit.Framework;

namespace DevDefined.Common.Tests.Dsl
{
    [TestFixture]
    public class ComponentDslTests
    {
        [Test]
        public void CreateComponentDsl()
        {
            // not so much a test, as an example of how you could construct a
            // "DSL" like syntax to represent a monorail view component with 
            // section overrides.

            var people = new List<Person>
                             {
                                 new Person {FirstName = "Alex", LastName = "Henderson"},
                                 new Person {FirstName = "Joe", LastName = "Bloggs"}
                             };

            var compDsl = new ComponentDsl();

            compDsl.Add
                (
                GridComponent => compDsl.Component
                                     (
                                     compDsl.Parameters
                                         (
                                         source => people
                                         ),
                                     header => compDsl.Section
                                                   (
                                                   tr => compDsl.As
                                                             (
                                                             th => compDsl.As
                                                                       (
                                                                       compDsl.Text("Names")
                                                                       )
                                                             )
                                                   ),
                                     item => compDsl.Section
                                                 (
                                                 tr => compDsl.As
                                                           (
                                                           td => compDsl.As
                                                                     (
                                                                     compDsl.Item<Person>(p => p.FirstName + " " + p.LastName)
                                                                     )
                                                           )
                                                 )
                                     )
                );

            var dsl = new StandardDsl();

            dsl.Add
                (
                html => dsl.As
                            (
                            body => compDsl
                            )
                );

            Console.WriteLine(DslToXml.ToXml(dsl));
        }
    }
}