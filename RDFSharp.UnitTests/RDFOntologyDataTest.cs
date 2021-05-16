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
    public class RDFOntologyDataTest
    {

        RDFOntology ont = null;
        RDFOntologyFact ontologyFact = null;
        RDFOntologyLiteral ontologyLiteral = null;
        RDFOntologyFact ontologyFact2 = null;
        RDFOntologyLiteral ontologyLiteral2 = null;

        [TestInitialize]
        public void init()
        {
            ont = new RDFOntology(new RDFResource("http://ont/"));
            ontologyFact = new RDFOntologyFact(new RDFResource("http://ont/facts/garfield"));
            ontologyLiteral = new RDFOntologyLiteral(new RDFPlainLiteral("Garfield"));
            ontologyFact2 = new RDFOntologyFact(new RDFResource("http://ont/facts/john"));
            ontologyLiteral2 = new RDFOntologyLiteral(new RDFPlainLiteral("John"));
        }


        [TestMethod]
        public void Add_ExistingFact_Failure() {
            //Arrange

            
            //Act
            ont.Data.AddFact(ontologyFact)
                    .AddFact(ontologyFact);

            //Assert
            Assert.AreEqual(1, ont.Data.FactsCount);
        }


        [TestMethod]
        public void Add_ExistingLiteral_Failure()
        {
            //Arrange


            //Act
            ont.Data.AddLiteral(ontologyLiteral)
                    .AddLiteral(ontologyLiteral);


            //Assert
            Assert.AreEqual(1, ont.Data.LiteralsCount);
        }

        [TestMethod]
        public void Add_SameAs_Relation_Success() {
            //Arrange
            var differentOntologyFact = new RDFOntologyFact(new RDFResource("http://ont/facts/orangeFunnyCat"));

            ont.Data.AddFact(ontologyFact)
                    .AddFact(differentOntologyFact);

            //Act
            ont.Data.AddSameAsRelation(differentOntologyFact, ontologyFact);

            //Assert
            Assert.IsTrue(ont.Data.CheckIsSameFactAs(ontologyFact, differentOntologyFact));

        }

        [TestMethod]
        public void Add_DifferentFrom_Relation_Success() {
            //Arrange

            ont.Data.AddFact(ontologyFact)
                    .AddFact(ontologyFact2);

            //Act
            ont.Data.AddDifferentFromRelation(ontologyFact, ontologyFact2);

            //Assert
            Assert.IsTrue(ont.Data.CheckIsDifferentFactFrom(ontologyFact, ontologyFact2));
        }

        [TestMethod]
        public void Add_Assertion_Success() {
            //Arrange
            var hasName = new RDFOntologyDatatypeProperty(new RDFResource("http://ont/props/hasName"));
            ont.Data.AddFact(ontologyFact);
            ont.Model.PropertyModel.AddProperty(hasName);

            //Act
            ont.Data.AddAssertionRelation(ontologyFact, hasName, ontologyLiteral);

            //Assert
            Assert.IsTrue(ont.Data.CheckIsAssertion(ontologyFact, hasName, ontologyLiteral));
        }

        [TestMethod]
        public void RemoveFact_Success() {
            //Arrange
            ont.Data.AddFact(ontologyFact)
                    .AddFact(ontologyFact2);

            //Act
            ont.Data.RemoveFact(ontologyFact);

            //Assert
            Assert.AreEqual(1, ont.Data.FactsCount);
        }


        [TestMethod]
        public void RenameFact_Success() {
            //Arrange
            ont.Data.AddFact(ontologyFact)
                    .AddFact(ontologyFact2);
            var newOntologyFact = new RDFOntologyFact(new RDFResource("http://ont/facts/odie"));

            //Act
            ont.Data.RenameFact(ontologyFact2, newOntologyFact);

            //Assert
            var renamedFact = ont.Data.SelectFact("http://ont/facts/odie");
            Assert.AreEqual(renamedFact, newOntologyFact);
        }

        [TestMethod]
        public void Remove_Literal_Success()
        {
            //Arrange
            ont.Data.AddLiteral(ontologyLiteral)
                    .AddLiteral(ontologyLiteral2);

            //Act
            ont.Data.RemoveLiteral(ontologyLiteral);

            //Assert
            Assert.AreEqual(1, ont.Data.LiteralsCount);
        }

        [TestMethod]
        public void Remove_NotExistingLiteral_Failure()
        {
            //Arrange
            ont.Data.AddLiteral(ontologyLiteral);
                
            //Act
            ont.Data.RemoveLiteral(ontologyLiteral2);

            //Assert
            Assert.AreEqual(1, ont.Data.LiteralsCount);
        }

        [TestMethod]
        public void Remove_SameAs_Relation_Success()
        {
            //Arrange
            var differentOntologyFact = new RDFOntologyFact(new RDFResource("http://ont/facts/orangeFunnyCat"));
            ont.Data.AddFact(ontologyFact)
                    .AddFact(differentOntologyFact);
            ont.Data.AddSameAsRelation(differentOntologyFact, ontologyFact);

            //Act
            ont.Data.RemoveSameAsRelation(differentOntologyFact, ontologyFact);

            //Assert
            Assert.IsFalse(ont.Data.CheckIsSameFactAs(ontologyFact, differentOntologyFact));

        }

        [TestMethod]
        public void Remove_Assertion_Success()
        {
            //Arrange
            var hasName = new RDFOntologyDatatypeProperty(new RDFResource("http://ont/props/hasName"));
            ont.Data.AddFact(ontologyFact);
            ont.Data.AddFact(ontologyFact2);
            ont.Model.PropertyModel.AddProperty(hasName);
            ont.Data.AddAssertionRelation(ontologyFact, hasName, ontologyLiteral);
            ont.Data.AddAssertionRelation(ontologyFact2, hasName, ontologyLiteral2);

            //Act
            ont.Data.RemoveAssertionRelation(ontologyFact, hasName, ontologyLiteral);

            //Assert
            Assert.IsFalse(ont.Data.CheckIsAssertion(ontologyFact, hasName, ontologyLiteral));
        }

        [TestMethod]
        public void SelectLiteral_Success() {
            //Arrange
            ont.Data.AddLiteral(ontologyLiteral);
            ont.Data.AddLiteral(ontologyLiteral2);

            //Act
            var selectedLiteral = ont.Data.SelectLiteral("Garfield");

            //Assert
            Assert.AreEqual(selectedLiteral, ontologyLiteral);
        }

        [TestMethod]
        public void SelectNotExistingLiteral_Failure() {
            //Arrange
            ont.Data.AddLiteral(ontologyLiteral);
            ont.Data.AddLiteral(ontologyLiteral2);

            //Act
            var selectedLiteral = ont.Data.SelectLiteral("Odie");

            //Assert
            Assert.IsNull(selectedLiteral);
        }

        [TestMethod]
        public void SelectFact_Success() {
            //Arrange
            ont.Data.AddFact(ontologyFact)
                    .AddFact(ontologyFact2);

            //Act
            var selectedFact = ont.Data.SelectFact("http://ont/facts/garfield");

            //Assert
            Assert.AreEqual(selectedFact, ontologyFact);
        }

    }
}
