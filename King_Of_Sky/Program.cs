using System;
using System.Collections.Generic;

namespace King_Of_Sky
{
    class Program
    {
        static void Main(string[] args)
        { 
            CommandCenter commandCenter = new CommandCenter();
            commandCenter.GetPlayerManager().Welcome();
            commandCenter.GetPlayerManager().LoginOrSignUp();
            
            while (true)
            {
                commandCenter.EnterCommand();
            }
        }
    }

    class CommandCenter
    {
        private PlayerManager playerManager;
        private ShipManager shipManager;
        private ShipFactoryManager shipFactoryManager;
        private CombatManager combatManager;
        
        public CommandCenter()
        {
            playerManager = new PlayerManager();
            shipManager = new ShipManager();
            shipFactoryManager = new ShipFactoryManager();
            combatManager = new CombatManager();
        }

        public void EnterCommand()
        {
            Console.WriteLine("Available Commands:\n" +
                "<'player' or 'p'> - View list of KOS players or logout\n" +
                "<'ships' or 's'>  - View ships in your armada\n" +
                "<'build' or 'b'>  - Add ships to your armada\n" +
                "<'combat' or 'c'> - Battle other ships or train your own\n" +
                "<'quit' or 'q'>   - Close application\n\n" +
                "Enter Main Menu Command Below:");
            string[] command;
            try
            {
                command = Console.ReadLine().Split(' ');
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid input was entered");
                return;
            }

            if      (command[0].ToLower() == "p" || command[0].ToLower() == "player")
            {
                playerManager.EnterPlayerManagerCommand();
            }
            else if (command[0].ToLower() == "s" || command[0].ToLower() == "ship" || command[0].ToLower() == "ships")
            {
                shipManager.EnterShipManagerCommand(playerManager);
            }
            else if (command[0].ToLower() == "b" || command[0].ToLower() == "build")
            {
                shipFactoryManager.EnterBuildCommand(playerManager);
            }
            else if (command[0].ToLower() == "c" || command[0].ToLower() == "combat")
            {
                combatManager.EnterCombatManagerCommand(playerManager);
            }
            else if (command[0].ToLower() == "q" || command[0].ToLower() == "quit")
            {
                ExitApplication();
            }
            else
            {
                InvalidInput();
            }
            EnterCommand();
        }

        public PlayerManager GetPlayerManager()
        {
            return playerManager;
        }

        public ShipManager GetShipManager()
        {
            return shipManager;
        }

        public ShipFactoryManager GetShipFactoryManager()
        {
            return shipFactoryManager;
        }

        public CombatManager GetCombatManager()
        {
            return combatManager;
        }

        public void InvalidInput()
        {
            Console.WriteLine("The entered input did not match any of the available commands\n");
        }

        public void ExitApplication()
        {
            Environment.Exit(0);
        }
    }

    class PlayerManager
    {
        private List<Player> players;
        private Player currentPlayer;

        public PlayerManager()
        {
            players = new List<Player>();
        }

