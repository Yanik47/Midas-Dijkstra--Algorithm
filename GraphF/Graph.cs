using AiSD2_4.GraphF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiSD2_4.GraphF
{
    internal class Graph
    {
        private Node[] adjancencyList;
        private int[] kingdomWeight;
        private int MinCostNode = int.MaxValue;


        public Graph(int numberOfVertices)
        {
            adjancencyList = new Node[numberOfVertices];

        }

        public void CreateKingdomWeight(int size)
        {
            kingdomWeight = new int[size];
        }

        public int GetKingdomWeight(int source)
        {
            return kingdomWeight[source];
        }

        public void AddNodeParam(int source, int cost, int weight)
        {
            if (cost < MinCostNode)
            {
                MinCostNode = cost;
            }
            adjancencyList[source] = new Node(source, cost);
            kingdomWeight[source] = weight;
        }
        public int GetMinCostNode()
        {
            return MinCostNode;
        }

        public void AddEdgeToNode(int source, int target, int weight)
        {
            Node node = adjancencyList[source];

            if (node == null)
            {
                adjancencyList[source] = new Node(source, target, weight);
            }
            else
            {
                node.AddEdge(target, weight);
            }
        }

        public int GetCostNode(int source)
        {
            return adjancencyList[source].Cost;
        }
        public void SetCostNode(int source, int cost)
        {
            adjancencyList[source].Cost = cost;
        }

        public Node[] GetAdjancencyList()
        {
            Node[] list = new Node[adjancencyList.Length];
            foreach (Node node in adjancencyList)
            {
                list[node.Value] = node;
            }
            return list;
        }

        public int GetAdjancencyListSize()
        {
            return adjancencyList.Length;
        }

        public List<Edge> GetEdgesFromNode(int source)
        {
            Node node = adjancencyList[source];
            return node.GetEdges();
        }
    }
}
