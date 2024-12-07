using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AiSD2_4.GraphF;
using AiSD2_4.FibonacciHeap;
using AiSD2_4.Algorithms;
using System.Security.Cryptography;
using System.Data;
using System.Security;
using Windows.Services.Store;

namespace AiSD2_4.DijkstraAlgorithms
{

    internal class MidasAlgorithm
    {

        public class Pair
        {
            public int Source { get; private set; }
            public int Target { get; private set; }

            public int ConvertPrice { get; private set; }

            public Pair(int source, int target, int convertPrice)
            {
                Source = source;
                Target = target;
                convertPrice = ConvertPrice;
            }
        }

        public class DescComparer<T> : IComparer<T> where T : IComparable<T>
        {
            public int Compare(T x, T y)
            {
                return y.CompareTo(x);
            }
        }
        
  
        private static double UpdatePrice(Graph graph, double weight, double currentPrice, double coefficient, double actuallySold)
        {
            double newWeight = weight + actuallySold;
            return currentPrice * (1 - coefficient * (Math.Pow(newWeight, 1.0 / 3.0) - Math.Pow(weight, 1.0 / 3.0)));
        }



        private static Dictionary<int, int> Profit(Graph graph, SortedList<double, Pair> profit, double weight, ref double zarobek, Boolean flag)
        {
            Dictionary<int, int> Ile = new Dictionary<int, int>();
            Random random = new Random();
            double powerOfRandom = 1000.0;
            double actuallySold = 0.0;
            double increment = 1;
            double coefficient = 0.01;
            double startWeight = weight;
            double exWeight = 0.0;
            double currentPrice = profit.Keys[0];

            if (profit.Count > 0)
            {
                for (int i = 0; i < profit.Count; i++)
                {
                    if(profit.First().Key > currentPrice)
                    {
                        i = 0;
                        currentPrice = profit.Keys[i];
                    }
                    
                    double targetPrice = 0.0;

                    if (i + 1 < profit.Count && !flag)
                    {
                        targetPrice = profit.Keys[i + 1]; 
                    }

                    System.Diagnostics.Debug.WriteLine($"For Profit: {currentPrice} Current Price: {currentPrice} Target Price: {targetPrice}\n");
                    int target = 0;

                    while (currentPrice > targetPrice && weight > 0 && currentPrice >=1 && actuallySold <= startWeight)
                    {
                        if (profit.First().Key > currentPrice)
                        {
                            i = 0;
                            currentPrice = profit.Keys[i];
                        }

                        double saleAmount = Math.Min(weight, increment);

                        actuallySold += saleAmount;
                        Pair profitList = profit.Values[i];
                        target = profitList.Target;
                        zarobek += saleAmount * graph.GetCostNode(target);
                        weight -= saleAmount;

                        currentPrice = UpdatePrice(graph, weight + actuallySold, currentPrice, coefficient, actuallySold) + (double)(random.Next(500)) / powerOfRandom;
                                               
                        double newKey = currentPrice;
                        profit.Remove(profit.Keys[i]);
                        profit.Add(newKey, profitList);

                        System.Diagnostics.Debug.WriteLine($"Number of metal: {target} Current price: {currentPrice} Target price: {targetPrice} Actually Sold {actuallySold}\n");
                        /*foreach (var item in profit)
                        {
                            System.Diagnostics.Debug.WriteLine($"Profit: {item.Key} ");
                        }*/

                    }
                    System.Diagnostics.Debug.WriteLine("Actually Sold: " + actuallySold + " Weight: " + (startWeight).ToString() + "\n");
                    System.Diagnostics.Debug.WriteLine("Actually profit: " + zarobek + "\n");

                    if (actuallySold > 0)
                    {
                        if (!Ile.ContainsKey(target))
                        {
                            Ile.Add(target, 0);
                        }
                        
                        Ile[target] += (int)(actuallySold - exWeight);

                        if (actuallySold >= startWeight || !(currentPrice > 0))
                        {
                            break;
                        }
                        exWeight = actuallySold;
                    }
                    i = 0;
                    currentPrice = targetPrice;
                }
            }

            return Ile;
        }


