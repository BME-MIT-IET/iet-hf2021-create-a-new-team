using Microsoft.VisualStudio.TestTools.UnitTesting;
using RDFSharp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RDFSharp.UnitTests
{
    [TestClass]
    public class RDFTripleTest
    {
        private RDFResource subjectResource = TestRDFObjectProvider.subjectResources[0];
        private RDFResource predicateResource = TestRDFObjectProvider.predicateResources[0];
        private RDFResource blankPredicateResource = new RDFResource();
        private RDFLiteral objectLiteral = TestRDFObjectProvider.objectLiterals[0];

        public string getDesiredOutputStatment(RDFResource subject, RDFResource predicate, RDFLiteral obj)
        {
            return subject.ToString() + " " + predicate.ToString() + " " + obj.ToString();
        }

        [TestMethod]
        public void RDFTriple_ValidResources_ReturnsSameWithToString()
        {
            //Arrange
            var rdf = new RDFTriple(subjectResource, predicateResource, objectLiteral);
            var desiredOutput = getDesiredOutputStatment(subjectResource, predicateResource, objectLiteral);

            //Act
            var result = rdf.ToString();
            Console.WriteLine(result);

            //Assert
            Assert.AreEqual(desiredOutput, result);
        }

        [TestMethod]
        public void RDFTriple_BlankResource_ThrowsRDFModelException()
        {
            //Arrange - Act
            Action actual = () => new RDFTriple(subjectResource, blankPredicateResource, objectLiteral);

            //Assert
            Assert.ThrowsException<RDFModelException>(actual);
        }
    }
}
