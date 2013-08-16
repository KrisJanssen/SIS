using System;
using System.Collections.Generic;
using System.Linq;
using DevDefined.Common.Extensions.Annotations;
using NUnit.Framework;

namespace DevDefined.Common.Tests.Extensions.Annotations
{
    [TestFixture]
    public class AnnotationTests
    {
        #region Setup/Teardown

        [SetUp]
        public void ForceCollection()
        {
            GC.Collect();
        }

        #endregion

        [Test]
        public void AccessAnnotationDirectly()
        {
            var target = new ClassA();

            target.Annotation()["Tags"] = new[] {"C#", "Tests"};

            CollectionAssert.AreEqual(target.Annotation<string[]>("Tags"), new[] {"C#", "Tests"});
        }

        [Test]
        public void AnnotateClass()
        {
            var target = new ClassA();
            target.Annotate(Description => "instance we are testing");
            Assert.AreEqual("instance we are testing", target.Annotation<string>("Description"));
        }

        [Test]
        public void AnnotateDifferentInstances()
        {
            var target1 = new ClassA();
            var target2 = new ClassA();

            target1.Annotate(Description => "class number 1");
            target2.Annotate(Description => "class number 2");

            Assert.AreEqual("class number 1", target1.Annotation<string>("Description"));
            Assert.AreEqual("class number 2", target2.Annotation<string>("Description"));
        }

        [Test]
        [ExpectedException(ExpectedMessage = "The selected member does not belong to the declaring type \"DevDefined.Common.Tests.ClassB\"")]
        public void AnnotateNonOwnedProperty()
        {
            var targetA = new ClassA();
            var targetB = new ClassB();
            targetA.Annotate(() => targetB.FirstName, Suffix => "Mr");
            Assert.AreEqual("Mr", targetA.Annotation<string>(() => targetB.FirstName, "Suffix"));
        }

        [Test]
        public void AnnotateProperty()
        {
            var target = new ClassA();
            target.Annotate(() => target.FirstName, Suffix => "Mr");
            Assert.AreEqual("Mr", target.Annotation<string>(() => target.FirstName, "Suffix"));
        }

        [Test]
        public void QueryStoreForClassAnnotationsWithCertainKey()
        {
            var target1 = new ClassA();
            var target2 = new ClassA();
            var target3 = new ClassA();

            target1.Annotate(Description => "class number 1");
            target2.Annotate(Description => "class number 2");
            target3.Annotate(Parsed => true);

            List<ClassAnnotation> results = AnnotationStore.Classes
                .Where(a => a.HasKey("Description"))
                .ToList();

            Assert.AreEqual(2, results.Count);
        }

        [Test]
        public void QueryStoreForMemberAnnotations()
        {
            var target1 = new ClassA();
            var target2 = new ClassA();
            var target3 = new ClassA();

            target1.Annotate(() => target1.FirstName, CamelCase => true); // annotating a property
            target1.Annotate(() => target1.Field, Ignored => true); // annotating a field
            target2.Annotate(() => target2.Execute(), Parsed => true); // annotating a method

            target3.Annotate(Parsed => true);

            List<MemberAnnotation> results = AnnotationStore.Members
                .Where(p => p.HasKey("CamelCase"))
                .ToList();

            Assert.AreEqual(1, results.Count);
        }
    }
}