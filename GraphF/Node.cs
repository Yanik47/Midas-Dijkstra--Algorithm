using AiSD2_4.GraphF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiSD2_4.GraphF
{
    internal class Node
    {
        private List<Edge> neighbors;
        public int Cost { get; set; }
        public int Value { get; set; }

        public Node(int value, int target, int weight)
        {
            Value = value;
            Edge edge = new Edge(target, weight);
            if(neighbors == null)
            {
                neighbors = new List<Edge>();
            }
            neighbors.Add(edge);
        }
        public Node(int value, int cost)
        {
            if(neighbors == null)
            {
                neighbors = new List<Edge>();
            }
            Value = value;
            Cost = cost;
        }

        public void AddEdge( int target, int weight)
        {
            neighbors.Add(new Edge(target, weight));
        }

        public List<Edge> GetEdges()
        {
            return neighbors;
        }

    }
}

