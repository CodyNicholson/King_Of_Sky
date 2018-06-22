using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingOfTheSky.src
{
    class Player
    {
        private string name;
        private string password;
        private short level;
        private Ship[] ships;

        public Player(string name, string password)
        {
            this.name = name;
            this.password = password;
            this.level = 0;
            this.ships = new Ship[5];
        }

        public string GetName()
        {
            return this.name;
        }

        public string GetPassword()
        {
            return this.password;
        }

        public Ship[] GetShips()
        {
            return this.ships;
        }

        public short GetLevel()
        {
            return this.level;
        }

        public void ListShips()
        {
            Console.WriteLine("Ships in Captain " + GetName() + "'s armada:");
            for (int i = 0; i < GetShips().Length; i++)
            {
                if (GetShips()[i] != null)
                {
                    Console.WriteLine((i + 1) + ". " + GetShips()[i].GetName());
                }
                else
                {
                    Console.WriteLine((i + 1) + ". ~Empty~");
                }
            }
            Console.WriteLine();
        }

        public void SetLevel(short level)
        {
            this.level = level;
        }

        public void PlaceShipInArray(Ship newShip)
        {
            for (int i = 0; i < GetShips().Length; i++)
            {
                if (GetShips()[i] == null)
                {
                    GetShips()[i] = newShip;
                    Console.WriteLine("The " + newShip.GetName() + " is number " + (i + 1) + " in your armada\n");
                    return;
                }
            }
            Console.WriteLine("Your armada is full. Enter the number of the ship you would like to replace, or enter anything else to cancel ship creation:");
            string command = Console.ReadLine();
            if (int.TryParse(command, out int shipNum))
            {
                Console.WriteLine("\nThe " + GetShips()[shipNum - 1].GetName() + " has been replaced by The " + newShip.GetName() + "\n");
                GetShips()[shipNum - 1] = newShip;
            }
        }
    }
}
