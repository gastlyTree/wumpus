using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph;

namespace GraphMatrix
{
    /// <summary>
    /// A directed graph. an 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DGraphMatrix<T>: AGraphMatrix<T> where T: IComparable<T>
    {

        public DGraphMatrix()
        {
            isDirected = true;
        }

        public override Edge<T>[] getAllEdges()
        {
            List<Edge<T>> edges = new List<Edge<T>>();
            //visit every row
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                //visit every column
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    //if the current location has an edge
                    if(matrix[r,c] != null)
                    {
                        edges.Add(matrix[r, c]);
                    }
                }
            }
            //return the edges to an array
            return edges.ToArray();


        }
    }
}
