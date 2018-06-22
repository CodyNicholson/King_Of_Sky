using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingOfTheSky.src
{
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
            catch (Exception)
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
                Console.WriteLine((i + 1) + ". Captain " + GetPlayerList()[i].GetName());
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
}
