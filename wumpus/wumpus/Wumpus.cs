using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wumpus
{
    public class Wumpus : Mob
    {
        #region Attributes
        public const int SPOOKED_CHANCE = 75;
        #endregion

        #region Constructors
        public Wumpus() { }

        public Wumpus(Room locationSet)
        {
            this.location = locationSet;
        }
        #endregion

        #region Methods

        public bool moveWumpus(Room newLocation)
        {
            bool moved = false;
            Random rnd = new Random();
            int roll = rnd.Next(1, 101);
            if(roll <= 75)
            {
                location = newLocation;
                moved = true;
            }
            return moved;
        }

        #endregion
    }
}
