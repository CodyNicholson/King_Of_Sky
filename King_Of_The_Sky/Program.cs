using System;
using System.Collections.Generic;

namespace King_Of_The_Sky
{
    class Program
    {
        static void Main(string[] args)
        { 
            Command commandCenter = new Command();
            commandCenter.GetAccountManager().Welcome();
            commandCenter.GetAccountManager().LoginOrSignUp();
            
            while (true)
            {
                commandCenter.EnterCommand();
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

        public AccountManager GetAccountManager()
        {
            return this.accountManager;
        }

        public void EnterCommand()
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
                        InvalidInput();
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
                        accountManager.GetCurrentPlayer().PlaceShipInArray(factory.CreateBomber(shipName));
                    }
                    else if (command[1].ToLower() == "crusier" || command[1].ToLower() == "c")
                    {
                        accountManager.GetCurrentPlayer().PlaceShipInArray(factory.CreateCrusier(shipName));
                    }
                    else if (command[1].ToLower() == "glider" || command[1].ToLower() == "g")
                    {
                        accountManager.GetCurrentPlayer().PlaceShipInArray(factory.CreateGlider(shipName));
                    }
                }
            }
            else if (command[0].ToLower() == "s" || command[0].ToLower() == "ship" || command[0].ToLower() == "ships")
            {
                if (command.Length == 1)
                {
                    Console.WriteLine("Available Commands:\n<'ships'>              - View all ships in armada\n<'ships'> <'{number}'> - View specific deatils of one ship in armada\n<'ships'> <'equip'>    - Outfit your ships with equiptment\n\nShips in armada:");
                    for (int i = 0; i < accountManager.GetCurrentPlayer().GetShips().Length; i++)
                    {
                        try
                        {
                            accountManager.GetCurrentPlayer().GetShips()[i].GetStats(i);
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
                    accountManager.GetCurrentPlayer().GetShips()[shipNum-1].GetStats();
                    accountManager.GetCurrentPlayer().GetShips()[shipNum-1].GetEquiptmentStats();
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
                        inventory.ListHulls(accountManager.GetCurrentPlayer());
                        inventory.ListCannons(accountManager.GetCurrentPlayer());
                        inventory.ListTorpedos(accountManager.GetCurrentPlayer());
                        inventory.ListBombs(accountManager.GetCurrentPlayer());
                    }
                    else if (command.Length == 3)
                    {
                        if (command[2].ToLower() == "h" || command[1].ToLower() == "hulls")
                        {
                            inventory.ListHulls(accountManager.GetCurrentPlayer());
                        }
                        else if (command[2].ToLower() == "c" || command[2].ToLower() == "cannons")
                        {
                            inventory.ListCannons(accountManager.GetCurrentPlayer());
                        }
                        else if (command[2].ToLower() == "t" || command[2].ToLower() == "torpedos")
                        {
                            inventory.ListTorpedos(accountManager.GetCurrentPlayer());
                        }
                        else if (command[2].ToLower() == "b" || command[2].ToLower() == "bombs")
                        {
                            inventory.ListBombs(accountManager.GetCurrentPlayer());
                        }
                    }
                    else if (command.Length == 5)
                    {
                        if (command[2].ToLower() == "h")
                        {
                            try
                            {
                                accountManager.GetCurrentPlayer().GetShips()[int.Parse(command[4]) - 1].EquipHull(inventory.GetHulls()[int.Parse(command[3]) - 1]);
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
                                accountManager.GetCurrentPlayer().GetShips()[int.Parse(command[4]) - 1].EquipCannon(inventory.GetCannons()[int.Parse(command[3]) - 1]);
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
                                accountManager.GetCurrentPlayer().GetShips()[int.Parse(command[4]) - 1].EquipTorpedo(inventory.GetTorpedos()[int.Parse(command[3]) - 1]);
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
                                accountManager.GetCurrentPlayer().GetShips()[int.Parse(command[4]) - 1].EquipBomb(inventory.GetBombs()[int.Parse(command[3]) - 1]);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("The entered bomb or ship does not exist\n");
                            }
                        }
                        else
                        {
                            InvalidInput();
                        }
                    }
                }
                else
                {
                    InvalidInput();
                }
            }
            else if (command[0].ToLower() == "c" || command[0].ToLower() == "combat")
            {

            }
            else if (command[0].ToLower() == "p" || command[0].ToLower() == "player")
            {
                if(command.Length == 1)
                {
                    Console.WriteLine("Available Commands:\n<'player' or 'p'>                   - Lists all KOS players\n<'player' or 'p'> <'logout' or 'l'> - Logs the current player out and returns to welcome screen\n\nKOS Players Include:");
                    for (int i = 0; i < accountManager.GetPlayerList().Count; i++)
                    {
                        Console.WriteLine("- Captain " + accountManager.GetPlayerList()[i].GetName());
                    }
                    Console.WriteLine("\nYou are currently signed in as Captain " + accountManager.GetCurrentPlayer().GetName() + "\n");
                }
                else if (command[1].ToLower() == "l" || command[1].ToLower() == "logout")
                {
                    accountManager.Logout();
                }
                else
                {
                    InvalidInput();
                }
            }
            else if (command[0].ToLower() == "q" || command[0].ToLower() == "quit" || command[0] == "")
            {
                Environment.Exit(0);
            }
            else
            {
                InvalidInput();
            }
        }

