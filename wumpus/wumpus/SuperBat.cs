using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wumpus
{
    public class SuperBat : Mob
    {
        #region Attributes

        #endregion

        #region Constructors
        public SuperBat() { }

        public SuperBat(Room locationSet)
        {
            this.location = locationSet;
        }
        #endregion

        #region Methods

        public void movePlayer()
        {

        }

        #endregion
    }
}
