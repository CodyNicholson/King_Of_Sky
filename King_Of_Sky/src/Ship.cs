using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingOfTheSky.src
{
    class Ship
    {
        private string name;
        private int level;
        private int totalHealth;
        private int tempHealth;
        private int armor;
        private int speed;
        private int weight;
        private Hull hull;
        private Cannon cannon;
        private Torpedo torpedo;
        private Bomb bomb;

        public string GetName()
        {
            return this.name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public int GetLevel()
        {
            return this.level;
        }

        public void SetLevel(int lvl)
        {
            this.level = lvl;
        }

        public void LevelUp()
        {
            Random rand = new Random();

            SetLevel(level + 1);
            Console.WriteLine("The " + GetName() + " leveled up to " + GetLevel());

            if (this.GetType().ToString() == "King_Of_Sky.Glider")
            {
                // Calc gains
                int healthGain = rand.Next(50, 150);
                int armorGain = rand.Next(10, 100);
                int speedGain = rand.Next(150, 300);

                Console.WriteLine("Health: " + GetTotalHealth() + " -> " + (GetTotalHealth() + healthGain));
                Console.WriteLine("Armor: " + GetArmor() + " -> " + (GetArmor() + armorGain));
                Console.WriteLine("Speed: " + GetSpeed() + " -> " + (GetSpeed() + speedGain));

                // Add gains
                SetTotalHealth(GetTotalHealth() + healthGain);
                SetArmor(GetArmor() + armorGain);
                SetSpeed(GetSpeed() + speedGain);
            }
            else if (this.GetType().ToString() == "King_Of_Sky.Bomber")
            {
                // Calc gains
                int healthGain = rand.Next(50, 150);
                int armorGain = rand.Next(10, 100);
                int speedGain = rand.Next(150, 300);

                Console.WriteLine("Health: " + GetTotalHealth() + " -> " + (GetTotalHealth() + healthGain));
                Console.WriteLine("Armor: " + GetArmor() + " -> " + (GetArmor() + armorGain));
                Console.WriteLine("Speed: " + GetSpeed() + " -> " + (GetSpeed() + speedGain));

                // Add gains
                SetTotalHealth(GetTotalHealth() + healthGain);
                SetArmor(GetArmor() + armorGain);
                SetSpeed(GetSpeed() + speedGain);
            }
            else if (this.GetType().ToString() == "King_Of_Sky.Cruiser")
            {
                // Calc gains
                int healthGain = rand.Next(50, 150);
                int armorGain = rand.Next(10, 100);
                int speedGain = rand.Next(150, 300);

                Console.WriteLine("Health: " + GetTotalHealth() + " -> " + (GetTotalHealth() + healthGain));
                Console.WriteLine("Armor: " + GetArmor() + " -> " + (GetArmor() + armorGain));
                Console.WriteLine("Speed: " + GetSpeed() + " -> " + (GetSpeed() + speedGain));

                // Add gains
                SetTotalHealth(GetTotalHealth() + healthGain);
                SetArmor(GetArmor() + armorGain);
                SetSpeed(GetSpeed() + speedGain);
            }
            Console.WriteLine();
        }

        public int GetTotalHealth()
        {
            return this.totalHealth;
        }

        public void SetTotalHealth(int totalHealth)
        {
            this.totalHealth = totalHealth;
        }

        public int GetTempHealth()
        {
            return this.tempHealth;
        }

        public void SetTempHealth(int tempHealth)
        {
            this.tempHealth = tempHealth;
        }

        public int GetArmor()
        {
            return this.armor;
        }

        public void SetArmor(int armor)
        {
            this.armor = armor;
        }

        public int GetSpeed()
        {
            if (speed - weight < 0)
                return 0;
            return speed - weight;
        }

        public void SetSpeed(int speed)
        {
            this.speed = speed;
        }

        public int GetWeight()
        {
            return this.weight;
        }

        public void SetWeight(int weight)
        {
            this.weight = weight;
        }

        public Hull GetHull()
        {
            return this.hull;
        }

        public Cannon GetCannon()
        {
            return this.cannon;
        }

        public Torpedo GetTorpedo()
        {
            return this.torpedo;
        }

        public Bomb GetBomb()
        {
            return this.bomb;
        }

        public void GetStats()
        {
            Console.WriteLine("The " + this.name + " is level " + this.level + " and has " + this.totalHealth + " health, " + this.armor + " armor, and " + this.speed + " speed");
        }

        public void GetStats(int i)
        {
            Console.WriteLine((i + 1) + ". The " + this.GetName() + " is level " + this.GetLevel() + " and has " + this.GetTotalHealth() + " health, " + this.GetArmor() + " armor, and " + this.GetSpeed() + " speed");
        }

        public void GetEquiptmentStats()
        {
            if (this.hull != null)
            {
                Console.WriteLine("The " + this.name + " has the " + this.hull.GetName() + " hull equipped which has " + this.hull.GetArmor() + " armor, and weighs " + this.hull.GetWeight());
            }
            else
            {
                Console.WriteLine("The " + this.name + " does not have a hull equipped");
            }
            if (this.cannon != null)
            {
                Console.WriteLine("The " + this.name + " has the " + this.cannon.GetName() + " cannon equipped which has " + this.cannon.GetPower() + " power and " + this.cannon.GetAccuracy() + "% accuracy, and weighs " + this.cannon.GetWeight());
            }
            else
            {
                Console.WriteLine("The " + this.name + " does not have a cannon equipped");
            }
            if (this.torpedo != null)
            {
                Console.WriteLine("The " + this.name + " has the " + this.torpedo.GetName() + " torpedo equipped which has " + this.torpedo.GetPower() + " power and " + this.torpedo.GetAccuracy() + "% accuracy, and weighs " + this.torpedo.GetWeight());
            }
            else
            {
                Console.WriteLine("The " + this.name + " does not have torpedos equipped");
            }
            if (this.bomb != null)
            {
                Console.WriteLine("The " + this.name + " has the " + this.bomb.GetName() + " bomb equipped which has " + this.bomb.GetPower() + " power and " + this.bomb.GetAccuracy() + "% accuracy, and weighs " + this.bomb.GetWeight());
            }
            else
            {
                Console.WriteLine("The " + this.name + " does not have bombs equipped");
            }
            Console.WriteLine();
        }

        public void EquipHull(Hull hull)
        {
            if (level >= hull.GetRequiredLevel())
            {
                if (this.hull != null)
                {
                    this.armor -= this.hull.GetArmor();
                    this.weight -= this.hull.GetWeight();
                }
                this.hull = hull;
                this.armor += hull.GetArmor();
                this.weight += hull.GetWeight();
                Console.WriteLine("The " + GetName() + " has the " + hull.GetName() + " Hull equipped\n");
            }
            else
            {
                Console.WriteLine("The entered hull or ship does not exist\n");
            }
        }

        public void EquipCannon(Cannon cannon)
        {
            if (level >= cannon.GetRequiredLevel())
            {
                if (this.cannon != null)
                {
                    this.weight -= this.cannon.GetWeight();
                }
                this.cannon = cannon;
                this.weight += cannon.GetWeight();
                Console.WriteLine("The " + GetName() + " has the " + cannon.GetName() + " cannon equipped\n");
            }
            else
            {
                Console.WriteLine("The entered cannon or ship does not exist\n");
            }
        }

        public void EquipTorpedo(Torpedo torpedo)
        {
            if (level >= torpedo.GetRequiredLevel())
            {
                if (this.torpedo != null)
                {
                    this.weight -= this.torpedo.GetWeight();
                }
                this.torpedo = torpedo;
                this.weight += torpedo.GetWeight();
                Console.WriteLine("The " + GetName() + " has the " + torpedo.GetName() + " torpedo equipped\n");
            }
            else
            {
                Console.WriteLine("The entered torpedo or ship does not exist\n");
            }
        }

        public void EquipBomb(Bomb bomb)
        {
            if (level >= bomb.GetRequiredLevel())
            {
                if (this.bomb != null)
                {
                    this.weight -= this.bomb.GetWeight();
                }
                this.bomb = bomb;
                this.weight += bomb.GetWeight();
                Console.WriteLine("The " + GetName() + " has the " + bomb.GetName() + " bomb equipped\n");
            }
            else
            {
                Console.WriteLine("The entered bomb or ship does not exist\n");
            }
        }

        public void InvalidInput()
        {
            Console.WriteLine("The entered input did not match any of the available commands\n");
        }
    }

    class Glider : Ship
    {
        public Glider(string name)
        {
            Random rand = new Random();
            this.SetName(name);
            this.SetLevel(1);
            this.SetTotalHealth(rand.Next(1000, 2000));
            this.SetArmor(rand.Next(10, 80));
            this.SetSpeed(rand.Next(100, 200));
            this.SetWeight(0);
        }
    }

    class Bomber : Ship
    {
        public Bomber(string name)
        {
            Random rand = new Random();
            this.SetName(name);
            this.SetLevel(1);
            this.SetTotalHealth(rand.Next(2500, 5000));
            this.SetArmor(rand.Next(100, 200));
            this.SetSpeed(rand.Next(10, 80));
            this.SetWeight(0);
        }
    }

    class Cruiser : Ship
    {
        Random rand = new Random();
        public Cruiser(string name)
        {
            this.SetName(name);
            this.SetLevel(1);
            this.SetTotalHealth(rand.Next(1500, 3000));
            this.SetArmor(rand.Next(50, 150));
            this.SetSpeed(rand.Next(50, 150));
            this.SetWeight(0);
        }
    }
}
