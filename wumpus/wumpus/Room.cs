using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wumpus
{
    public class Room
    {
        //A list containing any mob that is in the room, Incuding the player
        List<Mob> roomSquaters = new List<Mob>();
        public int roomNumber;

        /// <summary>
        /// Will add a mob to the room, and then determine if the game is over
        /// because of that action.
        /// </summary>
        /// <param name="mobToAdd">The mob that will be added tot he room</param>
        /// <returns></returns>
        public bool addToRoom(Mob mobToAdd)
        {
            bool isGameOver = false;

            roomSquaters.Add(mobToAdd);

            if(roomSquaters.OfType<Wumpus>().Any() && roomSquaters.OfType<Player>().Any() ||
                roomSquaters.OfType<BottomlessPit>().Any() && roomSquaters.OfType<Player>().Any())
            {
                isGameOver = true;
            }

            return isGameOver;
        }
    }
}
