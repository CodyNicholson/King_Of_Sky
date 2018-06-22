using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingOfTheSky.src
{
    class ShipManager
    {
        private Hull[] hulls;
        private Cannon[] cannons;
        private Torpedo[] torpedos;
        private Bomb[] bombs;

        public ShipManager()
        {
            this.hulls = new Hull[] { new Hull("Wood", 200, 0, 0), new Hull("Iron", 500, 50, 10), new Hull("Steel", 1500, 100, 20) };
            this.cannons = new Cannon[] { new Cannon("Standard Cannon", 100, 0, 100, 0), new Cannon("Ion Cannon", 300, 30, 100, 10), new Cannon("Dragon Fire", 500, 200, 80, 20) };
            this.torpedos = new Torpedo[] { new Torpedo("Standard Torpedo", 70, 0, 90, 0), new Torpedo("Blue Lightning", 100, 60, 90, 10), new Torpedo("Black Thunder", 1000, 90, 70, 20) };
            this.bombs = new Bomb[] { new Bomb("Standard Bomb", 120, 0, 80, 0), new Bomb("TNT", 400, 65, 55, 10), new Bomb("Dynamite", 600, 85, 75, 20) };
        }

        public void EnterShipManagerCommand(PlayerManager playerManager)
        {
            Console.WriteLine("Available Commands:\n" +
                    "<'ships' or 's'>  - View all ships in the current player's armada with their ship numbers\n" +
                    "<'{ship number}'> - View specific deatils of one ship in armada\n" +
                    "<'equip' or 'e'>  - Outfit your ships with various equiptment\n" +
                    "<'return' or 'r'> - Return to main menu\n\n" +
                    "Enter Ship Manager Command:");

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

            if (command[0].ToLower() == "ships" || command[0].ToLower() == "s")
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
            else if (int.TryParse(command[0], out int shipNum))
            {
                try
                {
                    if (playerManager.GetCurrentPlayer().GetShips()[shipNum - 1] != null)
                    {
                        playerManager.GetCurrentPlayer().GetShips()[shipNum - 1].GetStats();
                        playerManager.GetCurrentPlayer().GetShips()[shipNum - 1].GetEquiptmentStats();
                    }
                    else
                    {
                        Console.WriteLine("You do not yet have a ship " + shipNum + ". To create a new ship, return to the main menu and enter the 'build' command.\n");
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("The number you entered was invalid, you can only have 5 ships\n");
                }
            }
            else if (command[0].ToLower() == "e" || command[0].ToLower() == "equip")
            {
                EnterEquipCommand(playerManager);
            }
            else if (command[0].ToLower() == "r" || command[0].ToLower() == "return" || command[0].ToLower() == "")
            {
                return;
            }
            else
            {
                InvalidInput();
            }
            EnterShipManagerCommand(playerManager);
        }

        public void EnterEquipCommand(PlayerManager playerManager)
        {
            Console.WriteLine("Available Commands:\n" +
                        "<'list' or 'l'>  - View all available equiptment\n" +
                        "<'ships' or 's'> - View all ships in the current player's armada\n" +
                        "<'hulls' or 'h' or 'cannons' or 'c' or 'torpedos' or 't' or 'bombs' or 'b'> - View available equiptment of input type\n" +
                        "<'hulls' or 'h' or 'cannons' or 'c' or 'torpedos' or 't' or 'bombs' or 'b'> <'{equiptment number}'> <'{ship number}'> - Equipts the provided ship with the provided equiptment\n" +
                        "<'return' or 'r'> - Go back to Ship Manager Menu\n\n" +
                        "Enter Equipt Manager Command:");

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
                if (command[0].ToLower() == "list" || command[0].ToLower() == "l")
                {
                    ListHulls(playerManager.GetCurrentPlayer());
                    ListCannons(playerManager.GetCurrentPlayer());
                    ListTorpedos(playerManager.GetCurrentPlayer());
                    ListBombs(playerManager.GetCurrentPlayer());
                }
                if (command[0].ToLower() == "ships" || command[0].ToLower() == "s")
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
                else if (command[0].ToLower() == "h" || command[0].ToLower() == "hulls")
                {
                    ListHulls(playerManager.GetCurrentPlayer());
                }
                else if (command[0].ToLower() == "c" || command[0].ToLower() == "cannons")
                {
                    ListCannons(playerManager.GetCurrentPlayer());
                }
                else if (command[0].ToLower() == "t" || command[0].ToLower() == "torpedos")
                {
                    ListTorpedos(playerManager.GetCurrentPlayer());
                }
                else if (command[0].ToLower() == "b" || command[0].ToLower() == "bombs")
                {
                    ListBombs(playerManager.GetCurrentPlayer());
                }
                else if (command[0].ToLower() == "r" || command[0].ToLower() == "return" || command[0].ToLower() == "")
                {
                    return;
                }
                else
                {
                    InvalidInput();
                }
            }
            else if (command.Length == 3)
            {
                if (command[0].ToLower() == "h" || command[0].ToLower() == "hulls")
                {
                    try
                    {
                        playerManager.GetCurrentPlayer().GetShips()[int.Parse(command[2]) - 1].EquipHull(GetHulls()[int.Parse(command[1]) - 1]);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("The entered hull or ship does not exist\n");
                    }
                }
                else if (command[0].ToLower() == "c" || command[0].ToLower() == "cannons")
                {
                    try
                    {
                        playerManager.GetCurrentPlayer().GetShips()[int.Parse(command[2]) - 1].EquipCannon(GetCannons()[int.Parse(command[1]) - 1]);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("The entered cannon or ship does not exist\n");
                    }
                }
                else if (command[0].ToLower() == "t" || command[0].ToLower() == "torpedos")
                {
                    try
                    {
                        playerManager.GetCurrentPlayer().GetShips()[int.Parse(command[2]) - 1].EquipTorpedo(GetTorpedos()[int.Parse(command[1]) - 1]);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("The entered torpedo or ship does not exist\n");
                    }
                }
                else if (command[0].ToLower() == "b" || command[0].ToLower() == "bombs")
                {
                    try
                    {
                        playerManager.GetCurrentPlayer().GetShips()[int.Parse(command[2]) - 1].EquipBomb(GetBombs()[int.Parse(command[1]) - 1]);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("The entered bomb or ship does not exist\n");
                    }
                }
                else
                {
                    InvalidInput();
                }
            }
            else
            {
                InvalidInput();
            }
            EnterEquipCommand(playerManager);
        }

        public Hull[] GetHulls()
        {
            return this.hulls;
        }

        public Cannon[] GetCannons()
        {
            return this.cannons;
        }

        public Torpedo[] GetTorpedos()
        {
            return this.torpedos;
        }

        public Bomb[] GetBombs()
        {
            return this.bombs;
        }

        public void ListHulls(Player currentPlayer)
        {
            Console.WriteLine("Available Hulls:");
            for (int i = 0; i < GetHulls().Length; i++)
            {
                if (currentPlayer.GetLevel() >= GetHulls()[i].GetRequiredLevel())
                {
                    Console.WriteLine((i + 1) + ". " + GetHulls()[i].GetName() + ": Armor - " + GetHulls()[i].GetArmor() + ", Weight - " + GetHulls()[i].GetWeight());
                }
            }
            Console.WriteLine();
        }

        public void ListCannons(Player currentPlayer)
        {
            Console.WriteLine("Available Cannons:");
            for (int i = 0; i < GetCannons().Length; i++)
            {
                if (currentPlayer.GetLevel() >= GetCannons()[i].GetRequiredLevel())
                {
                    Console.WriteLine((i + 1) + ". " + GetCannons()[i].GetName() + ": Power - " + GetCannons()[i].GetPower() + ", Weight - " + GetCannons()[i].GetWeight() + ", Accuracy - " + GetCannons()[i].GetAccuracy());
                }
            }
            Console.WriteLine();
        }

        public void ListTorpedos(Player currentPlayer)
        {
            Console.WriteLine("Available Torpedos:");
            for (int i = 0; i < GetTorpedos().Length; i++)
            {
                if (currentPlayer.GetLevel() >= GetTorpedos()[i].GetRequiredLevel())
                {
                    Console.WriteLine((i + 1) + ". " + GetTorpedos()[i].GetName() + ": Power - " + GetTorpedos()[i].GetPower() + ", Weight - " + GetTorpedos()[i].GetWeight() + ", Accuracy - " + GetTorpedos()[i].GetAccuracy());
                }
            }
            Console.WriteLine();
        }

        public void ListBombs(Player currentPlayer)
        {
            Console.WriteLine("Available Bombs:");
            for (int i = 0; i < GetBombs().Length; i++)
            {
                if (currentPlayer.GetLevel() >= GetBombs()[i].GetRequiredLevel())
                {
                    Console.WriteLine((i + 1) + ". " + GetBombs()[i].GetName() + ": Power - " + GetBombs()[i].GetPower() + ", Weight - " + GetBombs()[i].GetWeight() + ", Accuracy - " + GetBombs()[i].GetAccuracy());
                }
            }
            Console.WriteLine();
        }

        public void InvalidInput()
        {
            Console.WriteLine("The entered input did not match any of the available commands\n");
        }
    }
}
