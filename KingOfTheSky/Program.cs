using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KingOfTheSky
{
    class Program
    {
        static void Main(string[] args)
        {
            Factory factory = new Factory();
            List<Ship> ships = new List<Ship>();
            Hull[] hulls = { new Hull("Wooden", 200, 1.2), new Hull("Iron", 500, 1.5), new Hull("Steel", 1500, 2.0) };
            Cannon[] cannons = { new Cannon("Ion Cannon", 300, 1.3, 100), new Cannon("Dragon Fire", 500, 2.0, 80) };
            Torpedo[] torpedos = { new Torpedo("Blue Lightning", 500, 1.2, 90), new Torpedo("Black Thunder", 1000, 1.8, 70) };
            Bomb[] bombs = { new Bomb( "Extra Cargo", 400, 1.3, 55), new Bomb("Dynamite", 600, 1.7, 75) };
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
            string[] command = Console.ReadLine().Split(' ');
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
                    Console.WriteLine((i+1) + ". " + ships[i].name + "\nStats: Lvl - " + ships[i].level + ", HP - " + ships[i].health + ", Spd - " + ships[i].speed);
                    //Console.WriteLine("Hull: " + ships[i].hull.name + ", Cannon: " + ships[i].cannon.name + ", Torpedo: " + ships[i].torpedo.name + ", Bomb: " + ships[i].bomb.name);
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
                            ships[int.Parse(command[3]) - 1].hull = hulls[int.Parse(command[2]) - 1];
                            Console.WriteLine("The " + ships[int.Parse(command[3]) - 1].name + " has the " + hulls[int.Parse(command[2]) - 1].name + " Hull equipted\n");
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine("The entered hull or ship does not exist");
                        }
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
                Console.WriteLine((i + 1) + ". " + hulls[i].name + ": HP - " + hulls[i].health + ", Weight - " + hulls[i].weight);
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
        public int health;
        public double weight;

        public Hull(string name, int health, double weight)
        {
            this.name = name;
            this.health = health;
            this.weight = weight;
        }
    }

    class Weapon
    {
        public string name;
        public int power;
        public double weight;
        public short accuracy;
    }

    class Cannon : Weapon
    {
        public Cannon(string name, int power, double weight, short accuracy)
        {
            this.name = name;
            this.power = power;
            this.weight = weight;
            this.accuracy = accuracy;
        }
    }

    class Torpedo : Weapon
    {
        public Torpedo(string name, int power, double weight, short accuracy)
        {
            this.name = name;
            this.power = power;
            this.weight = weight;
            this.accuracy = accuracy;
        }
    }

    class Bomb : Weapon
    {
        public Bomb(string name, int power, double weight, short accuracy)
        {
            this.name = name;
            this.power = power;
            this.weight = weight;
            this.accuracy = accuracy;
        }
    }
}
