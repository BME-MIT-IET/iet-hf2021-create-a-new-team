using Microsoft.VisualStudio.TestTools.UnitTesting;
using RDFSharp.Model;
using RDFSharp.Semantics.OWL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RDFSharp.UnitTests
{
    [TestClass]
    public class RDFOntologyLiteralTest
    {

        [TestMethod]
        public void CreateOntologyLiteral_Success()
        {
            //Arrange
            var literal = new RDFPlainLiteral("Garfield");

            //Act
            var ontologyLiteral = new RDFOntologyLiteral(literal);
            
            //Assert
            Assert.AreEqual(ontologyLiteral.Value, literal);
        }

        [TestMethod]
        [ExpectedException(typeof(RDFSemanticsException))]
        public void CreateOntologyLiteral_Failure()
        {
            //Arrange
            RDFPlainLiteral literal = null;

            //Act
            var ontologyLiteral = new RDFOntologyLiteral(literal);


        }
    }
}
