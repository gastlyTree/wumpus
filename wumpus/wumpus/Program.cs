using GraphMatrix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace wumpus
{
    class Program
    {
        static void TestShortestWeightedPath()
        {
            UGraphMatrix<string> uGraph = new UGraphMatrix<string>();
            uGraph.AddVertex("Prince Albert");
            uGraph.AddVertex("Saskatoon");
            uGraph.AddVertex("Yorkton");
            uGraph.AddVertex("Regina");
            uGraph.AddVertex("Weyburn");
            uGraph.AddEdge("Prince Albert", "Saskatoon", 2);
            uGraph.AddEdge("Saskatoon", "Yorkton", 4);
            uGraph.AddEdge("Saskatoon", "Regina", 1);
            uGraph.AddEdge("Regina", "Yorkton", 3);
            uGraph.AddEdge("Regina", "Weyburn", 5);
            uGraph.AddEdge("Yorkton", "Weyburn", 1);

            Console.WriteLine(uGraph);

            Console.WriteLine(uGraph.ShortestWeightedPath("Weyburn", "Prince Albert"));

        }

        static void Main(string[] args)
        {
            
        }
    }
}
