using Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GraphMatrix
{
    /// <summary>
    /// An undirected graph. an edge from A to B, means you can travel
    /// in both directions . Direction is not important. if the user adds
    /// a single edge from A to B, this class will add an edge from a to b
    /// as well as B to A
    /// </summary>
    public class UGraphMatrix<T> : AGraphMatrix<T> where T : IComparable<T>
    {
        public UGraphMatrix()
        {
            isDirected = false;
        }

        //since we are adding an edge in both directions, we divide by 2 to
        //return the correct number of logical edges
        public override int NumEdges
        {
            get
            {
                //call the parent' numbEdges property using "base" keyword
                return base.NumEdges/2;
            }
        }

        public override Edge<T>[] getAllEdges()
        {
            List<Edge<T>> edges = new List<Edge<T>>();
            //visit every row
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                //visit every column starting at index one greater than the 
                // the row number. Essentially, we are looking for edges above
                // the diagonal of the matrix. this prevents duplpicate edges
                // for udirected graphs
                for (int c = r+1; c < matrix.GetLength(1); c++)
                {
                    //if the current location has an edge
                    if (matrix[r, c] != null)
                    {
                        edges.Add(matrix[r, c]);
                    }
                }
            }
            //return the edges to an array
            return edges.ToArray();

        }

        //since this is undirected, when a user adds an edge, we add it in both directions
        public override void AddEdge(T from, T to)
        {
            base.AddEdge(from, to);
        }

        public override void AddEdge(T from, T to, double weight)
        {
            base.AddEdge(from, to, weight);
            base.AddEdge(to, from, weight);
        }

        public override void RemoveEdge(T from, T to)
        {
            base.RemoveEdge(from, to);
            base.RemoveEdge(to, from);
        }
    }
}
