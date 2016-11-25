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

        //list of all the mobs that are in the caves
        public List<Mob> mobsInTheGame;

        //set of random int's to be used for spawning the mobs
        public Stack<int> locationsUsed;

        //Matrix with that contains all the cave rooms and their connections.
        public UGraphMatrix<Room> Caves;

        #endregion

        #region Constructors

        public Cave()
        {
            mobsInTheGame = new List<Mob>();
            Caves = new UGraphMatrix<Room>();

            //spawn the mobs
            randomizeSpawnPoints();
            fillTheRooms();
            
            //Create the cave rooms
            for (int i = 1; i <= 20; i++)
            {
                Caves.AddVertex(new Room(i));
            }
            //add the connections between the cave rooms.
            //there is probably a better way of doing this.
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
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create the mobs, and then add them to the list of mobs in the game.
        /// Then add them to the rooms.
        /// </summary>
        public void fillTheRooms()
        {
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

            //****************************
            //this is not working quite right. It only adds a few mobs to the rooms.
            foreach (Mob mob in mobsInTheGame)
            {
                mob.location.addToRoom(mob);
            }
        }

        /// <summary>
        /// fills the locationsUsed stack with random numbers from 0 to 19.
        /// No numbers can repeat.
        /// amount of numbers is equal to the number of mobs
        /// </summary>
        public void randomizeSpawnPoints()
        {
            Random rnd = new Random();
            locationsUsed = new Stack<int>();
            //the +1 is for the player
            while (locationsUsed.Count < numBats + numPits + numWumpus + 1)
            {
                int roll = rnd.Next(0, 19);
                if (!locationsUsed.Contains(roll))
                {
                    locationsUsed.Push(roll);
                }
            }
        }

        /// <summary>
        /// probably not super neccisary
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Caves.ToString();
        }

        #endregion
    }
}
