using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using RDFSharp;
using RDFSharp.Model;
using RDFSharp.Query;

namespace NonFunctionalTests {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine(Directory.GetCurrentDirectory() + "\n");

            // load file to graph
            Console.WriteLine("Loading file...");
            Stopwatch sw = new Stopwatch();

            FileStream fs = File.OpenRead(@"..\..\..\test_data\szepmuveszeti.n3");
            sw.Start();
            var museum_graph = RDFGraph.FromStream(RDFModelEnums.RDFFormats.Turtle, fs);
            sw.Stop();
            Console.WriteLine($"Load time: {sw.ElapsedMilliseconds / 1000.0} sec, Triples count: {museum_graph.TriplesCount,0:N0}");

            // calc total and avarge uri length
            long sum_uri_len = 0;
            foreach (var triple in museum_graph) {
                sum_uri_len += triple.Subject.ToString().Length;
                sum_uri_len += triple.Predicate.ToString().Length;
                sum_uri_len += triple.Object.ToString().Length;
            }
            Console.WriteLine($"Sum. patternmember len: {sum_uri_len,0:N0}\nAvg. patternmember len: {sum_uri_len / (museum_graph.TriplesCount * 3)}");

            // select query
            var ns_ecrm = new RDFNamespace("ecrm", " http://erlangen-crm.org/current/");
            RDFVariable actor = new RDFVariable("actor");
            RDFVariable note = new RDFVariable("note");
            RDFVariable label = new RDFVariable("label");

            var pg = new RDFPatternGroup("PG1")
                    .AddPattern(new RDFPattern(actor, RDFVocabulary.RDF.TYPE, new RDFResource(ns_ecrm + "E39_Actor")))
                    .AddPattern(new RDFPattern(actor, new RDFResource(ns_ecrm + "P3_has_note"), note))
                    .AddPattern(new RDFPattern(actor, RDFVocabulary.RDFS.LABEL, label));

            RDFSelectQuery query = new RDFSelectQuery()
                .AddPrefix(RDFNamespaceRegister.GetByPrefix("ecrm"))
                .AddPatternGroup(pg)
                .AddProjectionVariable(actor)
                .AddProjectionVariable(label);
            {
                Console.WriteLine("\nQuerry in progress");
                sw.Restart();
                sw.Start();
                RDFSelectQueryResult result = query.ApplyToGraph(museum_graph);
                sw.Stop();
                Console.WriteLine("Results count: " + result.SelectResultsCount);
                Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds / 1000.0} sec");
            }

            // select querry with regex filter
            {
                Console.WriteLine("\nQuerry in progress");
                pg.AddFilter(new RDFRegexFilter(note, new Regex(@"alkotó", RegexOptions.IgnoreCase)));
                sw.Restart();
                sw.Start();
                RDFSelectQueryResult result2 = query.ApplyToGraph(museum_graph);
                sw.Stop();
                Console.WriteLine("Results count: " + result2.SelectResultsCount);
                Console.WriteLine($"Elapsed: {sw.ElapsedMilliseconds / 1000.0} sec");
            }

        }
    }
}
