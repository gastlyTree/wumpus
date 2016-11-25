using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphMatrix;

namespace wumpus
{
    public class Cave
    {
        #region Attributes
        //The amounts of different mobs that will be added to the caves.
        public const int numBats = 2;
        public const int numPits = 2;
        public const int numWumpus = 1;

        public List<Mob> mobsInTheGame;

        //Trying to make a graph of type room, but running into problems. should ask Rob
        public UGraphMatrix<Room> Caves;

        #endregion

        #region Constructors

        public Cave()
        {
            mobsInTheGame = new List<Mob>();
            Caves = new UGraphMatrix<Room>();

            Random rnd = new Random();
            Stack<int> locationsUsed = new Stack<int>();
            while (locationsUsed.Count < numBats + numPits + numWumpus + 1)
            {
                int roll = rnd.Next(0, 19);
                if(!locationsUsed.Contains(roll))
                {
                    locationsUsed.Push(roll);
                }
            }
            
            //Create the cave rooms
            for (int i = 1; i <= 20; i++)
            {
                Caves.AddVertex(new Room(i));
            }
            //add the connections between the cave rooms
            Caves.AddEdge(Caves.vertices[0].Data, Caves.vertices[1].Data);
            Caves.AddEdge(Caves.vertices[0].Data, Caves.vertices[2].Data);
            Caves.AddEdge(Caves.vertices[0].Data, Caves.vertices[3].Data);
            Caves.AddEdge(Caves.vertices[1].Data, Caves.vertices[4].Data);
            Caves.AddEdge(Caves.vertices[1].Data, Caves.vertices[6].Data);
            Caves.AddEdge(Caves.vertices[2].Data, Caves.vertices[5].Data);
            Caves.AddEdge(Caves.vertices[2].Data, Caves.vertices[7].Data);
            Caves.AddEdge(Caves.vertices[3].Data, Caves.vertices[8].Data);
            Caves.AddEdge(Caves.vertices[3].Data, Caves.vertices[9].Data);
            Caves.AddEdge(Caves.vertices[4].Data, Caves.vertices[5].Data);
            Caves.AddEdge(Caves.vertices[4].Data, Caves.vertices[10].Data);
            Caves.AddEdge(Caves.vertices[5].Data, Caves.vertices[11].Data);
            Caves.AddEdge(Caves.vertices[6].Data, Caves.vertices[8].Data);
            Caves.AddEdge(Caves.vertices[6].Data, Caves.vertices[12].Data);
            Caves.AddEdge(Caves.vertices[7].Data, Caves.vertices[9].Data);
            Caves.AddEdge(Caves.vertices[7].Data, Caves.vertices[13].Data);
            Caves.AddEdge(Caves.vertices[8].Data, Caves.vertices[14].Data);
            Caves.AddEdge(Caves.vertices[9].Data, Caves.vertices[15].Data);
            Caves.AddEdge(Caves.vertices[10].Data, Caves.vertices[12].Data);
            Caves.AddEdge(Caves.vertices[10].Data, Caves.vertices[16].Data);
            Caves.AddEdge(Caves.vertices[11].Data, Caves.vertices[13].Data);
            Caves.AddEdge(Caves.vertices[11].Data, Caves.vertices[16].Data);
            Caves.AddEdge(Caves.vertices[12].Data, Caves.vertices[17].Data);
            Caves.AddEdge(Caves.vertices[13].Data, Caves.vertices[18].Data);
            Caves.AddEdge(Caves.vertices[14].Data, Caves.vertices[15].Data);
            Caves.AddEdge(Caves.vertices[14].Data, Caves.vertices[17].Data);
            Caves.AddEdge(Caves.vertices[15].Data, Caves.vertices[18].Data);
            Caves.AddEdge(Caves.vertices[16].Data, Caves.vertices[19].Data);
            Caves.AddEdge(Caves.vertices[17].Data, Caves.vertices[19].Data);
            Caves.AddEdge(Caves.vertices[18].Data, Caves.vertices[19].Data);


            //Add the mobs to the rooms

            //add the super bats
            for (int i = 0; i < numBats; i++)
            {
                mobsInTheGame.Add(new SuperBat(Caves.vertices[locationsUsed.Pop()].Data));
            }
            //add the pits
            for (int i = 0; i < numPits; i++)
            {
                mobsInTheGame.Add(new BottomlessPit(Caves.vertices[locationsUsed.Pop()].Data));
            }
            //add the wumpus
            for (int i = 0; i < numWumpus; i++)
            {
                mobsInTheGame.Add(new Wumpus(Caves.vertices[locationsUsed.Pop()].Data));
            }

            foreach (Mob mob in mobsInTheGame)
            {
                mob.location.addToRoom(mob);
            }

        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return Caves.ToString();
        }

        #endregion
    }
}
