using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Graph;

namespace GraphMatrix
{
    public abstract class AGraphMatrix<T> : AGraph<T> where T : IComparable<T>
    {
        #region Attributes
        public Edge<T>[,] matrix;
        #endregion

        #region Constructor

        public AGraphMatrix()
        {
            matrix = new Edge<T>[0,0];
        }

        #endregion

        /// <summary>
        /// Called from the parent class when a vertex is added.
        /// Creates room for the edges.
        /// </summary>
        /// <param name="v"></param>
        public override void AddVertexAdjustEdges(Vertex<T> v)
        {
            //Create a reference to the existing matrix
            Edge<T>[,] oldMatrix = matrix;
            //create the new larger matrix
            matrix = new Edge<T>[NumVertices, NumVertices];
            //copy edges from old matrix to new matrix
            for (int r = 0; r < oldMatrix.GetLength(0); r++)
            {
                for (int c = 0; c < oldMatrix.GetLength(1); c++)
                {
                    //copy currernt edge to new matrix
                    matrix[r, c] = oldMatrix[r, c];
                }
            }

        }

        public override void RemoveVertexAdjustEdges(Vertex<T> v)
        {
            numEdges = 0;

            Edge<T>[,] oldMatrix = matrix;
            //create the new larger matrix
            matrix = new Edge<T>[NumVertices, NumVertices];
            //copy edges from old matrix to new matrix
            for (int r = 0; r < oldMatrix.GetLength(0); r++)
            {
                for (int c = 0; c < oldMatrix.GetLength(1); c++)
                {
                    if(oldMatrix[r,c] != null)
                    {
                        //if the edge does not contain vertex to remove
                        if(r != v.Index && c != v.Index)
                        {
                            AddEdge(oldMatrix[r, c]);
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Gets a list of all vertices that are neighbours of data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override IEnumerable<Vertex<T>> EnumerateNeighbors(T data)
        {
            List<Vertex<T>> neighbours = new List<Vertex<T>>();
            //what row are the edges for data in
            //Stored as an index in the corresponding index containing data
            Vertex<T> v = GetVertex(data);
            //loop trhough all of the neighboues, and add them as a vertex
            //object to the list
            for (int c = 0; c < matrix.GetLength(1); c++)
            {
                //if teh current location is an edge
                if(matrix[v.Index,c] != null)
                {
                    //add the "to" vertex to the list
                    neighbours.Add(matrix[v.Index, c].To);
                }
            }

            return neighbours;
        }

        public override Edge<T> GetEdge(T from, T to)
        {
            if(!HasEdge(from,to))
            {
                throw new ApplicationException("No such edge");
            }
            //index in and return the edge object
            return matrix[GetVertex(from).Index, GetVertex(to).Index];
        }

        public override bool HasEdge(T from, T to)
        {
            return matrix[GetVertex(from).Index, GetVertex(to).Index] != null;
        }

        public override void RemoveEdge(T from, T to)
        {
            if (HasEdge(from,to))
            {
                //Index in to matrix and set location to null
                matrix[GetVertex(from).Index, GetVertex(to).Index] = null;
                numEdges--;
            }
            else
            {
                throw new ApplicationException("Edge not found");
            }
        }

        public override void AddEdge(Edge<T> e)
        {
            if(HasEdge(e.From.Data, e.To.Data))
            {
                throw new ApplicationException("Edge already exists");
            }
            //index into array and add the edge
            matrix[e.From.Index, e.To.Index] = e;
            //increment the edge count
            numEdges++;
        }

        

        public override string ToString()
        {
            StringBuilder result = new StringBuilder("\nEdge Matrix:\n");
            for (int r = 0; r < matrix.GetLength(0); r++)
            {
                Vertex<T> v = vertices[r];
                result.Append(v.Data.ToString() + "\t");
                for (int c = 0; c < matrix.GetLength(1); c++)
                {
                    result.Append((matrix[r, c] == null ? "null" : matrix[r, c].To.ToString()) + "\t");
                }
                result.Append("\n");

            }
            //Return the vertices appended to the edges
            return base.ToString() + result;
        }
    }
}
