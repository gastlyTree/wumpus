using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wumpus
{
    public class Mob
    {
        public Room location;

        public Mob()
        {
            location = new Room();
        }

        public Mob(Room locationSet)
        {
            this.location = locationSet;
        }
    }
}
