using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public abstract class AGraph<T> : IGraph<T> where T : IComparable<T>
    {
        #region Attributes
        //stores the vertices of the graph
        public List<Vertex<T>> vertices;

        //A dictionary is a hashtable. we will use it to store a data items index
        //into the vertices list.. this will make it much more efficient to lookup
        // a vertex in the vertices list.
        public Dictionary<T, int> revLookUp;

        //store the number of edges in the graph
        public int numEdges;

        //is the graph directed or not
        public bool isDirected;

        //Is the graph weighted
        public bool isWeighted;

        #endregion

        #region Constructors
        public AGraph()
        {
            vertices = new List<Vertex<T>>();
            revLookUp = new Dictionary<T, int>();
            numEdges = 0;
        }
        #endregion

        #region Properties
        //make this property virtual, so it can be overwriten
        public virtual int NumEdges
        {
            get
            {
                return numEdges;
            }
        }

        public int NumVertices
        {
            get
            {
                return vertices.Count();
            }
        }
        #endregion

        #region Abstract Methods

        //a helper method that will allow us to implement the other two add edge methods
        public abstract void AddEdge(Edge<T> e);

        public abstract IEnumerable<Vertex<T>> EnumerateNeighbors(T data);

        public abstract Edge<T> GetEdge(T from, T to);

        public abstract bool HasEdge(T from, T to);

        public abstract void RemoveEdge(T from, T to);

        //When adding a vertex here, we need to tell the child class to make room
        //for the edges of this vertex.
        public abstract void AddVertexAdjustEdges(Vertex<T> v);
        public abstract void RemoveVertexAdjustEdges(Vertex<T> v);

        public abstract Edge<T>[] getAllEdges();

        #endregion

        public virtual void AddEdge(T from, T to)
        {
            // if this is the first edge, set the isWeighted Attribute to false
            if(numEdges == 0)
            {
                isWeighted = false;
            }
            else if(isWeighted)
            {
                throw new ApplicationException("Cant add unweighted edge to an weighted graph");
            }

            //Create an edge object
            Edge<T> e = new Edge<T>(GetVertex(from), GetVertex(to));
            //add the edge to whatever child implementation we are using
            AddEdge(e);
        }

        public virtual void AddEdge(T from, T to, double weight)
        {
            // if this is the first edge, set the isWeighted Attribute to true
            if (numEdges == 0)
            {
                isWeighted = true;
            }
            else if (!isWeighted)
            {
                throw new ApplicationException("Cant add weighted edge to an unweighted graph");
            }

            //Create an edge object
            Edge<T> e = new Edge<T>(GetVertex(from), GetVertex(to), weight);
            //add the edge to whatever child implementation we are using
            AddEdge(e);
        }

        public void AddVertex(T data)
        {
            //if the vertex already exists
            if(HasVertex(data))
            {
                //throw an exception
                throw new ApplicationException("Vertex already exists");
            }
            //instantiate a vertex object
            Vertex<T> v = new Vertex<T>(vertices.Count, data);
            ////add to the vertex list
            vertices.Add(v);
            //Also add to the dictionary
            revLookUp.Add(data, v.Index);
            //Tell child class to make room for this vertices edges
            AddVertexAdjustEdges(v);

        }

        public IEnumerable<Vertex<T>> EnumerateVertices()
        {
            return vertices;
        }

        public Vertex<T> GetVertex(T data)
        {
            if(!HasVertex(data))
            {
                throw new ApplicationException("No such vertex");
            }
            //Note that c# overloads [] to get a value out of the dictionary
            int index = revLookUp[data];
            return vertices[index];
        }

        public bool HasVertex(T data)
        {
            //most efficient to look in the dictionary
            return revLookUp.ContainsKey(data);
        }

        public void RemoveVertex(T data)
        {
            /*
            if vertex exists
               remove vertex from the vertices list
               romove data from the dictionary
               Decrement the indices (in the dictionary and the vertices array)
                   that are after the removed item in the vertices array
               adjust the vertex count
               remove edges pretaining to this vertex 
            else
               throw exception
            */ 
            
            Vertex<T> v = GetVertex(data);
            vertices.Remove(v);
            revLookUp.Remove(data);
            for (int i = v.Index; i < vertices.Count; i++)
            {
                //update the current vertex object
                vertices[i].Index--;
                //update the current data items index in the dictionary
                revLookUp[vertices[i].Data]--;
            }

            RemoveVertexAdjustEdges(v);

           
        }

        #region Traversals

        public void BreadthFirstTraversal(T start, VisitorDelegate<T> whatToDo)
        {
            //get the starting vertex
            Vertex<T> vStart = GetVertex(start);
            //referance to the current vertex
            Vertex<T> vCurrent;
            //Dictionary to track the vertices already visited
            Dictionary<T, T> visitedVertices = new Dictionary<T, T>();
            //queue to store unprocessed neughbours
            Queue<Vertex<T>> verticesRemaining = new Queue<Vertex<T>>();

            verticesRemaining.Enqueue(vStart);
            while (verticesRemaining.Count != 0)
            {
                vCurrent = verticesRemaining.Dequeue();
                if (!visitedVertices.ContainsKey(vCurrent.Data))
                {
                    whatToDo(vCurrent.Data);
                    visitedVertices.Add(vCurrent.Data, vCurrent.Data);

                    foreach (Vertex<T> v in EnumerateNeighbors(vCurrent.Data))
                    {
                        verticesRemaining.Enqueue(v);
                    }
                }
            }
        }

        public void DepthFirstTraversal(T start, VisitorDelegate<T> whatToDo)
        {
            //get the starting vertex
            Vertex<T> vStart = GetVertex(start);
            //referance to the current vertex
            Vertex<T> vCurrent;
            //Dictionary to track the vertices already visited
            Dictionary<T, T> visitedVertices = new Dictionary<T, T>();
            //stack to store unprocessed neughbours
            Stack<Vertex<T>> verticesRemaining = new Stack<Vertex<T>>();

            /*
            Push start vertex onto the stack
            while the stack is not empty
                current <-- pop top off the stack
                if current has not been visited yet
                    process the current vertex (call the delegate)
                    add current to the visited list
                    for each neighbour of current
                        push current neighbour onto the stack 
             */
            verticesRemaining.Push(vStart);
            while (verticesRemaining.Count != 0)
            {
                vCurrent = verticesRemaining.Pop();
                if(!visitedVertices.ContainsKey(vCurrent.Data))
                {
                    whatToDo(vCurrent.Data);
                    visitedVertices.Add(vCurrent.Data, vCurrent.Data);
                    
                    foreach (Vertex<T> v in EnumerateNeighbors(vCurrent.Data))
                    {
                        verticesRemaining.Push(v);
                    }
                }
            }

        }
        #endregion

        #region
        public IGraph<T> MinimumSpanningTree()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region shortest path implementation

        public IGraph<T> ShortestWeightedPath(T start, T end)
        {
            //Array of VertexData objects, one for each vertex in the graph
            VertexData[] vTable = new VertexData[vertices.Count];
            //Index of the starting point
            int iStartingIndex = GetVertex(start).Index;

            //Load the vTable with initial data
            for (int i = 0; i < vertices.Count; i++)
            {
                vTable[i] = new VertexData(vertices[i], double.PositiveInfinity,
                    null);
            }

            //set the start vertex data object's distance to 0
            vTable[iStartingIndex].Distance = 0;

            //create the priority queue
            PriorityQueue pq = new PriorityQueue();
            //enqueue the start vertex
            pq.Enqueue(vTable[iStartingIndex]);

            /*
            While there are still vertices on the priority queue
                current <-- pq dequeue
                    if the current is not known
                        set current to known
                        for each neighbour (vertex) of current
                            w <-- get the vertex data object
                            get the edge object connecting current and w
                            proposeddistance = current's distance + edge's distance
                            if w's distance > proposed distance
                                w's distance <-- proposedDistance
                                w's previous <-- current
                                pq enqueue w
            return BuildGraph(end vertex and vTable)
             */
            while(!pq.isEmpty())
            {
                //Get the vertexData with the lowest cost
                VertexData vCurrent = pq.Dequeue();
                //if this vertex is still unkown, process it
                if(!vCurrent.Known)
                {
                    //just found the shortest path for this vertex
                    vCurrent.Known = true;
                    //loop through the neighbours of the current vertex
                    foreach(Vertex<T> wVertex in EnumerateNeighbors(vCurrent.vVertex.Data))
                    {
                        //get the vertexData object for the current neighbour
                        VertexData w = vTable[wVertex.Index];
                        //grt the edge from vCurrent to w, we need its weight
                        Edge<T> eEdge = GetEdge(vCurrent.vVertex.Data, w.vVertex.Data);
                        //calculate the proposed distance
                        double dProposed = vCurrent.Distance + eEdge.Weight;
                        //compare proposed and current
                        if(w.Distance > dProposed)
                        {
                            //we found a shorter distance
                            w.Distance = dProposed;
                            //set the previous
                            w.vPrevious = vCurrent.vVertex;
                            //enqueue the neighbour
                            pq.Enqueue(w);
                        }
                    }
                }
            }

            return BuildGraph(GetVertex(end), vTable);
        }

        private IGraph<T> BuildGraph(Vertex<T> vEnd, VertexData[] vTable)
        {
            //Instantiate an instance of the child type graph using reflection thing
            IGraph<T> result = (IGraph<T>)GetType().Assembly.
                CreateInstance(this.GetType().FullName);

            /*
            Add the end vertex to result 
            dataLast <-- vTable (location of vEnd)
            previous <-- previous of dataLast
            while previous is not null
                add prevous to result
                add the edge from last and previous
                dataLast <-- vTable(location of previous)
                previous <-- dataLast previous
            return result
             */
            result.AddVertex(vEnd.Data);
            VertexData dataLast = vTable[vEnd.Index];
            Vertex<T> previous = dataLast.vPrevious;
            while(previous != null)
            {
                result.AddVertex(previous.Data);
                result.AddEdge(dataLast.vVertex.Data, previous.Data);
                dataLast = vTable[previous.Index];
                previous = dataLast.vPrevious;
            }
            return result;

        }

        /// <summary>
        ///     Provides a priority queue which is essentially a sorted list
        ///     of values. A call to dequeue will remove a value from the list
        ///     
        /// </summary>
        internal class PriorityQueue
        {
            private List<VertexData> sl;

            public PriorityQueue()
            {
                sl = new List<VertexData>();
            }

            internal void Enqueue (VertexData vData)
            {
                sl.Add(vData);
                sl.Sort();
            }

            internal VertexData Dequeue()
            {
                VertexData retVal = sl[0];
                sl.RemoveAt(0);
                return retVal;
            }

            public bool isEmpty()
            {
                if (sl.Count > 0 )
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            /// <summary>
            /// Remove this after testing
            /// </summary>
            public void DisplayQueue()
            {
                foreach (VertexData v in sl)
                {
                    Console.WriteLine(v.ToString());
                }
                Console.WriteLine();
            }

        }

        internal class VertexData : IComparable
        {
            public Vertex<T> vVertex;
            public double Distance;
            public bool Known;
            public Vertex<T> vPrevious;

            /// <summary>
            ///     Constructor
            /// </summary>
            /// <param name="vVertex"></param>
            /// <param name="Distance"></param>
            /// <param name="vPrevious"></param>
            /// <param name="Known">Note the default value</param>
            public VertexData(Vertex<T> vVertex, double Distance, 
                Vertex<T> vPrevious, bool Known = false)
            {
                this.vVertex = vVertex;
                this.Distance = Distance;
                this.vPrevious = vPrevious;
                this.Known = Known;
            }

            public int CompareTo(object obj)
            {
                //Just campare distance component for the sake of the
                // shortest path algorithm
                return this.Distance.CompareTo(((VertexData)obj).Distance);
            }

            public override string ToString()
            {
                return "Vertex: " + vVertex + " Distance: " + Distance
                    + " Previous: " + vPrevious + "Known? " + Known;
            }
        }

        #endregion

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            //Loop through each vertice and add to the result
            foreach (Vertex<T> v in EnumerateVertices())
            {
                result.Append(v + ", ");
            }
            //Take off the last comma
            if (vertices.Count > 0)
            {
                result.Remove(result.Length - 2, 2);
            }
            return GetType().Name + "\nVertices: " + result + "\n";
        }
    }
}