        public void InvalidInput()
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

        public List<Player> GetPlayerList()
        {
            return this.players;
        }

        public Player GetCurrentPlayer()
        {
            return this.currentPlayer;
        }

        public void LoginOrSignUp()
        {
            Console.WriteLine("Do you have a KOS account? Enter 'yes' or 'no' below:");
            string doYouHaveAccount = Console.ReadLine();
            if (doYouHaveAccount == "yes" || doYouHaveAccount == "y")
            {
                if (!DoAccountsExist())
                {
                    Console.WriteLine("Your account was not found\n");
                    LoginOrSignUp();
                    return;
                }
                Console.WriteLine("Enter username below:");
                string username = Console.ReadLine();
                Console.WriteLine("Enter password below:");
                string password = Console.ReadLine();
                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i].GetName() == username && players[i].GetPassword() == password)
                    {
                        currentPlayer = players[i];
                        Console.WriteLine("Welcome back captain " + players[i].GetName() + "\n");
                        Console.WriteLine("Enter a command to get started\n\nAvailable Commands:\n<'build' or 'b'>  - Add ships to your armada\n<'ships' or 's'> " +
                            " - View ships in your armada\n<'combat' or 'c'> - Battle other ships or train your own\n<'quit' or 'q'>   - Close application\n");
                        return;
                    }
                }
                Console.WriteLine("Your account was not found\n");
                LoginOrSignUp();
                return;
            }
            else
            {
                Console.WriteLine("To create a new player enter username below:");
                string username = Console.ReadLine();
                if (username == "")
                {
                    Console.WriteLine("You did not enter a username\n");
                    LoginOrSignUp();
                    return;
                }
                if (IsUsernameTaken(username))
                {
                    LoginOrSignUp();
                    return;
                }
                else
                {
                    Console.WriteLine("Enter password below:");
                    string password = Console.ReadLine();
                    if (password == "")
                    {
                        Console.WriteLine("You did not enter a password\n");
                        LoginOrSignUp();
                        return;
                    }
                    else
                    {
                        Console.WriteLine();
                        Player player = new Player(username, password);
                        players.Add(player);
                        currentPlayer = player;
                        Console.WriteLine("Welcome to KOS Captain " + currentPlayer.GetName() + "\n");
                    }
                }
            }
        }

        public bool DoAccountsExist()
        {
            return players.Count > 0;
        }

        public bool IsUsernameTaken(String username)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (username.ToLower() == players[i].GetName().ToLower())
                {
                    Console.WriteLine("Username '" + username.ToLower() + "' is taken by another user\n");
                    return true;
                }
            }
            return false;
        }

        public void Welcome()
        {
            Console.WriteLine("/ * * * * * * * * * * * * * /\n Welcome to King Of The Sky! \n/ * * * * * * * * * * * * * /\n");
        }

        public void Logout()
        {
            Console.WriteLine("Captain " + currentPlayer.GetName() + " is signing off\n");
            currentPlayer = null;
            Welcome();
            LoginOrSignUp();
        }
    }

    class CombatManager
    {
        private short altitude;
        private Random rand;


        public CombatManager()
        {
            rand = new Random();
        }

        public void Battle(params Ship[] fighters)
        {
            for (int i = 0; i < fighters.Length; i++)
            {
                fighters[i].SetTempHealth(fighters[i].GetHealth());
            }
            if(fighters.Length < 2)
            {
                Console.WriteLine("You must enter at least two fighters to battle");
            }
        }

        public void FireCannon(Ship source, Ship target)
        {
            int speedDiff = source.GetSpeed() - target.GetSpeed();
            int hitChance = rand.Next(0, 100);
            if (speedDiff > 0 && hitChance <= source.GetCannon().GetAccuracy())
            {
                takeDamage(target, source.GetCannon().GetPower());
                Console.WriteLine("The " + source.GetName() + " has hit the " + target.GetName() + " with the " + source.GetCannon() + "\n");
            }
            else if (true)
            {
                source.GetCannon().GetAccuracy();
            }
        }

        public void FireTorpedo(Ship source, Ship target)
        {

        }

        public void FireBomb(Ship source, Ship target)
        {

        }

        public void Ram(Ship source, Ship target)
        {

        }

        public void takeDamage(Ship target, int damage)
        {
            target.SetTempHealth(target.GetTempHealth() - damage);
            if (target.GetTempHealth() <= 0)
            {
                Console.WriteLine("The " + target.GetName() + " has been destroyed");
                // Remove fighter from queue
            }
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
            string command2 = Console.ReadLine();
            if (command2[0].Equals('1') || command2[0].Equals('2') || command2[0].Equals('3') || command2[0].Equals('4') || command2[0].Equals('5'))
            {
                Console.WriteLine("The " + GetShips()[int.Parse((command2[0]).ToString()) - 1]);
                GetShips()[int.Parse((command2[0]).ToString()) - 1] = newShip;

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
    }

    class ShipFactory
    {
        public Cruiser CreateCrusier(string name)
        {
            Cruiser c = new Cruiser(name);
            Console.WriteLine("You have built a new Cruiser named: The " + c.GetName() + "\nStats:\nHealth: " + c.GetHealth() + "\nArmor: " + c.GetArmor() + "\nSpeed: " + c.GetSpeed() + "\n");
            return c;
        }

        public Glider CreateGlider(string name)
        {
            Glider g = new Glider(name);
            Console.WriteLine("You have built a new Glider named: The " + g.GetName() + "\nStats:\nHealth: " + g.GetHealth() + "\nArmor: " + g.GetArmor() + "\nSpeed: " + g.GetSpeed() + "\n");
            return g;
        }

        public Bomber CreateBomber(string name)
        {
            Bomber b = new Bomber(name);
            Console.WriteLine("You have built a new Bomber named: The " + b.GetName() + "\nStats:\nHealth: " + b.GetHealth() + "\nArmor: " + b.GetArmor() + "\nSpeed: " + b.GetSpeed() + "\n");
            return b;
        }
    }

    class Ship
    {
        private string name;
        private short level;
        private int health;
        private int tempHealth;
        private int armor;
        private int speed;
        private int build_points;
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

        public short GetLevel()
        {
            return this.level;
        }

        public void SetLevel(short lvl)
        {
            this.level = lvl;
        }

        public int GetHealth()
        {
            return this.health;
        }

        public void SetHealth(int health)
        {
            this.health = health;
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
            if (this.speed < 0)
                return 0;
            return this.speed;
        }

        public void SetSpeed(int speed)
        {
            this.speed = speed;
        }

        public int GetBuildPoints()
        {
            return this.build_points;
        }

        public void SetBuildPoints(int buildPoints)
        {
            this.build_points = build_points;
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
            Console.WriteLine("The " + this.name + " is level " + this.level + " and has " + this.health + " health, " + this.armor + " armor, and " + this.speed + " speed");
        }

        public void GetStats(int i)
        {
            Console.WriteLine((i+1) + ". The " + this.GetName() + " is level " + this.GetLevel() + " and has " + this.GetHealth() + " health, " + this.GetArmor() + " armor, and " + this.GetSpeed() + " speed");
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
                    this.speed += this.hull.GetWeight();
                }
                this.hull = hull;
                this.armor += hull.GetArmor();
                this.speed -= hull.GetWeight();
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
                    this.speed += this.cannon.GetWeight();
                }
                this.cannon = cannon;
                this.speed -= cannon.GetWeight();
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
                    this.speed += this.torpedo.GetWeight();
                }
                this.torpedo = torpedo;
                this.speed -= torpedo.GetWeight();
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
                    this.speed += this.bomb.GetWeight();
                }
                this.bomb = bomb;
                this.speed -= bomb.GetWeight();
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
            this.SetLevel(0);
            this.SetHealth(rand.Next(1000, 2000));
            this.SetArmor(rand.Next(10, 80));
            this.SetSpeed(rand.Next(100, 200));
            this.SetBuildPoints(0);
        }
    }

    class Bomber : Ship
    {
        public Bomber(string name)
        {
            Random rand = new Random();
            this.SetName(name);
            this.SetLevel(0);
            this.SetHealth(rand.Next(2500, 5000));
            this.SetArmor(rand.Next(100, 200));
            this.SetSpeed(rand.Next(10, 80));
            this.SetBuildPoints(0);
        }
    }

    class Cruiser : Ship
    {
        public Cruiser(string name)
        {
            Random rand = new Random();
            this.SetName(name);
            this.SetLevel(0);
            this.SetHealth(rand.Next(1500, 3000));
            this.SetArmor(rand.Next(50, 150));
            this.SetSpeed(rand.Next(50, 150));
            this.SetBuildPoints(0);
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
