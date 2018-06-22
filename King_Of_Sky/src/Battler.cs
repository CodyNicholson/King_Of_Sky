using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingOfTheSky.src
{
    class Battler
    {
        Player player;
        Ship ship;

        public Battler(Player player, Ship ship)
        {
            this.player = player;
            this.ship = ship;
        }

        public Player GetPlayer()
        {
            return player;
        }

        public void SetPlayer(Player player)
        {
            this.player = player;
        }

        public Ship GetShip()
        {
            return ship;
        }

        public void SetShip(Ship ship)
        {
            this.ship = ship;
        }
    }
}
