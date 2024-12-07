using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiSD2_4.GraphF
{
    internal class Edge
    {
        public int Target { get; set; }
        public int Weight { get; set; }

        public Edge(int target, int weight)
        {
            Target = target;
            Weight = weight;

        }
    }
}