        public static Response Midas(Graph graph, int source, double weight)
        {
            int[] options = new int[graph.GetAdjancencyListSize()];

            System.Diagnostics.Debug.WriteLine("\nMidas algorithm\n");

            //tablica, indeks to wierzchołek, wartość to koszt dojścia
            options = DijkstraAlgorithm.Dijkstra(graph, source);

            System.Diagnostics.Debug.WriteLine("\nDijkstra success\n");

            foreach(var item in options)
            {
                System.Diagnostics.Debug.WriteLine(item.GetType());
                System.Diagnostics.Debug.WriteLine("Dijkstra item: " + item.ToString());
            }
            int[] result = new int[graph.GetAdjancencyListSize()];
            var profit = new SortedList<double, Pair>(new DescComparer<double>());
            var profitGold = new SortedList<double, Pair>(new DescComparer<double>());

            for (int j = 0; j < graph.GetAdjancencyListSize(); j++)
            {
                // j = wierzcholek, options[j] = koszt dostania sie do wierzcholka 
                if (options[j] != int.MaxValue)
                {
                    //tablica od wierzcholka "dla zlota" do pozostalych 
                    result = DijkstraAlgorithm.Dijkstra(graph, j);
                    foreach(var item in result)
                    {
                        System.Diagnostics.Debug.WriteLine("\nDistances in Midas: " + item.ToString() + "\n");
                    }
                    //profit od najwiekszej konwersji, tutaj musi byc maximum
                    //i to koncowy wierzholek, result[i] to koszt dojscia do wierzcholka
                    for (int i = 0; i < graph.GetAdjancencyListSize(); i++)
                    {
                        if (result[i] != int.MaxValue)
                        { 
                            System.Diagnostics.Debug.WriteLine("Cena konwersji: " + ((result[i] + options[j])*weight + graph.GetCostNode(j)*weight/2 + graph.GetMinCostNode()/2).ToString());
                            System.Diagnostics.Debug.WriteLine("Cena surowcu: " + graph.GetCostNode(i).ToString());
                            System.Diagnostics.Debug.WriteLine("Waga surowcu" + weight);
                            System.Diagnostics.Debug.WriteLine("Waga krolestwa: " + graph.GetKingdomWeight(i));
                            System.Diagnostics.Debug.WriteLine("Wracajac z wierzcholka " + j + " do wierzcholka " + i + " Cena surowcu(bez uwzglednienia rynku): " + (graph.GetCostNode(i)*weight).ToString());
                            int alchPrice = result[i] + options[j];
                            double convertPrice = (alchPrice)*weight + graph.GetCostNode(j)*weight/ 2 + graph.GetMinCostNode() / 2;
                            result[i] = (int)(graph.GetCostNode(i)*weight - (int)convertPrice);
                            System.Diagnostics.Debug.WriteLine("Wracajac z wierzcholka "+ j + " do wierzcholka "+ i +" Profit(bez uwzglednienia rynku): " + result[i]);
                            
                            Pair newPair1 = new Pair(j, i, alchPrice);
                            if (!profit.ContainsKey(result[i]))
                            {
                                profit.Add(result[i], newPair1);
                            }
                            
                            profit[result[i]] = newPair1;
                            if (i == 0) 
                            {
                                if (!profitGold.ContainsKey(result[i]))
                                {
                                    profitGold.Add(result[i], newPair1);
                                }
                                profitGold[result[i]] = newPair1;
                            }
                        }
                    }
                }
            }
            //tu oblicam ile trzeba kupic kazdego rodzaju materialu dla maksymalnego zysku
            double zarobek = 0.0;
            double zarobekGold = 0.0;

            System.Diagnostics.Debug.WriteLine("Profit Algorithm Start Work: ");
            Boolean flag = false;   
            Dictionary<int, int> Ile = Profit(graph, profit, weight, ref zarobek, flag);
            System.Diagnostics.Debug.WriteLine("Profit Algorithm End Work: ");
            Pair newPair = new Pair(1, 1, 500);
            if (!profitGold.ContainsKey(0))
            {

                profitGold.Add(0, newPair);
            }

            profitGold[0] = newPair;
            flag = true;
            Dictionary<int, int> IleAleGold = Profit(graph, profitGold, weight, ref zarobekGold, flag);

            Response response = new Response();
            response.Ile = Ile;
            response.IleAleGold = IleAleGold;
            response.zarobek = zarobek;
            response.zarobekGold = zarobekGold;

            return response;
        }
    }
}
