﻿using System;
using System.Collections.Generic;

namespace King_Of_The_Sky
{
    class Program
    {
        static void Main(string[] args)
        { 
            Command commandCenter = new Command();
            commandCenter.getAccountManager().welcome();
            commandCenter.getAccountManager().loginOrSignUp();
            
            while (true)
            {
                commandCenter.enterCommand();
            }
        }
    }

    class Command
    {
        private ShipFactory factory;
        private Inventory inventory;
        private AccountManager accountManager;

        public Command()
        {
            factory = new ShipFactory();
            inventory = new Inventory();
            accountManager = new AccountManager();
        }

        public ShipFactory GetShipFactory()
        {
            return this.factory;
        }

        public Inventory GetInventory()
        {
            return this.inventory;
        }

        public AccountManager getAccountManager()
        {
            return this.accountManager;
        }

        public void enterCommand()
        {
            Console.WriteLine("Available Commands:\n<'build' or 'b'>  - Add ships to your armada\n<'ships' or 's'> " +
                " - View ships in your armada\n<'combat' or 'c'> - Battle other ships or train your own\n<'quit' or 'q'>   - Close application\n");
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

            if      (command[0].ToLower() == "b" || command[0].ToLower() == "build")
            {
                if(command.Length == 1)
                {
                    Console.WriteLine("There are three types of ships you can build: Glider, Crusier, and Bomber\nTo build one enter:\n<'build' or 'b'> <'glider' or 'g' or 'crusier' or 'c' or 'bomber' or 'b'> <'name'>\nThe name can be multiple words\nTo get more information about a certain type of ship try to build it without a name\n");
                }
                else if (command.Length == 2)
                {
                    if (command[1].ToLower() == "glider" || command[1].ToLower() == "g")
                    {
                        Console.WriteLine("Gliders are fast and evasive but cannot take much damage\n");
                    }
                    else if (command[1].ToLower() == "crusier" || command[1].ToLower() == "c")
                    {
                        Console.WriteLine("Crusiers are standard battleships with all-around average stats\n");
                    }
                    else if (command[1].ToLower() == "bomber" || command[1].ToLower() == "b")
                    {
                        Console.WriteLine("Bombers are slow tanks that can take a hit\n");
                    }
                    else
                    {
                        invalidInput();
                    }
                }
                else if (command.Length > 2)
                {
                    string shipName = command[2];
                    for (int i = 3; i < command.Length; i++)
                    {
                        shipName = shipName + " " + command[i];
                    }

                    if (command[1].ToLower() == "bomber" || command[1].ToLower() == "b")
                    {
                        accountManager.getCurrentPlayer().placeShipInArray(factory.createBomber(shipName));
                    }
                    else if (command[1].ToLower() == "crusier" || command[1].ToLower() == "c")
                    {
                        accountManager.getCurrentPlayer().placeShipInArray(factory.createCrusier(shipName));
                    }
                    else if (command[1].ToLower() == "glider" || command[1].ToLower() == "g")
                    {
                        accountManager.getCurrentPlayer().placeShipInArray(factory.createGlider(shipName));
                    }
                }
            }
            else if (command[0].ToLower() == "s" || command[0].ToLower() == "ship" || command[0].ToLower() == "ships")
            {
                if (command.Length == 1)
                {
                    Console.WriteLine("Available Commands:\n<'ships'>              - View all ships in armada\n<'ships'> <'{number}'> - View specific deatils of one ship in armada\n<'ships'> <'equip'>    - Outfit your ships with equiptment\n\nShips in armada:");
                    for (int i = 0; i < accountManager.getCurrentPlayer().getShips().Length; i++)
                    {
                        try
                        {
                            accountManager.getCurrentPlayer().getShips()[i].getStats(i);
                        }
                        catch (NullReferenceException e)
                        {
                            Console.WriteLine((i + 1) + ". ~Empty~");
                        }
                    }
                    Console.WriteLine();
                }
                else if (int.TryParse(command[1], out int shipNum))
                {
                    accountManager.getCurrentPlayer().getShips()[shipNum-1].getStats();
                    accountManager.getCurrentPlayer().getShips()[shipNum-1].getEquiptmentStats();
                }
                else if (command[1].ToLower() == "e" || command[1].ToLower() == "equip")
                {
                    if (command.Length == 2)
                    {
                        Console.WriteLine("Here you can outfit your ship with different equiptment.\n\nAvailable Commands:\n<'ships'> " +
                            "<'equip'> - View equiptment and available commands\n<'ships'> <'equip'> <'hulls' or 'h' or 'cannons' or 'c' " +
                            "or 'torpedos' or 't' or 'bombs' or 'b'> - View available equiptment of input type\n<'ships'> <'hulls' or 'h'> " +
                            "or <'cannons' or 'c' or 'torpedos' or 't' or 'bombs' or 'b'> <'{equiptment number}'> <'{ship number}'> - Equipts " +
                            "the provided ship with the provided equiptment\n");
                        inventory.listHulls(accountManager.getCurrentPlayer());
                        inventory.listCannons(accountManager.getCurrentPlayer());
                        inventory.listTorpedos(accountManager.getCurrentPlayer());
                        inventory.listBombs(accountManager.getCurrentPlayer());
                    }
                    else if (command.Length == 3)
                    {
                        if (command[2].ToLower() == "h" || command[1].ToLower() == "hulls")
                        {
                            inventory.listHulls(accountManager.getCurrentPlayer());
                        }
                        else if (command[2].ToLower() == "c" || command[2].ToLower() == "cannons")
                        {
                            inventory.listCannons(accountManager.getCurrentPlayer());
                        }
                        else if (command[2].ToLower() == "t" || command[2].ToLower() == "torpedos")
                        {
                            inventory.listTorpedos(accountManager.getCurrentPlayer());
                        }
                        else if (command[2].ToLower() == "b" || command[2].ToLower() == "bombs")
                        {
                            inventory.listBombs(accountManager.getCurrentPlayer());
                        }
                    }
                    else if (command.Length == 5)
                    {
                        if (command[2].ToLower() == "h")
                        {
                            try
                            {
                                accountManager.getCurrentPlayer().getShips()[int.Parse(command[4]) - 1].equipHull(inventory.getHulls()[int.Parse(command[3]) - 1]);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("The entered hull or ship does not exist\n");
                            }
                        }
                        else if (command[2].ToLower() == "c")
                        {
                            try
                            {
                                accountManager.getCurrentPlayer().getShips()[int.Parse(command[4]) - 1].equipCannon(inventory.getCannons()[int.Parse(command[3]) - 1]);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("The entered cannon or ship does not exist\n");
                            }
                        }
                        else if (command[2].ToLower() == "t")
                        {
                            try
                            {
                                accountManager.getCurrentPlayer().getShips()[int.Parse(command[4]) - 1].equipTorpedo(inventory.getTorpedos()[int.Parse(command[3]) - 1]);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("The entered torpedo or ship does not exist\n");
                            }
                        }
                        else if (command[2].ToLower() == "b")
                        {
                            try
                            {
                                accountManager.getCurrentPlayer().getShips()[int.Parse(command[4]) - 1].equipBomb(inventory.getBombs()[int.Parse(command[3]) - 1]);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("The entered bomb or ship does not exist\n");
                            }
                        }
                        else
                        {
                            invalidInput();
                        }
                    }
                }
                else
                {
                    invalidInput();
                }
            }
            else if (command[0].ToLower() == "c" || command[0].ToLower() == "combat")
            {

            }
            else if (command[0].ToLower() == "p" || command[0].ToLower() == "player")
            {
                if(command.Length == 1)
                {
                    Console.WriteLine("KOS Players Include:");
                    for (int i = 0; i < accountManager.getPlayerList().Count; i++)
                    {
                        Console.WriteLine("Captain " + accountManager.getPlayerList()[i].getName());
                    }
                    Console.WriteLine("\nYou are currently signed in as Captain " + accountManager.getCurrentPlayer().getName() + "\n");
                }
                else if (command[1].ToLower() == "l" || command[1].ToLower() == "logout")
                {
                    accountManager.logout();
                }
                else
                {
                    invalidInput();
                }
            }
            else if (command[0].ToLower() == "q" || command[0].ToLower() == "quit" || command[0] == "")
            {
                Environment.Exit(0);
            }
            else
            {
                invalidInput();
            }
        }

