using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingOfTheSky.src
{
    class Hull 
    {
        private string name;
        private int armor;
        private int weight;
        private short requiredLevel;

        public Hull(string name, int health, int weight, short requiredLevel)
        {
            this.name = name;
            this.armor = health;
            this.weight = weight;
            this.requiredLevel = requiredLevel;
        }

        public string GetName()
        {
            return this.name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public int GetArmor()
        {
            return this.armor;
        }

        public void SetArmor(int armor)
        {
            this.armor = armor;
        }

        public int GetWeight()
        {
            return this.weight;
        }

        public void SetWeight(int weight)
        {
            this.weight = weight;
        }

        public short GetRequiredLevel()
        {
            return this.requiredLevel;
        }

        public void SetRequiredLevel(short requiredLevel)
        {
            this.requiredLevel = requiredLevel;
        }
    }

    class Weapon 
    {
        private string name;
        private int power;
        private int weight;
        private short accuracy;
        private short requiredLevel;

        public string GetName()
        {
            return this.name;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public int GetPower()
        {
            return this.power;
        }

        public void SetPower(int power)
        {
            this.power = power;
        }

        public int GetWeight()
        {
            return this.weight;
        }

        public void SetWeight(int weight)
        {
            this.weight = weight;
        }

        public short GetAccuracy()
        {
            return this.accuracy;
        }

        public void SetAccuracy(short accuracy)
        {
            this.accuracy = accuracy;
        }

        public short GetRequiredLevel()
        {
            return this.requiredLevel;
        }

        public void SetRequiredLevel(short requiredLevel)
        {
            this.requiredLevel = requiredLevel;
        }
    }

    class Cannon : Weapon
    {
        public Cannon(string name, int power, int weight, short accuracy, short requiredLevel)
        {
            this.SetName(name);
            this.SetPower(power);
            this.SetWeight(weight);
            this.SetAccuracy(accuracy);
            this.SetRequiredLevel(requiredLevel);
        }
    }

    class Torpedo : Weapon
    {
        public Torpedo(string name, int power, int weight, short accuracy, short requiredLevel)
        {
            this.SetName(name);
            this.SetPower(power);
            this.SetWeight(weight);
            this.SetAccuracy(accuracy);
            this.SetRequiredLevel(requiredLevel);
        }
    }

    class Bomb : Weapon
    {
        public Bomb(string name, int power, int weight, short accuracy, short requiredLevel)
        {
            this.SetName(name);
            this.SetPower(power);
            this.SetWeight(weight);
            this.SetAccuracy(accuracy);
            this.SetRequiredLevel(requiredLevel);
        }
    }
}
