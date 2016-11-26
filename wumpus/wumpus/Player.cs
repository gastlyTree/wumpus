using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wumpus
{
    /// <summary>
    /// Player will be instantiated and set to a reference in the actual implementation so as to make it easier to reference in the code
    /// </summary>
    class Player : Mob
    {
        #region Attributes
        public int arrowCount;
        #endregion

        #region Constructors
        public Player()
        {
            arrowCount = 5;
        }

        public Player(Room locationSet)
        {
            this.location = locationSet;
            arrowCount = 5;
        }
        #endregion

            #region Methods

            /// <summary>
            /// this will take care of updating the player current location,
            /// Deleting them from the old location,
            /// and adding them to the specified room.
            /// </summary>
            /// <param name="newRoom"></param>
        public void movePlayer(Room newRoom)
        {
            location.roomSquaters.Remove(this);
            newRoom.addToRoom(this);
            location = newRoom;
        }

        public void shootArrow()
        {
            arrowCount--;
        }

        #endregion
    }
}
