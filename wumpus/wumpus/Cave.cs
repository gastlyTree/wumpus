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

        public List<Mob> mobsInTheGame = new List<Mob>();

        //Trying to make a graph of type room, but running into problems. should ask Rob
        public UGraphMatrix<Room> Caves = new UGraphMatrix<Room>(); 

        #endregion

        #region Constructors

        #endregion

        #region Methods

        #endregion
    }
}
