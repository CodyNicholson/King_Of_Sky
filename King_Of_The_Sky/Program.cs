using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace King_Of_The_Sky
{
    class Program
    {
        static void Main(string[] args)
        {
            Factory factory = new Factory();
            List<Ship> ships = new List<Ship>();
            Hull[] hulls = { new Hull("Wooden", 200, 120), new Hull("Iron", 500, 50), new Hull("Steel", 1500, 100) };
            Cannon[] cannons = { new Cannon("Ion Cannon", 300, 30, 100), new Cannon("Dragon Fire", 500, 200, 80) };
            Torpedo[] torpedos = { new Torpedo("Blue Lightning", 100, 60, 90), new Torpedo("Black Thunder", 1000, 90, 70) };
            Bomb[] bombs = { new Bomb( "Extra Cargo", 400, 65, 55), new Bomb("Dynamite", 600, 85, 75) };
            Command commandCenter = new Command(factory, ships, hulls, cannons, torpedos, bombs);
            bool gameRunning = true;

            Console.WriteLine("Welcome to King Of The Sky! Enter a command to get started.\n");
            
            while (gameRunning)
            {
                commandCenter.enterCommand();
            }
        }
    }

    class Command
    {
        Factory factory;
        List<Ship> ships;
        Hull[] hulls;
        Cannon[] cannons;
        Torpedo[] torpedos;
        Bomb[] bombs;

        public Command(Factory factory, List<Ship> ships, Hull[] hulls, Cannon[] cannons, Torpedo[] torpedos, Bomb[] bombs)
        {
            this.factory = factory;
            this.ships = ships;
            this.hulls = hulls;
            this.cannons = cannons;
            this.torpedos = torpedos;
            this.bombs = bombs;
        }

        public void enterCommand()
        {
            Console.WriteLine("Enter Command Below:");
            string[] command;
            try
            {
                command = Console.ReadLine().Split(' ');
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid input was entered");
                return;
            }
            Console.WriteLine();

            if     ((command[0].ToLower() == "b" || command[0].ToLower() == "build") && command.Length >= 3)
            {
                string shipName = command[2];
                for(int i = 3; i < command.Length; i++)
                {
                    shipName = shipName + " " + command[i];
                }

                if (command[1].ToLower() == "bomber" || command[1].ToLower() == "b")
                {
                    ships.Add(factory.createBomber(shipName));
                }
                else if (command[1].ToLower() == "crusier" || command[1].ToLower() == "c")
                {
                    ships.Add(factory.createCrusier(shipName));
                }
                else if (command[1].ToLower() == "glider" || command[1].ToLower() == "g")
                {
                    ships.Add(factory.createGlider(shipName));
                }
            }
            else if (command[0].ToLower() == "s" || command[0].ToLower() == "ship" || command[0].ToLower() == "ships")
            {
                Console.WriteLine("Ships in HQ:");
                for (int i = 0; i < ships.Count; i++)
                {
                    ships[i].getStats();
                    ships[i].getEquiptmentStats();
                }
                Console.WriteLine();
            }
            else if (command[0].ToLower() == "e" || command[0].ToLower() == "equipt")
            {
                if (command.Length == 1)
                {
                    listHulls();
                    listCannons();
                    listTorpedos();
                    listBombs();
                }
                else if (command.Length == 2)
                {
                    if (command[1].ToLower() == "h" || command[1].ToLower() == "hulls")
                    {
                        listHulls();
                    }
                    else if (command[1].ToLower() == "c" || command[1].ToLower() == "cannons")
                    {
                        listCannons();
                    }
                    else if (command[1].ToLower() == "t" || command[1].ToLower() == "torpedos")
                    {
                        listTorpedos();
                    }
                    else if (command[1].ToLower() == "b" || command[1].ToLower() == "bombs")
                    {
                        listBombs();
                    }
                }
                else if (command.Length == 4)
                {
                    if (command[1].ToLower() == "h")
                    {
                        try
                        {
                            ships[int.Parse(command[3]) - 1].equiptHull(hulls[int.Parse(command[2]) - 1]);
                            Console.WriteLine("The " + ships[int.Parse(command[3]) - 1].name + " has the " + hulls[int.Parse(command[2]) - 1].name + " Hull equipted\n");
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("The entered hull or ship does not exist");
                        }
                    }
                    else if (command[1].ToLower() == "c")
                    {
                        try
                        {
                            ships[int.Parse(command[3]) - 1].equiptCannon(cannons[int.Parse(command[2]) - 1]);
                            Console.WriteLine("The " + ships[int.Parse(command[3]) - 1].name + " has the " + cannons[int.Parse(command[2]) - 1].name + " Cannon equipted\n");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("The entered cannon or ship does not exist");
                        }
                    }
                    else if (command[1].ToLower() == "t")
                    {
                        try
                        {
                            ships[int.Parse(command[3]) - 1].equiptTorpedo(torpedos[int.Parse(command[2]) - 1]);
                            Console.WriteLine("The " + ships[int.Parse(command[3]) - 1].name + " has the " + torpedos[int.Parse(command[2]) - 1].name + " Torpedo equipted\n");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("The entered torpedo or ship does not exist");
                        }
                    }
                    else if (command[1].ToLower() == "b")
                    {
                        try
                        {
                            ships[int.Parse(command[3]) - 1].equiptBomb(bombs[int.Parse(command[2]) - 1]);
                            Console.WriteLine("The " + ships[int.Parse(command[3]) - 1].name + " has the " + bombs[int.Parse(command[2]) - 1].name + " Bombs equipted\n");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("The entered bomb or ship does not exist");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input");
                    }
                }
            }
            else if (command[0].ToLower() == "c" || command[0].ToLower() == "combat")
            {

            }
            else if (command[0].ToLower() == "q" || command[0].ToLower() == "quit" || command[0] == "")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("The entered input did not match any of the available commands\n");
            }
        }

        public void listHulls()
        {
            Console.WriteLine("Available Hulls:");
            for (int i = 0; i < hulls.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + hulls[i].name + ": Armor - " + hulls[i].armor + ", Weight - " + hulls[i].weight);
            }
            Console.WriteLine();
        }

        public void listCannons()
        {
            Console.WriteLine("Available Cannons:");
            for (int i = 0; i < cannons.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + cannons[i].name + ": Power - " + cannons[i].power + ", Weight - " + cannons[i].weight + ", Accuracy - " + cannons[i].accuracy);
            }
            Console.WriteLine();
        }

        public void listTorpedos()
        {
            Console.WriteLine("Available Torpedos:");
            for (int i = 0; i < torpedos.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + torpedos[i].name + ": Power - " + torpedos[i].power + ", Weight - " + torpedos[i].weight + ", Accuracy - " + torpedos[i].accuracy);
            }
            Console.WriteLine();
        }

        public void listBombs()
        {
            Console.WriteLine("Available Bombs:");
            for (int i = 0; i < bombs.Length; i++)
            {
                Console.WriteLine((i + 1) + ". " + bombs[i].name + ": Power - " + bombs[i].power + ", Weight - " + bombs[i].weight + ", Accuracy - " + bombs[i].accuracy);
            }
            Console.WriteLine();
        }
    }

    class Factory
    {
        public Cruiser createCrusier(string name)
        {
            Cruiser c = new Cruiser(name);
            Console.WriteLine("You have built a new Cruiser named: The " + c.name + "\nStats:\nHealth: " + c.health + "\nArmor: " + c.armor + "\nSpeed: " + c.speed + "\n");
            return c;
        }

        public Glider createGlider(string name)
        {
            Glider g = new Glider(name);
            Console.WriteLine("You have built a new Glider named: The " + g.name + "\nStats:\nHealth: " + g.health + "\nArmor: " + g.armor + "\nSpeed: " + g.speed + "\n");
            return g;
        }

        public Bomber createBomber(string name)
        {
            Bomber b = new Bomber(name);
            Console.WriteLine("You have built a new Bomber named: The " + b.name + "\nStats:\nHealth: " + b.health + "\nArmor: " + b.armor + "\nSpeed: " + b.speed + "\n");
            return b;
        }
    }

    class Ship
    {
        public string name;
        public short level;
        public int health;
        public int armor;
        public int speed;
        public int build_points;
        public Hull hull;
        public Cannon cannon;
        public Torpedo torpedo;
        public Bomb bomb;

        public void getStats()
        {
            Console.WriteLine("The " + this.name + " is level " + this.level + " and has " + this.health + " health, " + this.armor + " armor, and " + this.speed + " speed");
        }

        public void getEquiptmentStats()
        {
            if (this.hull != null)
            {
                Console.WriteLine("The " + this.name + " has the " + this.hull.name + " hull equipted which has " + this.hull.armor + " armor, and weighs " + this.hull.weight);
            }
            else
            {
                Console.WriteLine("The " + this.name + " does not have a hull equipted");
            }
            if (this.cannon != null)
            {
                Console.WriteLine("The " + this.name + " has the " + this.cannon.name + " cannon equipted which has " + this.cannon.power + " power and " + this.cannon.accuracy + "% accuracy, and weighs " + this.cannon.weight);
            }
            else
            {
                Console.WriteLine("The " + this.name + " does not have a cannon equipted");
            }
            if (this.torpedo != null)
            {
                Console.WriteLine("The " + this.name + " has the " + this.torpedo.name + " torpedo equipted which has " + this.torpedo.power + " power and " + this.torpedo.accuracy + "% accuracy, and weighs " + this.torpedo.weight);
            }
            else
            {
                Console.WriteLine("The " + this.name + " does not have torpedos equipted");
            }
            if (this.bomb != null)
            {
                Console.WriteLine("The " + this.name + " has the " + this.bomb.name + " bomb equipted which has " + this.bomb.power + " power and " + this.bomb.accuracy + "% accuracy, and weighs " + this.bomb.weight);
            }
            else
            {
                Console.WriteLine("The " + this.name + " does not have bombs equipted");
            }
        }

        public void equiptHull(Hull hull)
        {
            if (this.hull != null)
            {
                this.armor -= this.hull.armor;
                this.speed += this.hull.weight;
            }
            this.hull = hull;
            this.armor += hull.armor;
            this.speed -= hull.weight;
        }

        public void equiptCannon(Cannon cannon)
        {
            if (this.cannon != null)
            {
                this.speed += this.cannon.weight;
            }
            this.cannon = cannon;
            this.speed -= cannon.weight;
        }

        public void equiptTorpedo(Torpedo torpedo)
        {
            if (this.torpedo != null)
            {
                this.speed += this.torpedo.weight;
            }
            this.torpedo = torpedo;
            this.speed -= torpedo.weight;
        }

        public void equiptBomb(Bomb bomb)
        {
            if (this.bomb != null)
            {
                this.speed += this.bomb.weight;
            }
            this.bomb = bomb;
            this.speed -= bomb.weight;
        }
    }

    class Glider : Ship
    {
        public Glider(string name)
        {
            this.name = name;
            Random rand = new Random();
            this.level = 0;
            this.health = rand.Next(1000, 2000);
            this.armor = rand.Next(10, 80);
            this.speed = rand.Next(100, 200);
            this.build_points = 0;
            
        }
    }

    class Bomber : Ship
    {
        public Bomber(string name)
        {
            this.name = name;
            Random rand = new Random();
            this.level = 0;
            this.health = rand.Next(2500, 5000);
            this.armor = rand.Next(100, 200);
            this.speed = rand.Next(10, 80);
            this.build_points = 0;
        }
    }

    class Cruiser : Ship
    {
        public Cruiser(string name)
        {
            this.name = name;
            Random rand = new Random();
            this.level = 0;
            this.health = rand.Next(1500, 3500);
            this.armor = rand.Next(50, 150);
            this.speed = rand.Next(50, 150);
            this.build_points = 0;
        }
    }

    class Hull
    {
        public string name;
        public int armor;
        public int weight;

        public Hull(string name, int health, int weight)
        {
            this.name = name;
            this.armor = health;
            this.weight = weight;
        }
    }

    class Weapon
    {
        public string name;
        public int power;
        public int weight;
        public short accuracy;
    }

    class Cannon : Weapon
    {
        public Cannon(string name, int power, int weight, short accuracy)
        {
            this.name = name;
            this.power = power;
            this.weight = weight;
            this.accuracy = accuracy;
        }
    }

    class Torpedo : Weapon
    {
        public Torpedo(string name, int power, int weight, short accuracy)
        {
            this.name = name;
            this.power = power;
            this.weight = weight;
            this.accuracy = accuracy;
        }
    }

    class Bomb : Weapon
    {
        public Bomb(string name, int power, int weight, short accuracy)
        {
            this.name = name;
            this.power = power;
            this.weight = weight;
            this.accuracy = accuracy;
        }
    }
}
