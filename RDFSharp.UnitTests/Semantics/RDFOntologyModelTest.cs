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

        RDFOntologyModel model = null;
        RDFOntologyClass siberian = null;
        RDFOntologyClass persian = null;
        RDFOntologyModel new_model = null;


        [TestInitialize]
        public void init()
        {
            model = new RDFOntologyModel();
            siberian = new RDFOntologyClass(new RDFResource("http://ont/classes/siberian"));
            persian = new RDFOntologyClass(new RDFResource("http://ont/classes/persian"));
            new_model = new RDFOntologyModel();
        }


        [TestMethod]
        public void UnionWith_hasSameNumberOfClassesAndProperties() {
            //Arrange
            model.ClassModel.AddClass(siberian);          
            var hasName = new RDFOntologyDatatypeProperty(new RDFResource("http://ont/props/hasName"));         
            model.PropertyModel.AddProperty(hasName);

            var new_model = new RDFOntologyModel();
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
            model.ClassModel.AddClass(siberian);
            model.ClassModel.AddClass(persian);
            var hasName = new RDFOntologyDatatypeProperty(new RDFResource("http://ont/props/hasName"));
            model.PropertyModel.AddProperty(hasName);
            
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
            model.ClassModel.AddClass(siberian);
            model.ClassModel.AddClass(persian);
            var hasName = new RDFOntologyDatatypeProperty(new RDFResource("http://ont/props/hasName"));
            model.PropertyModel.AddProperty(hasName);         

            var new_model = new RDFOntologyModel();
            new_model.ClassModel.AddClass(persian);

            //Act
            var difference_model = model.DifferenceWith(new_model);

            //Assert
            Assert.AreEqual(1, difference_model.ClassModel.ClassesCount);
            Assert.AreEqual(1, difference_model.PropertyModel.DatatypePropertiesCount);
        }
    }
}
