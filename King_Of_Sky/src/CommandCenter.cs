using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingOfTheSky.src
{
    class CommandCenter
    {
        private PlayerManager playerManager;
        private ShipManager shipManager;
        private ShipFactory shipFactoryManager;
        private CombatManager combatManager;

        public CommandCenter()
        {
            playerManager = new PlayerManager();
            shipManager = new ShipManager();
            shipFactoryManager = new ShipFactory();
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
            catch (Exception)
            {
                Console.WriteLine("Invalid input was entered");
                return;
            }

            if (command[0].ToLower() == "p" || command[0].ToLower() == "player")
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

        public ShipFactory GetShipFactoryManager()
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
}