        public void EnterPlayerManagerCommand()
        {
            Console.WriteLine("Available Player Manager Commands:\n" +
                    "<'players' or 'p'> - Lists all KOS players\n" +
                    "<'logout' or 'l'>  - Logs the current player out and returns to welcome screen\n" +
                    "<'return' or 'r'>  - Returns to main menu\n\n" +
                    "Enter Player Management Command:");

            string[] command;
            try
            {
                command = Console.ReadLine().Split(' ');
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid input was entered");
                return;
            }

            if (command[0].ToLower() == "p" || command[0].ToLower() == "player")
            {
                Console.WriteLine("KOS Players Include:");
                for (int i = 0; i < GetPlayerList().Count; i++)
                {
                    Console.WriteLine("- Captain " + GetPlayerList()[i].GetName());
                }
                Console.WriteLine("\nYou are currently signed in as Captain " + GetCurrentPlayer().GetName() + "\n");
            }
            else if (command[0].ToLower() == "l" || command[0].ToLower() == "logout")
            {
                Logout();
                return;
            }
            else if (command[0].ToLower() == "r" || command[0].ToLower() == "return" || command[0].ToLower() == "")
            {
                return;
            }
            else
            {
                InvalidInput();
            }
            EnterPlayerManagerCommand();
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
            Console.WriteLine();
            if (doYouHaveAccount == "yes" || doYouHaveAccount == "y")
            {
                Console.WriteLine("Enter username below:");
                string username = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("Enter password below:");
                string password = Console.ReadLine();
                Console.WriteLine();
                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i].GetName() == username && players[i].GetPassword() == password)
                    {
                        currentPlayer = players[i];
                        Console.WriteLine("Welcome back captain " + players[i].GetName() + "\n");
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
                Console.WriteLine();
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
                    Console.WriteLine("Create a password by entering it below:");
                    string password = Console.ReadLine();
                    Console.WriteLine();
                    if (password == "")
                    {
                        Console.WriteLine("You did not enter a password\n");
                        LoginOrSignUp();
                        return;
                    }
                    else
                    {
                        Player player = new Player(username, password);
                        players.Add(player);
                        currentPlayer = player;
                        Console.WriteLine("Welcome to KOS Captain " + currentPlayer.GetName() + "\n");
                        return;
                    }
                }
            }
        }

        public void ListAllPlayers()
        {
            Console.WriteLine("KOS Players:");
            for (int i = 0; i < GetPlayerList().Count; i++)
            {
                Console.WriteLine((i+1) + ". Captain " + GetPlayerList()[i].GetName());
            }
            Console.WriteLine();
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
            Console.WriteLine("/ * * * * * * * * * * * * * /\n" +
                              "   Welcome to King Of Sky!   \n" +
                              "/ * * * * * * * * * * * * * /\n");
        }

        public void Logout()
        {
            Console.WriteLine("Captain " + currentPlayer.GetName() + " is signing off\n");
            currentPlayer = null;
            Welcome();
            LoginOrSignUp();
        }

        public void InvalidInput()
        {
            Console.WriteLine("The entered input did not match any of the available commands\n");
        }
    }

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
            catch (Exception e)
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
                    catch (NullReferenceException e)
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
                catch (Exception e)
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
            catch (Exception e)
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
                        catch (NullReferenceException e)
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
                    catch (Exception e)
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
                    catch (Exception e)
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
                    catch (Exception e)
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

    class ShipFactoryManager
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
            catch (Exception e)
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
                        catch (NullReferenceException e)
                        {
                            Console.WriteLine((i + 1) + ". ~Empty~");
                        }
                    }
                    Console.WriteLine();
                }
                else if(command[0].ToLower() == "return" || command[0].ToLower() == "r" || command[0].ToLower() == "")
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

    class CombatManager
    {
        private Random rand;

        public CombatManager()
        {
            rand = new Random();
        }

        public void EnterCombatManagerCommand(PlayerManager playerManager)
        {
            Console.WriteLine("Available Commands:\n" +
                "<'battle' or 'b'>  - Choose a player to battle or train your ships\n" +
                "<'players' or 'p'> - View other players to battle\n" +
                "<'return' or 'r'>  - Return to main menu\n\n" +
                "Enter Combat Manager command:");

            string[] command;
            try
            {
                command = Console.ReadLine().Split(' ');
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid input was entered");
                return;
            }

            if ((command[0].ToLower() == "battle" || command[0].ToLower() == "b") && command.Length == 1)
            {
                List<Battler> battlers = SelectBattlers(playerManager);

                if(battlers.Count < 2)
                {
                    Console.WriteLine("You must select at least two battlers");
                    return;
                }

                int startingPlayerIndex = rand.Next(0, battlers.Count);
                Console.WriteLine("Captain " + battlers[startingPlayerIndex].GetPlayer().GetName() +
                    " and The " + battlers[startingPlayerIndex].GetShip().GetName() + " will have the first move\n");

                while (battlers.Count > 1)
                {
                    EnterBattleCommand(battlers, startingPlayerIndex);
                }

                Console.WriteLine("Captain " + battlers[0].GetPlayer().GetName() + " and The " + battlers[0].GetShip().GetName() + " win!\n");
                battlers[0].GetShip().LevelUp();

                return;
            }
            else if (command[0].ToLower() == "players" || command[0].ToLower() == "p")
            {
                playerManager.ListAllPlayers();
            }
            else if (command[0].ToLower() == "return" || command[0].ToLower() == "r" || command[0].ToLower() == "")
            {
                return;
            }
            else
            {
                InvalidInput();
            }
            EnterCombatManagerCommand(playerManager);
        }

        public void EnterBattleCommand(List<Battler> battlers, int playerIndex)
        {
            Console.WriteLine("Available Battle Commands:\n" +
                    "<'fire' or 'f'> - Lists details about your ship's weapons" +
                    "<'fire' or 'f'> <'cannon' or 'c' or 'torpedo' or 't' or 'bomb' or 'b'> - Fires the current ships input weapon\n" +
                    "<'ram' or 'r'> - Attempts to ram the other ship\n" +
                    "<'concede' or 'c'> - Forfeit the match\n\n" +
                    "Enter Battle Command Below:");

            string[] command;
            try
            {
                command = Console.ReadLine().Split(' ');
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid input was entered");
                return;
            }

            if (command.Length == 1)
            {
                if (command[0].ToLower() == "fire" || command[0].ToLower() == "f")
                {
                    battlers[playerIndex].GetShip().GetEquiptmentStats();
                }
                if (command[0].ToLower() == "ram" || command[0].ToLower() == "r")
                {
                    Ram(battlers[playerIndex].GetShip(), ChooseTarget(battlers).GetShip());
                }
                if (command[0].ToLower() == "concede" || command[0].ToLower() == "c")
                {
                    battlers.Remove(battlers[playerIndex]);
                }
            }
        }

        public void Fire(Ship source, Weapon weapon, Ship target)
        {
            int speedDiff = source.GetSpeed() - target.GetSpeed(); // If + source is faster, if - target is faster
            int hitChance = rand.Next(0, 100); // Random num between 0 and 100
            int accuracy = weapon.GetAccuracy();

            if (speedDiff > target.GetSpeed())
            {
                accuracy /= 2;
            }
            else if (speedDiff > 0)
            {
                accuracy -= (speedDiff/10);
            }

            if (hitChance <= accuracy)
            {
                TakeDamage(target, weapon.GetPower());
                Console.WriteLine("The " + source.GetName() + " has hit the " + target.GetName() + " with the " + weapon.GetName() + "\n");
            }
            else
            {
                Console.WriteLine("The " + source.GetName() + " has missed the " + target.GetName() + " with the " + weapon.GetName() + "\n");
            }
        }

        public void Ram(Ship source, Ship target)
        {
            if(rand.Next(2) == 1)
            {
                TakeDamage(target, source.GetWeight());
            }
            else
            {
                Console.WriteLine("The " + source.GetName() + " has missed the " + target.GetName() + " with it's ram attack\n");
            }
        }

        public void TakeDamage(Ship target, int damage)
        {
            target.SetTempHealth(target.GetTempHealth() - damage);
            if (target.GetTempHealth() <= 0)
            {
                Console.WriteLine("The " + target.GetName() + " has been destroyed");
                // Remove fighter from queue
            }
        }

        public Ship ChoosePlayersShipForBattle(Player player, Player currentPlayer)
        {
            player.ListShips();
            if (player == currentPlayer)
            {
                Console.WriteLine("Choose one of your ships for battle:");
            }
            else
            {
                Console.WriteLine("Choose one of Captain " + player.GetName() + "'s ships for battle:");
            }

            string chooseShipCommand;
            try
            {
                chooseShipCommand = Console.ReadLine();
                Console.WriteLine();
                if (player.GetShips()[int.Parse(chooseShipCommand)-1] == null)
                {
                    Console.WriteLine("This ship does not exist\n");
                    return null;
                }
                Console.WriteLine("The " + player.GetShips()[int.Parse(chooseShipCommand) - 1].GetName() + " will fight in the next battle\n");
                return player.GetShips()[int.Parse(chooseShipCommand)-1];
            }
            catch (Exception e)
            {
                Console.WriteLine("The input you entered was not a valid ship number\n");
                return null;
            }
        }

        public Player ChooseOpponentToBattle(PlayerManager playerManager)
        {
            playerManager.ListAllPlayers();
            Console.WriteLine("Choose a player to battle by entering their number below:");

            string choosePlayerCommand;
            try
            {
                choosePlayerCommand = Console.ReadLine();
                Console.WriteLine("\nCaptain " + playerManager.GetPlayerList()[int.Parse(choosePlayerCommand)-1].GetName() + " will be your opponent\n");
                return playerManager.GetPlayerList()[int.Parse(choosePlayerCommand)-1];
            }
            catch (Exception e)
            {
                Console.WriteLine("The input you entered did not match a player number in the database\n");
                return null;
            }
        }

        public Battler ChooseTarget(List<Battler> battlers)
        {
            Console.WriteLine("Available Targets:");
            for (int i = 0; i < battlers.Count; i++)
            {
                Console.WriteLine((i+1) + ". Captain " + battlers[i].GetPlayer().GetName() + " and The " + battlers[i].GetShip().GetName());
            }
            Console.WriteLine("\nChoose Target:");
            if (int.TryParse(Console.ReadLine(), out int targetIndex))
            {
                Console.WriteLine("\nCaptain " + battlers[targetIndex-1].GetPlayer().GetName() + " and The " + battlers[targetIndex-1].GetShip().GetName() + " selected as target\n");
                return battlers[targetIndex - 1];
            }
            else
            {
                InvalidInput();
                return ChooseTarget(battlers);
            }
        }

        public void SetTempHealthToFull(List<Battler> battlers)
        {
            for (int i = 0; i < battlers.Count; i++)
            {
                battlers[i].GetShip().SetTempHealth(battlers[i].GetShip().GetTotalHealth());
            }
        }

        public List<Battler> SelectBattlers(PlayerManager playerManager)
        {
            List<Battler> battlers = new List<Battler>();

            Ship playersShip = ChoosePlayersShipForBattle(playerManager.GetCurrentPlayer(), playerManager.GetCurrentPlayer());
            if (playersShip == null)
            {
                Console.WriteLine("Ship does not exist\n");
                EnterCombatManagerCommand(playerManager);
                return null;
            }

            battlers.Add(new Battler(playerManager.GetCurrentPlayer(), playersShip));

            Player opponent = ChooseOpponentToBattle(playerManager);
            if (opponent == null)
            {
                Console.WriteLine("Player does not exist\n");
                EnterCombatManagerCommand(playerManager);
                return null;
            }

            Ship opposingShip = ChoosePlayersShipForBattle(opponent, playerManager.GetCurrentPlayer());
            if (opposingShip == null)
            {
                Console.WriteLine("Ship does not exist\n");
                EnterCombatManagerCommand(playerManager);
                return null;
            }

            battlers.Add(new Battler(opponent, opposingShip));

            SetTempHealthToFull(battlers);

            for (int i = 0; i < battlers.Count; i++)
            {
                Console.WriteLine("Captain " + battlers[i].GetPlayer().GetName() + " and The " + battlers[i].GetShip().GetName());
                if (i != battlers.Count - 1)
                {
                    Console.WriteLine("< ***** Vs. ***** >");
                }
            }
            Console.WriteLine();

            return battlers;
        }

        public void InvalidInput()
        {
            Console.WriteLine("The entered input did not match any of the available commands\n");
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
                    Console.WriteLine((i+1) + ". ~Empty~");
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

    class Battler
    {
        Player player;
        Ship ship;

        public Battler(Player player, Ship ship)
        {
            this.player = player;
            this.ship = ship;
        }

        public Player GetPlayer()
        {
            return player;
        }

        public void SetPlayer(Player player)
        {
            this.player = player;
        }

        public Ship GetShip()
        {
            return ship;
        }

        public void SetShip(Ship ship)
        {
            this.ship = ship;
        }
    }

    class Ship
    {
        private string name;
        private short level;
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

        public short GetLevel()
        {
            return this.level;
        }

        public void SetLevel(short lvl)
        {
            this.level = lvl;
        }

        public void LevelUp()
        {
            short lvl = GetLevel();
            lvl = lvl++;
            SetLevel(lvl);
            Console.WriteLine("Your ship leveled up to " + GetLevel() + "\n");
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
            Console.WriteLine((i+1) + ". The " + this.GetName() + " is level " + this.GetLevel() + " and has " + this.GetTotalHealth() + " health, " + this.GetArmor() + " armor, and " + this.GetSpeed() + " speed");
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
            this.SetLevel(0);
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
            this.SetLevel(0);
            this.SetTotalHealth(rand.Next(2500, 5000));
            this.SetArmor(rand.Next(100, 200));
            this.SetSpeed(rand.Next(10, 80));
            this.SetWeight(0);
        }
    }

    class Cruiser : Ship
    {
        public Cruiser(string name)
        {
            Random rand = new Random();
            this.SetName(name);
            this.SetLevel(0);
            this.SetTotalHealth(rand.Next(1500, 3000));
            this.SetArmor(rand.Next(50, 150));
            this.SetSpeed(rand.Next(50, 150));
            this.SetWeight(0);
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
