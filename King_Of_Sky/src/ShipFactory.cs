using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingOfTheSky.src
{
    class ShipFactory
    {
        public void EnterBuildCommand(PlayerManager playerManager)
        {
            Console.WriteLine("Available Commands:\n" +
                        "<'ships' or 's'>                                                  - Lists the ships in the current player's armada\n" +
                        "<'glider' or 'g' or 'crusier' or 'c' or 'bomber' or 'b'>          - Get more information about a certain type of ship\n" +
                        "<'glider' or 'g' or 'crusier' or 'c' or 'bomber' or 'b'> <'name'> - Build a type of ship with a name that can be multiple words\n" +
                        "<'return' or 'r'> - Return to main menu\n\n" +
                        "Enter Build Manager Command:");
            string[] command;
            try
            {
                command = Console.ReadLine().Split(' ');
                Console.WriteLine();
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid input was entered");
                return;
            }

            if (command.Length == 1)
            {
                if (command[0].ToLower() == "glider" || command[0].ToLower() == "g")
                {
                    Console.WriteLine("Gliders are fast and evasive but cannot take much damage\n");
                }
                else if (command[0].ToLower() == "crusier" || command[0].ToLower() == "c")
                {
                    Console.WriteLine("Crusiers are standard battleships with all-around average stats\n");
                }
                else if (command[0].ToLower() == "bomber" || command[0].ToLower() == "b")
                {
                    Console.WriteLine("Bombers are slow tanks that can take a hit\n");
                }
                else if (command[0].ToLower() == "ships" || command[0].ToLower() == "s")
                {
                    Console.WriteLine("Ships in armada:");
                    for (int i = 0; i < playerManager.GetCurrentPlayer().GetShips().Length; i++)
                    {
                        try
                        {
                            playerManager.GetCurrentPlayer().GetShips()[i].GetStats(i);
                        }
                        catch (NullReferenceException)
                        {
                            Console.WriteLine((i + 1) + ". ~Empty~");
                        }
                    }
                    Console.WriteLine();
                }
                else if (command[0].ToLower() == "return" || command[0].ToLower() == "r" || command[0].ToLower() == "")
                {
                    return;
                }
                else
                {
                    InvalidInput();
                }
            }
            else if (command.Length > 1)
            {
                string shipName = command[1];
                for (int i = 2; i < command.Length; i++)
                {
                    shipName = shipName + " " + command[i];
                }

                if (command[0].ToLower() == "bomber" || command[0].ToLower() == "b")
                {
                    playerManager.GetCurrentPlayer().PlaceShipInArray(CreateBomber(shipName));
                }
                else if (command[0].ToLower() == "crusier" || command[0].ToLower() == "c")
                {
                    playerManager.GetCurrentPlayer().PlaceShipInArray(CreateCrusier(shipName));
                }
                else if (command[0].ToLower() == "glider" || command[0].ToLower() == "g")
                {
                    playerManager.GetCurrentPlayer().PlaceShipInArray(CreateGlider(shipName));
                }
            }
            EnterBuildCommand(playerManager);
        }

        public Cruiser CreateCrusier(string name)
        {
            Cruiser c = new Cruiser(name);
            Console.WriteLine("You have built a new Cruiser named: The " + c.GetName() + "\n" +
                "Stats:\n" +
                "Health: " + c.GetTotalHealth() + "\n" +
                "Armor: " + c.GetArmor() + "\n" +
                "Speed: " + c.GetSpeed() + "\n");
            return c;
        }

        public Glider CreateGlider(string name)
        {
            Glider g = new Glider(name);
            Console.WriteLine("You have built a new Glider named: The " + g.GetName() + "\n" +
                "Stats:\n" +
                "Health: " + g.GetTotalHealth() + "\n" +
                "Armor: " + g.GetArmor() + "\n" +
                "Speed: " + g.GetSpeed() + "\n");
            return g;
        }

        public Bomber CreateBomber(string name)
        {
            Bomber b = new Bomber(name);
            Console.WriteLine("You have built a new Bomber named: The " + b.GetName() + "\n" +
                "Stats:\n" +
                "Health: " + b.GetTotalHealth() + "\n" +
                "Armor: " + b.GetArmor() + "\n" +
                "Speed: " + b.GetSpeed() + "\n");
            return b;
        }

        public void InvalidInput()
        {
            Console.WriteLine("The entered input did not match any of the available commands\n");
        }
    }
}
