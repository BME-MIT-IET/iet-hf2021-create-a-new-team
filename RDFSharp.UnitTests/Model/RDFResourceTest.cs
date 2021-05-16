using Microsoft.VisualStudio.TestTools.UnitTesting;
using RDFSharp.Model;
using System;
using System.Text.RegularExpressions;

namespace RDFSharp.UnitTests
{
    [TestClass]
    public class RDFResourceTest
    {
        private string validURIString = TestRDFObjectProvider.validURIStrings[0];
        private string invalidURIString = TestRDFObjectProvider.invalidURIString;
        private Regex blankNodeStringRegex = TestRDFObjectProvider.blankNodeRegex;

        [TestMethod]
        public void RDFResource_ValidURI_ReturnsSameWithToString()
        {
            //Arrange
            var rdf = new RDFResource(validURIString);

            //Act
            var result = rdf.ToString();

            //Assert
            Assert.AreEqual(validURIString, result);
        }

        [TestMethod]
        public void RDFResource_BlankURI_ReturnsBnodeWithToString()
        {
            //Arrange
            var rdf = new RDFResource();

            //Act
            var result = rdf.ToString();

            //Assert
            StringAssert.Matches(result, blankNodeStringRegex);
        }

        [TestMethod]
        public void RDFResource_InvalidURI_ThrowsRDFModelException()
        {
            //Arrange - Act
            Action actual = () => new RDFResource(invalidURIString);

            //Assert
            Assert.ThrowsException<RDFModelException>(actual);
        }
    }
}
