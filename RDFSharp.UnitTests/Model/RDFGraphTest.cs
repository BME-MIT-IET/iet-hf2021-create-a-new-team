using Microsoft.VisualStudio.TestTools.UnitTesting;
using RDFSharp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RDFSharp.UnitTests
{
    [TestClass]
    public class RDFGraphTest
    {
        private string defaultNamespaceString = RDFNamespaceRegister.DefaultNamespace.ToString();
        private List<RDFTriple> startingTriples = TestRDFObjectProvider.triples.GetRange(0, 2);
        private RDFTriple newTriple = TestRDFObjectProvider.triples[2];

        [TestMethod]
        public void RDFGraph_DefaultCtor_ReturnsDefaultNamespaceWithToString()
        {
            //Arrange
            var graph = new RDFGraph();

            //Act
            var result = graph.ToString();

            //Assert
            Assert.AreEqual(defaultNamespaceString, result);
        }

        [TestMethod]
        public void Iterate_CreatedFromListOfTriples_ReturnsSameTriples()
        {
            //Arrange
            var graph = new RDFGraph(startingTriples);
            var desiredStoredTriples = new List<RDFTriple>(startingTriples);
            var storedTriples = new List<RDFTriple>();

            //Act
            foreach(var triple in graph)
            {
                storedTriples.Add(triple);
            }

            //Assert
            CollectionAssert.AreEqual(desiredStoredTriples, storedTriples);
        }

        private void getGraphStoredTriples(RDFGraph graph, ref List<RDFTriple> outputList)
        {
            foreach (var triple in graph)
            {
                outputList.Add(triple);
            }
        }

        [TestMethod]
        public void AddTriple_AddNewValidTriple_InsertsNewTriple()
        {
            //Arrange
            var graph = new RDFGraph(startingTriples);
            var desiredStoredTriples = new List<RDFTriple>(startingTriples);
            desiredStoredTriples.Add(newTriple);
            var storedTriples = new List<RDFTriple>();

            //Act
            graph.AddTriple(newTriple);

            //Assert
            getGraphStoredTriples(graph, ref storedTriples);
            CollectionAssert.AreEqual(desiredStoredTriples, storedTriples);
        }

        [TestMethod]
        public void AddTriple_AddNullTriple_InsertsNothingWithoutException()
        {
            //Arrange
            var graph = new RDFGraph(startingTriples);
            var desiredStoredTriples = new List<RDFTriple>(startingTriples);
            var storedTriples = new List<RDFTriple>();

            //Act
            graph.AddTriple(null);

            //Assert
            getGraphStoredTriples(graph, ref storedTriples);
            CollectionAssert.AreEqual(desiredStoredTriples, storedTriples);
        }

        [TestMethod]
        public void AddTriple_AddTripleAlreadyInList_InsertsNothingWithoutException()
        {
            //Arrange
            var graph = new RDFGraph(startingTriples);
            var desiredStoredTriples = new List<RDFTriple>(startingTriples);
            var storedTriples = new List<RDFTriple>();

            //Act
            graph.AddTriple(startingTriples[0]);

            //Assert
            getGraphStoredTriples(graph, ref storedTriples);
            CollectionAssert.AreEqual(desiredStoredTriples, storedTriples);
        }

        [TestMethod]
        public void RemoveTriple_ValidTripleGiven_RemovesTripleFromGraph()
        {
            //Arrange
            var graph = new RDFGraph(startingTriples);
            var desiredStoredTriples = new List<RDFTriple>(startingTriples);
            desiredStoredTriples.Remove(startingTriples[0]);
            var storedTriples = new List<RDFTriple>();

            //Act
            graph.RemoveTriple(startingTriples[0]);

            //Assert
            getGraphStoredTriples(graph, ref storedTriples);
            CollectionAssert.AreEqual(desiredStoredTriples, storedTriples);
        }

        [TestMethod]
        public void RemoveTriple_TripleNotInGraph_DoesNothing()
        {
            //Arrange
            var graph = new RDFGraph(startingTriples);
            var desiredStoredTriples = new List<RDFTriple>(startingTriples);
            var storedTriples = new List<RDFTriple>();

            //Act
            graph.RemoveTriple(newTriple);

            //Assert
            getGraphStoredTriples(graph, ref storedTriples);
            CollectionAssert.AreEqual(desiredStoredTriples, storedTriples);
        }

        [TestMethod]
        public void RemoveTriplesBySubject_ValidSubjectGiven_RemovesTripleWithSameSubject()
        {
            //Arrange
            var graph = new RDFGraph(startingTriples);
            var subjectToRemove = TestRDFObjectProvider.subjectResources[0];
            var desiredStoredTriples = new List<RDFTriple>(startingTriples);
            desiredStoredTriples.Remove(startingTriples[0]);
            var storedTriples = new List<RDFTriple>();

            //Act
            graph.RemoveTriplesBySubject(subjectToRemove);

            //Assert
            getGraphStoredTriples(graph, ref storedTriples);
            CollectionAssert.AreEqual(desiredStoredTriples, storedTriples);
        }

        [TestMethod]
        public void RemoveTriplesByPredicate_ValidPredicateGiven_RemovesTripleWithSamePredicate()
        {
            //Arrange
            var graph = new RDFGraph(startingTriples);
            var predicateToRemove = TestRDFObjectProvider.predicateResources[0];
            var desiredStoredTriples = new List<RDFTriple>(startingTriples);
            desiredStoredTriples.Remove(startingTriples[0]);
            var storedTriples = new List<RDFTriple>();

            //Act
            graph.RemoveTriplesByPredicate(predicateToRemove);

            //Assert
            getGraphStoredTriples(graph, ref storedTriples);
            CollectionAssert.AreEqual(desiredStoredTriples, storedTriples);
        }

        [TestMethod]
        public void RemoveTriplesByLiteral_ValidPredicateGiven_RemovesTripleWithSamePredicate()
        {
            //Arrange
            var graph = new RDFGraph(startingTriples);
            var literalToRemove = TestRDFObjectProvider.objectLiterals[0];
            var desiredStoredTriples = new List<RDFTriple>(startingTriples);
            desiredStoredTriples.Remove(startingTriples[0]);
            var storedTriples = new List<RDFTriple>();

            //Act
            graph.RemoveTriplesByLiteral(literalToRemove);

            //Assert
            getGraphStoredTriples(graph, ref storedTriples);
            CollectionAssert.AreEqual(desiredStoredTriples, storedTriples);
        }

        [TestMethod]
        public void ClearTriples_NonEmptyGraph_ClearsGraph()
        {
            //Arrange
            var graph = new RDFGraph(startingTriples);
            var desiredStoredTriples = new List<RDFTriple>();
            var storedTriples = new List<RDFTriple>();

            //Act
            graph.ClearTriples();

            //Assert
            getGraphStoredTriples(graph, ref storedTriples);
            CollectionAssert.AreEqual(desiredStoredTriples, storedTriples);
        }

        [TestMethod]
        public void ClearTriples_EmptyGraph_DoesNothingWithoutException()
        {
            //Arrange
            var graph = new RDFGraph();
            var desiredStoredTriples = new List<RDFTriple>();
            var storedTriples = new List<RDFTriple>();

            //Act
            graph.ClearTriples();

            //Assert
            getGraphStoredTriples(graph, ref storedTriples);
            CollectionAssert.AreEqual(desiredStoredTriples, storedTriples);
        }

        [TestMethod]
        public void SelectTriplesBySubject_ValidSubjectGiven_ReturnsTriplesWithSameSubject()
        {
            //Arrange
            var graph = new RDFGraph(startingTriples);
            var subjectToSelect = TestRDFObjectProvider.subjectResources[0];
            var desiredSelectedTriples = new List<RDFTriple>() { startingTriples[0] };
            var selectedTriples = new List<RDFTriple>();

            //Act
            var selectedGraph = graph.SelectTriplesBySubject(subjectToSelect);

            //Assert
            getGraphStoredTriples(selectedGraph, ref selectedTriples);
            CollectionAssert.AreEqual(desiredSelectedTriples, selectedTriples);
        }

        [TestMethod]
        public void SelectTriplesByPredicate_ValidPredicateGiven_ReturnsTriplesWithSamePredicate()
        {
            //Arrange
            var graph = new RDFGraph(startingTriples);
            var predicateToSelect = TestRDFObjectProvider.predicateResources[0];
            var desiredSelectedTriples = new List<RDFTriple>() { startingTriples[0] };
            var selectedTriples = new List<RDFTriple>();

            //Act
            var selectedGraph = graph.SelectTriplesByPredicate(predicateToSelect);

            //Assert
            getGraphStoredTriples(selectedGraph, ref selectedTriples);
            CollectionAssert.AreEqual(desiredSelectedTriples, selectedTriples);
        }

        [TestMethod]
        public void SelectTriplesByLiteral_ValidLiteralGiven_ReturnsTriplesWithSameLiteral()
        {
            //Arrange
            var graph = new RDFGraph(startingTriples);
            var literalToSelect = TestRDFObjectProvider.objectLiterals[0];
            var desiredSelectedTriples = new List<RDFTriple>() { startingTriples[0] };
            var selectedTriples = new List<RDFTriple>();

            //Act
            var selectedGraph = graph.SelectTriplesByLiteral(literalToSelect);

            //Assert
            getGraphStoredTriples(selectedGraph, ref selectedTriples);
            CollectionAssert.AreEqual(desiredSelectedTriples, selectedTriples);
        }

        [TestMethod]
        public void RemoveTriple_FluentRemoveCalls_RemovesMultipleTriple()
        {
            //Arrange
            var graph = new RDFGraph(TestRDFObjectProvider.triples);
            var desiredStoredTriples = new List<RDFTriple>(TestRDFObjectProvider.triples);
            desiredStoredTriples.RemoveRange(0, 2);
            var storedTriples = new List<RDFTriple>();

            //Act
            graph
                .RemoveTriple(startingTriples[0])
                .RemoveTriple(startingTriples[1]);

            //Assert
            getGraphStoredTriples(graph, ref storedTriples);
            CollectionAssert.AreEqual(desiredStoredTriples, storedTriples);
        }
    }
}
