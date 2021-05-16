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
    public class RDFOntologyTest
    {
        [TestMethod]
        public void Ctor_WithResourceAndModel()
        {
            //Assert
            var resource = new RDFResource("http://ont/");
            var model = new RDFOntologyModel();
            var siberian = new RDFOntologyClass(new RDFResource("http://ont/classes/siberian"));
            model.ClassModel.AddClass(siberian);
            var data = new RDFOntologyData();
            var fact = new RDFOntologyFact(new RDFResource("http://ont/facts/garfield"));
            data.AddFact(fact);


            //Act
            var ont = new RDFOntology(resource, model, data, null);

            //Assert
            Assert.AreEqual(fact, ont.Data.SelectFact("http://ont/facts/garfield"));
            Assert.AreEqual(siberian, ont.Model.ClassModel.SelectClass("http://ont/classes/siberian"));
        }

        [TestMethod]
        public void UnionWith_hasSumNumberOfModelAndData()
        {
            //Arrange
            var ont = new RDFOntology(new RDFResource("http://ont/"));
            var siberian = new RDFOntologyClass(new RDFResource("http://ont/classes/siberian"));
            ont.Model.ClassModel.AddClass(siberian);
            var hasName = new RDFOntologyDatatypeProperty(new RDFResource("http://ont/props/hasName"));
            ont.Model.PropertyModel.AddProperty(hasName);
            var ontologyFact = new RDFOntologyFact(new RDFResource("http://ont/facts/garfield"));
            ont.Data.AddFact(ontologyFact);

            var new_ont = new RDFOntology(new RDFResource("http://new_ont/"));
            var persian = new RDFOntologyClass(new RDFResource("http://ont/classes/persian"));
            new_ont.Model.ClassModel.AddClass(persian);
            var hasNote = new RDFOntologyAnnotationProperty(new RDFResource("http://ont/props/hasNote"));
            new_ont.Model.PropertyModel.AddProperty(hasNote);

            //Act
            var union_ont = ont.UnionWith(new_ont);

            //Assert
            Assert.AreEqual(2, union_ont.Model.ClassModel.ClassesCount);
            Assert.AreEqual(1, union_ont.Model.PropertyModel.AnnotationPropertiesCount);
            Assert.AreEqual(1, union_ont.Model.PropertyModel.DatatypePropertiesCount);
            Assert.AreEqual(1, union_ont.Data.FactsCount);
        }

        [TestMethod]
        public void IntersectWith_hasCommonModelAndDataOnly()
        {
            //Arrange
            var ont = new RDFOntology(new RDFResource("http://ont/"));
            var siberian = new RDFOntologyClass(new RDFResource("http://ont/classes/siberian"));
            ont.Model.ClassModel.AddClass(siberian);
            var hasName = new RDFOntologyDatatypeProperty(new RDFResource("http://ont/props/hasName"));
            ont.Model.PropertyModel.AddProperty(hasName);
            var persian = new RDFOntologyClass(new RDFResource("http://ont/classes/persian"));
            ont.Model.ClassModel.AddClass(persian);

            var new_ont = new RDFOntology(new RDFResource("http://new_ont/"));
            new_ont.Model.ClassModel.AddClass(siberian);
            new_ont.Model.ClassModel.AddClass(persian);

            //Act
            var common_ont = ont.IntersectWith(new_ont);

            //Assert
            Assert.AreEqual(2, common_ont.Model.ClassModel.ClassesCount);
            Assert.AreEqual(0, common_ont.Model.PropertyModel.DatatypePropertiesCount);
        }

        [TestMethod]
        public void DifferenceWith_hasDifferentModelAndDataOnly()
        {
            //Arrange
            var ont = new RDFOntology(new RDFResource("http://ont/"));
            var siberian = new RDFOntologyClass(new RDFResource("http://ont/classes/siberian"));
            ont.Model.ClassModel.AddClass(siberian);
            var hasName = new RDFOntologyDatatypeProperty(new RDFResource("http://ont/props/hasName"));
            ont.Model.PropertyModel.AddProperty(hasName);
            var persian = new RDFOntologyClass(new RDFResource("http://ont/classes/persian"));
            ont.Model.ClassModel.AddClass(persian);
            var ontologyFact = new RDFOntologyFact(new RDFResource("http://ont/facts/garfield"));
            ont.Data.AddFact(ontologyFact);

            var new_ont = new RDFOntology(new RDFResource("http://new_ont/"));
            new_ont.Model.ClassModel.AddClass(persian);

            //Act
            var difference_ont = ont.DifferenceWith(new_ont);

            //Assert
            Assert.AreEqual(1, difference_ont.Model.ClassModel.ClassesCount);
            Assert.AreEqual(1, difference_ont.Model.PropertyModel.DatatypePropertiesCount);
            Assert.AreEqual(1, difference_ont.Data.FactsCount);
        }


        [TestMethod]
        public void Merge_hasSumNumberOfModelAndData()
        {
            //Arrange
            var ont = new RDFOntology(new RDFResource("http://ont/"));
            var siberian = new RDFOntologyClass(new RDFResource("http://ont/classes/siberian"));
            ont.Model.ClassModel.AddClass(siberian);
            var hasName = new RDFOntologyDatatypeProperty(new RDFResource("http://ont/props/hasName"));
            ont.Model.PropertyModel.AddProperty(hasName);
            var ontologyFact = new RDFOntologyFact(new RDFResource("http://ont/facts/garfield"));
            ont.Data.AddFact(ontologyFact);

            var new_ont = new RDFOntology(new RDFResource("http://new_ont/"));
            var persian = new RDFOntologyClass(new RDFResource("http://ont/classes/persian"));
            new_ont.Model.ClassModel.AddClass(persian);
            var hasNote = new RDFOntologyAnnotationProperty(new RDFResource("http://ont/props/hasNote"));
            new_ont.Model.PropertyModel.AddProperty(hasNote);

            //Act
            ont.Merge(new_ont);

            //Assert
            Assert.AreEqual(2, ont.Model.ClassModel.ClassesCount);
            Assert.AreEqual(1, ont.Model.PropertyModel.AnnotationPropertiesCount);
            Assert.AreEqual(1, ont.Model.PropertyModel.DatatypePropertiesCount);
            Assert.AreEqual(1, ont.Data.FactsCount);
        }

    }
}
