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
    public class RDFOntologyModelTest
    {
        [TestMethod]
        public void UnionWith_hasSameNumberOfClassesAndProperties() {
            //Arrange
            var model = new RDFOntologyModel();
            var siberian = new RDFOntologyClass(new RDFResource("http://ont/classes/siberian"));
            model.ClassModel.AddClass(siberian);          
            var hasName = new RDFOntologyDatatypeProperty(new RDFResource("http://ont/props/hasName"));         
            model.PropertyModel.AddProperty(hasName);

            var new_model = new RDFOntologyModel();
            var persian = new RDFOntologyClass(new RDFResource("http://ont/classes/persian"));
            new_model.ClassModel.AddClass(persian);
            var hasNote = new RDFOntologyAnnotationProperty(new RDFResource("http://ont/props/hasNote"));
            new_model.PropertyModel.AddProperty(hasNote);

            //Act
            var union_model = model.UnionWith(new_model);

            //Assert
            Assert.AreEqual(2, union_model.ClassModel.ClassesCount);
            Assert.AreEqual(1, union_model.PropertyModel.AnnotationPropertiesCount);
            Assert.AreEqual(1, union_model.PropertyModel.DatatypePropertiesCount);
        }

        [TestMethod]
        public void IntersectWith_hasSameNumberOfClasses()
        {
            //Arrange
            var model = new RDFOntologyModel();
            var siberian = new RDFOntologyClass(new RDFResource("http://ont/classes/siberian"));
            model.ClassModel.AddClass(siberian);
            var hasName = new RDFOntologyDatatypeProperty(new RDFResource("http://ont/props/hasName"));
            model.PropertyModel.AddProperty(hasName);
            var persian = new RDFOntologyClass(new RDFResource("http://ont/classes/persian"));
            model.ClassModel.AddClass(persian);

            var new_model = new RDFOntologyModel();
            new_model.ClassModel.AddClass(siberian);
            new_model.ClassModel.AddClass(persian);

            //Act
            var common_model = model.IntersectWith(new_model);

            //Assert
            Assert.AreEqual(2, common_model.ClassModel.ClassesCount);
            Assert.AreEqual(0, common_model.PropertyModel.DatatypePropertiesCount);
        }

        [TestMethod]
        public void DifferenceWith_hasSameNumberOfClasses()
        {
            //Arrange
            var ont = new RDFOntologyModel();
            var siberian = new RDFOntologyClass(new RDFResource("http://ont/classes/siberian"));
            ont.ClassModel.AddClass(siberian);
            var hasName = new RDFOntologyDatatypeProperty(new RDFResource("http://ont/props/hasName"));
            ont.PropertyModel.AddProperty(hasName);
            var persian = new RDFOntologyClass(new RDFResource("http://ont/classes/persian"));
            ont.ClassModel.AddClass(persian);

            var new_model = new RDFOntologyModel();
            new_model.ClassModel.AddClass(persian);

            //Act
            var difference_model = ont.DifferenceWith(new_model);

            //Assert
            Assert.AreEqual(1, difference_model.ClassModel.ClassesCount);
            Assert.AreEqual(1, difference_model.PropertyModel.DatatypePropertiesCount);
        }
    }
}
