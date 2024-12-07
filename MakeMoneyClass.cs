using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AiSD2_4.DijkstraAlgorithms;
using AiSD2_4.GraphF;
using AiSD2_4.Algorithms;
using static AiSD2_4.DijkstraAlgorithms.MidasAlgorithm;
namespace AiSD2_4
{
    internal class MakeMoneyClass
    {

        
        public static Response MakeMoney(int countOfMetal, int countOfAlchemistryOperation, double weightOfOurMetal, List<string>metalData, List<String>alchemistryData)
        {

            Graph graph = new Graph(countOfMetal);
            graph.CreateKingdomWeight(countOfMetal);

            for (int i = 0; i < countOfMetal; i++)
            {
                //System.Console.WriteLine(countOfMetal);
                //System.Console.WriteLine(metalData[i]);
                System.Diagnostics.Debug.WriteLine(countOfMetal);
                System.Diagnostics.Debug.WriteLine("index" + i);
                System.Diagnostics.Debug.WriteLine(metalData.Count);
                //System.Diagnostics.Debug.WriteLine(metalData[i]);
                string[] metal = metalData[i].Split(' ');
                string[] metals = metal.Select(met => Regex.Replace(met, @"\s+", "")).ToArray();

                int metalId = i;
                int metalWeight = int.Parse(metals[1]);
                int metalCost = int.Parse(metals[0]);
                System.Diagnostics.Debug.WriteLine("MetalId" + (metalId).ToString() + " ");
                System.Diagnostics.Debug.WriteLine("MetalWeight" + (metalWeight).ToString() + " ");
                System.Diagnostics.Debug.WriteLine("MetalCost" + (metalCost).ToString() + " ");

                graph.AddNodeParam(metalId, metalCost, metalWeight);
            }

            for(int i = 0; i < countOfAlchemistryOperation; i++ )
            {
                //System.Console.WriteLine(countOfAlchemistryOperation);
                //System.Console.WriteLine(alchemistryData[i]);
                System.Diagnostics.Debug.WriteLine("max"+countOfAlchemistryOperation);
                System.Diagnostics.Debug.WriteLine("index" + i);
                System.Diagnostics.Debug.WriteLine(alchemistryData.Count);
                //System.Diagnostics.Debug.WriteLine(alchemistryData[i]);

                string[] alchemistry = alchemistryData[i].Split(' ');
                string[] alchemies = alchemistry.Select(alc => Regex.Replace(alc, @"\s+", "")).ToArray();

                int source = int.Parse(alchemies[0]);
                int target = int.Parse(alchemies[1]);
                int weight = int.Parse(alchemies[2]);
                System.Diagnostics.Debug.WriteLine("Source" + (source-1).ToString() + " ");
                System.Diagnostics.Debug.WriteLine("Target" + (target-1).ToString() + " ");
                System.Diagnostics.Debug.WriteLine("Weight" + (weight).ToString()  + " ");
                graph.AddEdgeToNode(source-1, target-1, weight);
            }
            System.Diagnostics.Debug.WriteLine("Graph created");
            Node[] adjancencyList = graph.GetAdjancencyList();
            System.Diagnostics.Debug.WriteLine("AdjancencyList created");

            foreach (Node node in adjancencyList)
            {
                System.Diagnostics.Debug.WriteLine("For Node(from): " + node.Value + " Cost: " + node.Cost);
                List<Edge> edges = node.GetEdges();
                foreach (Edge edge in edges)
                {
                    System.Diagnostics.Debug.WriteLine("Edge(\"to\" and weight): " + edge.Target + " " + edge.Weight);
                }
            }
            System.Diagnostics.Debug.WriteLine("Check end");
            Response response;
            response = MidasAlgorithm.Midas(graph, 0, weightOfOurMetal);

            double zarobek = response.zarobek;
            double zarobekGold = response.zarobekGold;

            Dictionary<int, int> Ile = response.Ile;
            Dictionary<int, int> IleGold = response.IleAleGold;

            System.Diagnostics.Debug.WriteLine("Zarobek: " + zarobek);
            System.Diagnostics.Debug.WriteLine("ZarobekGold: " + zarobekGold);

            foreach (KeyValuePair<int, int> entry in Ile)
            {

                System.Diagnostics.Debug.WriteLine("Ile: " + entry.Key + " " + entry.Value);
            }
            foreach(KeyValuePair<int, int> entry in IleGold  )
            {
                System.Diagnostics.Debug.WriteLine("IleGold: " + entry.Key + " " + entry.Value);
            }

            return response;


        }
    }
}
