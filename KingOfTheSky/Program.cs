using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingOfTheSky
{
    class Program
    {
        static void Main(string[] args)
        {
            Factory factory = new Factory();
            factory.createCrusier("Delphineus");
            factory.createGlider("Little Jack");
            factory.createBomber("Halberd");
            Console.ReadLine();
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

    class Glider
    {
        public string name;
        public short level;
        public int health;
        public int armor;
        public int speed;
        public int build_points;
        Hull hull;
        Cannon cannon;
        Torpedo torpedo;
        Bomb bomb;

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

    class Bomber
    {
        public string name;
        public short level;
        public int health;
        public int armor;
        public int speed;
        public int build_points;
        Hull hull;
        Cannon cannon;
        Torpedo torpedo;
        Bomb bomb;

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

    class Cruiser
    {
        public string name;
        public short level;
        public int health;
        public int armor;
        public int speed;
        public int build_points;
        Hull hull;
        Cannon cannon;
        Torpedo torpedo;
        Bomb bomb;

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
        public short weight;
    }

    class Weapon
    {
        public int power;
        public short weight;
        public short accuracy;
    }

    class Cannon : Weapon
    {
        public string name;

        public Cannon(string name)
        {
            this.name = name;
        }
    }

    class Torpedo : Weapon
    {
        public string name;
    }

    class Bomb : Weapon
    {
        public string name;
    }
}
