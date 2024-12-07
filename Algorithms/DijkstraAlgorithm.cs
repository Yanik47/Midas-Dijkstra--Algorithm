using System;
using System.Collections.Generic;
using AiSD2_4.GraphF;
using FibonacciHeap;

namespace AiSD2_4.Algorithms
{
    internal class DijkstraAlgorithm
    {
        public static int[] Dijkstra(Graph graph, int source)
        {
            int n = graph.GetAdjancencyListSize();  // предполагаемый метод для получения размера списка смежности
            System.Diagnostics.Debug.WriteLine("Start Algorithm from node: " + source);
            System.Diagnostics.Debug.WriteLine("\nSize of adjancency list: " + n);
            int[] distances = new int[n];
            FibonacciHeapNode<int, double>[] nodes = new FibonacciHeapNode<int, double>[n];
            FibonacciHeap<int, double> heap = new FibonacciHeap<int, double>(double.NegativeInfinity);

            for (int i = 0; i < n; i++)
            {
                System.Diagnostics.Debug.WriteLine("Distance of edge" + i + " :" + int.MaxValue);
                distances[i] = int.MaxValue;
                nodes[i] = new FibonacciHeapNode<int, double>(i, (double)distances[i]);
                System.Diagnostics.Debug.WriteLine("Inserting edge: " + nodes[i].Key + "Value: " + nodes[i].Data);
                heap.Insert(nodes[i]);
            }
            distances[source] = 0;
            heap.DecreaseKey(nodes[source], 0);

            while (!heap.IsEmpty())
            {
                var minNode = heap.RemoveMin();
                System.Diagnostics.Debug.WriteLine("\nExtracted node: " + minNode.Data);
                int u = minNode.Data;

                foreach (var edge in graph.GetEdgesFromNode(u)) 
                {

                    int v = edge.Target;
                    System.Diagnostics.Debug.WriteLine("\nEdge target: " + v);
                    System.Diagnostics.Debug.WriteLine("Distance of edge(old) " + v + " :" + distances[v]);

                    double alt = distances[u] + edge.Weight;
                    System.Diagnostics.Debug.WriteLine("New: " + alt + ": distances after: " + distances[u] + " + edge: " + edge.Weight);
                    if (alt < distances[v])
                    { 
                        System.Diagnostics.Debug.WriteLine("New distance of edge " + v + " : " + alt);
                        distances[v] = (int)alt;
                        heap.DecreaseKey(nodes[v], alt);
                    }
                }
            }
            System.Diagnostics.Debug.WriteLine("Finish Dijkstra! ");
            return distances;
        }
    }
}