        public void invalidInput()
        {
            Console.WriteLine("The entered input did not match any of the available commands\n");
        }
    }

    class AccountManager
    {
        private List<Player> players;
        private Player currentPlayer;

        public AccountManager()
        {
            players = new List<Player>();
        }

        public List<Player> getPlayerList()
        {
            return this.players;
        }

        public Player getCurrentPlayer()
        {
            return this.currentPlayer;
        }

        public void loginOrSignUp()
        {
            Console.WriteLine("Do you have a KOS account? Enter 'yes' or 'no' below:");
            string doYouHaveAccount = Console.ReadLine();
            if (doYouHaveAccount == "yes" || doYouHaveAccount == "y")
            {
                if (!doAccountsExist())
                {
                    Console.WriteLine("Your account was not found\n");
                    loginOrSignUp();
                    return;
                }
                Console.WriteLine("Enter username below:");
                string username = Console.ReadLine();
                Console.WriteLine("Enter password below:");
                string password = Console.ReadLine();
                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i].getName() == username && players[i].getPassword() == password)
                    {
                        currentPlayer = players[i];
                        Console.WriteLine("Welcome back captain " + players[i].getName() + "\n");
                        Console.WriteLine("Enter a command to get started\n\nAvailable Commands:\n<'build' or 'b'>  - Add ships to your armada\n<'ships' or 's'> " +
                            " - View ships in your armada\n<'combat' or 'c'> - Battle other ships or train your own\n<'quit' or 'q'>   - Close application\n");
                        return;
                    }
                }
                Console.WriteLine("Your account was not found\n");
                loginOrSignUp();
                return;
            }
            else
            {
                Console.WriteLine("To create a new player enter username below:");
                string username = Console.ReadLine();
                if (username == "")
                {
                    Console.WriteLine("You did not enter a username\n");
                    loginOrSignUp();
                    return;
                }
                if (isUsernameTaken(username))
                {
                    loginOrSignUp();
                    return;
                }
                else
                {
                    Console.WriteLine("Enter password below:");
                    string password = Console.ReadLine();
                    if (password == "")
                    {
                        Console.WriteLine("You did not enter a password\n");
                        loginOrSignUp();
                        return;
                    }
                    else
                    {
                        Console.WriteLine();
                        Player player = new Player(username, password);
                        players.Add(player);
                        currentPlayer = player;
                        Console.WriteLine("Welcome to KOS Captain " + currentPlayer.getName() + "\n");
                    }
                }
            }
        }

        public bool doAccountsExist()
        {
            return players.Count > 0;
        }

        public bool isUsernameTaken(String username)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (username.ToLower() == players[i].getName().ToLower())
                {
                    Console.WriteLine("Username '" + username.ToLower() + "' is taken by another user\n");
                    return true;
                }
            }
            return false;
        }

        public void welcome()
        {
            Console.WriteLine("/ * * * * * * * * * * * * * /\n Welcome to King Of The Sky! \n/ * * * * * * * * * * * * * /\n");
        }

        public void logout()
        {
            Console.WriteLine("Captain " + currentPlayer.getName() + " is signing off\n");
            currentPlayer = null;
            welcome();
            loginOrSignUp();
        }
    }

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

        public string getName()
        {
            return this.name;
        }

        public string getPassword()
        {
            return this.password;
        }

        public Ship[] getShips()
        {
            return this.ships;
        }

        public short getLevel()
        {
            return this.level;
        }

        public void setLevel(short level)
        {
            this.level = level;
        }

        public void placeShipInArray(Ship newShip)
        {
            for (int i = 0; i < getShips().Length; i++)
            {
                if (getShips()[i] == null)
                {
                    getShips()[i] = newShip;
                    Console.WriteLine("The " + newShip.getName() + " is number " + (i + 1) + " in your armada\n");
                    return;
                }
            }
            Console.WriteLine("Your armada is full. Enter the number of the ship you would like to replace, or enter anything else to cancel ship creation:");
            string command2 = Console.ReadLine();
            if (command2[0].Equals('1') || command2[0].Equals('2') || command2[0].Equals('3') || command2[0].Equals('4') || command2[0].Equals('5'))
            {
                Console.WriteLine("The " + getShips()[int.Parse((command2[0]).ToString()) - 1]);
                getShips()[int.Parse((command2[0]).ToString()) - 1] = newShip;

            }
        }
    }

    class Inventory
    {
        private Hull[] hulls;
        private Cannon[] cannons;
        private Torpedo[] torpedos;
        private Bomb[] bombs;

        public Inventory()
        {
            this.hulls = new Hull[] { new Hull("Wood", 200, 0, 0), new Hull("Iron", 500, 50, 10), new Hull("Steel", 1500, 100, 20) };
            this.cannons = new Cannon[] { new Cannon("Standard Cannon", 100, 0, 100, 0), new Cannon("Ion Cannon", 300, 30, 100, 10), new Cannon("Dragon Fire", 500, 200, 80, 20) };
            this.torpedos = new Torpedo[] { new Torpedo("Standard Torpedo", 70, 0, 90, 0), new Torpedo("Blue Lightning", 100, 60, 90, 10), new Torpedo("Black Thunder", 1000, 90, 70, 20) };
            this.bombs = new Bomb[] { new Bomb("Standard Bomb", 120, 0, 80, 0), new Bomb("TNT", 400, 65, 55, 10), new Bomb("Dynamite", 600, 85, 75, 20) };
        }

        public Hull[] getHulls()
        {
            return this.hulls;
        }

        public Cannon[] getCannons()
        {
            return this.cannons;
        }

        public Torpedo[] getTorpedos()
        {
            return this.torpedos;
        }

        public Bomb[] getBombs()
        {
            return this.bombs;
        }

        public void listHulls(Player currentPlayer)
        {
            Console.WriteLine("Available Hulls:");
            for (int i = 0; i < getHulls().Length; i++)
            {
                if (currentPlayer.getLevel() >= getHulls()[i].getRequiredLevel())
                {
                    Console.WriteLine((i + 1) + ". " + getHulls()[i].getName() + ": Armor - " + getHulls()[i].getArmor() + ", Weight - " + getHulls()[i].getWeight());
                }
            }
            Console.WriteLine();
        }

        public void listCannons(Player currentPlayer)
        {
            Console.WriteLine("Available Cannons:");
            for (int i = 0; i < getCannons().Length; i++)
            {
                if (currentPlayer.getLevel() >= getCannons()[i].getRequiredLevel())
                {
                    Console.WriteLine((i + 1) + ". " + getCannons()[i].getName() + ": Power - " + getCannons()[i].getPower() + ", Weight - " + getCannons()[i].getWeight() + ", Accuracy - " + getCannons()[i].getAccuracy());
                }
            }
            Console.WriteLine();
        }

        public void listTorpedos(Player currentPlayer)
        {
            Console.WriteLine("Available Torpedos:");
            for (int i = 0; i < getTorpedos().Length; i++)
            {
                if (currentPlayer.getLevel() >= getTorpedos()[i].getRequiredLevel())
                {
                    Console.WriteLine((i + 1) + ". " + getTorpedos()[i].getName() + ": Power - " + getTorpedos()[i].getPower() + ", Weight - " + getTorpedos()[i].getWeight() + ", Accuracy - " + getTorpedos()[i].getAccuracy());
                }
            }
            Console.WriteLine();
        }

        public void listBombs(Player currentPlayer)
        {
            Console.WriteLine("Available Bombs:");
            for (int i = 0; i < getBombs().Length; i++)
            {
                if (currentPlayer.getLevel() >= getBombs()[i].getRequiredLevel())
                {
                    Console.WriteLine((i + 1) + ". " + getBombs()[i].getName() + ": Power - " + getBombs()[i].getPower() + ", Weight - " + getBombs()[i].getWeight() + ", Accuracy - " + getBombs()[i].getAccuracy());
                }
            }
            Console.WriteLine();
        }
    }

    class ShipFactory
    {
        public Cruiser createCrusier(string name)
        {
            Cruiser c = new Cruiser(name);
            Console.WriteLine("You have built a new Cruiser named: The " + c.getName() + "\nStats:\nHealth: " + c.getHealth() + "\nArmor: " + c.getArmor() + "\nSpeed: " + c.getSpeed() + "\n");
            return c;
        }

        public Glider createGlider(string name)
        {
            Glider g = new Glider(name);
            Console.WriteLine("You have built a new Glider named: The " + g.getName() + "\nStats:\nHealth: " + g.getHealth() + "\nArmor: " + g.getArmor() + "\nSpeed: " + g.getSpeed() + "\n");
            return g;
        }

        public Bomber createBomber(string name)
        {
            Bomber b = new Bomber(name);
            Console.WriteLine("You have built a new Bomber named: The " + b.getName() + "\nStats:\nHealth: " + b.getHealth() + "\nArmor: " + b.getArmor() + "\nSpeed: " + b.getSpeed() + "\n");
            return b;
        }
    }

    class Ship
    {
        private string name;
        private short level;
        private int health;
        private int armor;
        private int speed;
        private int build_points;
        private Hull hull;
        private Cannon cannon;
        private Torpedo torpedo;
        private Bomb bomb;

        public string getName()
        {
            return this.name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public short getLevel()
        {
            return this.level;
        }

        public void setLevel(short lvl)
        {
            this.level = lvl;
        }

        public int getHealth()
        {
            return this.health;
        }

        public void setHealth(int health)
        {
            this.health = health;
        }

        public int getArmor()
        {
            return this.armor;
        }

        public void setArmor(int armor)
        {
            this.armor = armor;
        }

        public int getSpeed()
        {
            if (this.speed < 0)
                return 0;
            return this.speed;
        }

        public void setSpeed(int speed)
        {
            this.speed = speed;
        }

        public int getBuildPoints()
        {
            return this.build_points;
        }

        public void setBuildPoints(int buildPoints)
        {
            this.build_points = build_points;
        }

        public Hull getHull()
        {
            return this.hull;
        }

        public Cannon getCannon()
        {
            return this.cannon;
        }

        public Torpedo getTorpedo()
        {
            return this.torpedo;
        }

        public Bomb getBomb()
        {
            return this.bomb;
        }

        public void getStats()
        {
            Console.WriteLine("The " + this.name + " is level " + this.level + " and has " + this.health + " health, " + this.armor + " armor, and " + this.speed + " speed");
        }

        public void getStats(int i)
        {
            Console.WriteLine((i+1) + ". The " + this.getName() + " is level " + this.getLevel() + " and has " + this.getHealth() + " health, " + this.getArmor() + " armor, and " + this.getSpeed() + " speed");
        }

        public void getEquiptmentStats()
        {
            if (this.hull != null)
            {
                Console.WriteLine("The " + this.name + " has the " + this.hull.getName() + " hull equipped which has " + this.hull.getArmor() + " armor, and weighs " + this.hull.getWeight());
            }
            else
            {
                Console.WriteLine("The " + this.name + " does not have a hull equipped");
            }
            if (this.cannon != null)
            {
                Console.WriteLine("The " + this.name + " has the " + this.cannon.getName() + " cannon equipped which has " + this.cannon.getPower() + " power and " + this.cannon.getAccuracy() + "% accuracy, and weighs " + this.cannon.getWeight());
            }
            else
            {
                Console.WriteLine("The " + this.name + " does not have a cannon equipped");
            }
            if (this.torpedo != null)
            {
                Console.WriteLine("The " + this.name + " has the " + this.torpedo.getName() + " torpedo equipped which has " + this.torpedo.getPower() + " power and " + this.torpedo.getAccuracy() + "% accuracy, and weighs " + this.torpedo.getWeight());
            }
            else
            {
                Console.WriteLine("The " + this.name + " does not have torpedos equipped");
            }
            if (this.bomb != null)
            {
                Console.WriteLine("The " + this.name + " has the " + this.bomb.getName() + " bomb equipped which has " + this.bomb.getPower() + " power and " + this.bomb.getAccuracy() + "% accuracy, and weighs " + this.bomb.getWeight());
            }
            else
            {
                Console.WriteLine("The " + this.name + " does not have bombs equipped");
            }
            Console.WriteLine();
        }

        public void equipHull(Hull hull)
        {
            if (level >= hull.getRequiredLevel())
            {
                if (this.hull != null)
                {
                    this.armor -= this.hull.getArmor();
                    this.speed += this.hull.getWeight();
                }
                this.hull = hull;
                this.armor += hull.getArmor();
                this.speed -= hull.getWeight();
                Console.WriteLine("The " + getName() + " has the " + hull.getName() + " Hull equipped\n");
            }
            else
            {
                Console.WriteLine("The entered hull or ship does not exist\n");
            }
        }

        public void equipCannon(Cannon cannon)
        {
            if (level >= cannon.getRequiredLevel())
            {
                if (this.cannon != null)
                {
                    this.speed += this.cannon.getWeight();
                }
                this.cannon = cannon;
                this.speed -= cannon.getWeight();
                Console.WriteLine("The " + getName() + " has the " + cannon.getName() + " cannon equipped\n");
            }
            else
            {
                Console.WriteLine("The entered cannon or ship does not exist\n");
            }
        }

        public void equipTorpedo(Torpedo torpedo)
        {
            if (level >= torpedo.getRequiredLevel())
            {
                if (this.torpedo != null)
                {
                    this.speed += this.torpedo.getWeight();
                }
                this.torpedo = torpedo;
                this.speed -= torpedo.getWeight();
                Console.WriteLine("The " + getName() + " has the " + torpedo.getName() + " torpedo equipped\n");
            }
            else
            {
                Console.WriteLine("The entered torpedo or ship does not exist\n");
            }
        }

        public void equipBomb(Bomb bomb)
        {
            if (level >= bomb.getRequiredLevel())
            {
                if (this.bomb != null)
                {
                    this.speed += this.bomb.getWeight();
                }
                this.bomb = bomb;
                this.speed -= bomb.getWeight();
                Console.WriteLine("The " + getName() + " has the " + bomb.getName() + " bomb equipped\n");
            }
            else
            {
                Console.WriteLine("The entered bomb or ship does not exist\n");
            }
        }

        public void invalidInput()
        {
            Console.WriteLine("The entered input did not match any of the available commands\n");
        }
    }

    class Glider : Ship
    {
        public Glider(string name)
        {
            Random rand = new Random();
            this.setName(name);
            this.setLevel(0);
            this.setHealth(rand.Next(1000, 2000));
            this.setArmor(rand.Next(10, 80));
            this.setSpeed(rand.Next(100, 200));
            this.setBuildPoints(0);
        }
    }

    class Bomber : Ship
    {
        public Bomber(string name)
        {
            Random rand = new Random();
            this.setName(name);
            this.setLevel(0);
            this.setHealth(rand.Next(2500, 5000));
            this.setArmor(rand.Next(100, 200));
            this.setSpeed(rand.Next(10, 80));
            this.setBuildPoints(0);
        }
    }

    class Cruiser : Ship
    {
        public Cruiser(string name)
        {
            Random rand = new Random();
            this.setName(name);
            this.setLevel(0);
            this.setHealth(rand.Next(1500, 3000));
            this.setArmor(rand.Next(50, 150));
            this.setSpeed(rand.Next(50, 150));
            this.setBuildPoints(0);
        }
    }

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

        public string getName()
        {
            return this.name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public int getArmor()
        {
            return this.armor;
        }

        public void setArmor(int armor)
        {
            this.armor = armor;
        }

        public int getWeight()
        {
            return this.weight;
        }

        public void setWeight(int weight)
        {
            this.weight = weight;
        }

        public short getRequiredLevel()
        {
            return this.requiredLevel;
        }

        public void setRequiredLevel(short requiredLevel)
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

        public string getName()
        {
            return this.name;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        public int getPower()
        {
            return this.power;
        }

        public void setPower(int power)
        {
            this.power = power;
        }

        public int getWeight()
        {
            return this.weight;
        }

        public void setWeight(int weight)
        {
            this.weight = weight;
        }

        public short getAccuracy()
        {
            return this.accuracy;
        }

        public void setAccuracy(short accuracy)
        {
            this.accuracy = accuracy;
        }

        public short getRequiredLevel()
        {
            return this.requiredLevel;
        }

        public void setRequiredLevel(short requiredLevel)
        {
            this.requiredLevel = requiredLevel;
        }
    }

    class Cannon : Weapon
    {
        public Cannon(string name, int power, int weight, short accuracy, short requiredLevel)
        {
            this.setName(name);
            this.setPower(power);
            this.setWeight(weight);
            this.setAccuracy(accuracy);
            this.setRequiredLevel(requiredLevel);
        }
    }

    class Torpedo : Weapon
    {
        public Torpedo(string name, int power, int weight, short accuracy, short requiredLevel)
        {
            this.setName(name);
            this.setPower(power);
            this.setWeight(weight);
            this.setAccuracy(accuracy);
            this.setRequiredLevel(requiredLevel);
        }
    }

    class Bomb : Weapon
    {
        public Bomb(string name, int power, int weight, short accuracy, short requiredLevel)
        {
            this.setName(name);
            this.setPower(power);
            this.setWeight(weight);
            this.setAccuracy(accuracy);
            this.setRequiredLevel(requiredLevel);
        }
    }
}
