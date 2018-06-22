using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KingOfTheSky.src
{
    class CombatManager
    {
        private Random rand;
        private List<Battler> battlers;
        private int playerIndex;

        public CombatManager()
        {
            rand = new Random();
            battlers = new List<Battler>();
            playerIndex = 0;
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
            catch (Exception)
            {
                Console.WriteLine("Invalid input was entered");
                return;
            }

            if ((command[0].ToLower() == "battle" || command[0].ToLower() == "b") && command.Length == 1)
            {
                battlers = new List<Battler>();
                battlers = SelectBattlers(playerManager);

                if (battlers != null)
                {
                    if (battlers.Count < 2)
                    {
                        Console.WriteLine("You must select at least two battlers");
                        return;
                    }

                    playerIndex = rand.Next(0, battlers.Count);
                    Console.WriteLine("Captain " + battlers[playerIndex].GetPlayer().GetName() +
                        " and The " + battlers[playerIndex].GetShip().GetName() + " will have the first move\n");

                    // Start battle loop
                    EnterBattleCommand();

                    Console.WriteLine("Captain " + battlers[0].GetPlayer().GetName() + " and The " + battlers[0].GetShip().GetName() + " win!\n");
                    battlers[0].GetShip().LevelUp();
                }

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

        public void EnterBattleCommand()
        {
            Console.WriteLine("What should The " + battlers[playerIndex].GetShip().GetName() + " do Captain " + battlers[playerIndex].GetPlayer().GetName() + ":\n" +
                    "<'fire' or 'f'> - Lists details about your ship's weapons" +
                    "<'fire' or 'f'> <'cannon' or 'c' or 'torpedo' or 't' or 'bomb' or 'b'> - Fires the current ships input weapon\n" +
                    "<'ram' or 'r'> - Attempts to ram the other ship\n" +
                    "<'concede' or 'c'> - Forfeit the match\n\n" +
                    "Enter Battle Command Below:");

            bool incrementIndex = true;
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

            if (command.Length == 1)
            {
                if (command[0].ToLower() == "fire" || command[0].ToLower() == "f")
                {
                    battlers[playerIndex].GetShip().GetEquiptmentStats();
                }
                if (command[0].ToLower() == "ram" || command[0].ToLower() == "r")
                {
                    Ram(playerIndex, ChooseTarget());
                }
                if (command[0].ToLower() == "concede" || command[0].ToLower() == "c")
                {
                    battlers.Remove(battlers[playerIndex]);
                }
            }
            if (command.Length == 2 && command[0].ToLower() == "f" || command[0].ToLower() == "fire")
            {
                if (command[1].ToLower() == "cannon" || command[1].ToLower() == "c")
                {
                    if (battlers[playerIndex].GetShip().GetCannon() == null)
                    {
                        Console.WriteLine("The ship does not have a cannon equipped\n");
                        incrementIndex = false;
                    }
                    else
                    {
                        Fire(battlers[playerIndex].GetShip(), battlers[playerIndex].GetShip().GetCannon(), ChooseTarget());
                    }
                }
                if (command[1].ToLower() == "torpedo" || command[1].ToLower() == "t")
                {
                    if (battlers[playerIndex].GetShip().GetTorpedo() == null)
                    {
                        Console.WriteLine("The ship does not have a torpedo equipped\n");
                        incrementIndex = false;
                    }
                    else
                    {
                        Fire(battlers[playerIndex].GetShip(), battlers[playerIndex].GetShip().GetTorpedo(), ChooseTarget());
                    }
                }
                if (command[1].ToLower() == "bomb" || command[1].ToLower() == "b")
                {
                    if (battlers[playerIndex].GetShip().GetBomb() == null)
                    {
                        Console.WriteLine("The ship does not have a bomb equipped\n");
                        incrementIndex = false;
                    }
                    else
                    {
                        Fire(battlers[playerIndex].GetShip(), battlers[playerIndex].GetShip().GetBomb(), ChooseTarget());
                    }
                }
            }

            // Index
            if (battlers.Count - 1 == playerIndex && battlers.Count > 1)
            {
                playerIndex = 0;
            }
            else if (!incrementIndex)
            {
                // Dont increment
            }
            else
            {
                playerIndex++;
            }

            if (battlers.Count > 1)
            {
                EnterBattleCommand();
            }
            else
            {
                return;
            }
        }

        public void Fire(Ship source, Weapon weapon, int targetIndex)
        {
            int speedDiff = source.GetSpeed() - battlers[targetIndex].GetShip().GetSpeed(); // If + source is faster, if - target is faster
            int hitChance = rand.Next(0, 100); // Random num between 0 and 100
            int accuracy = weapon.GetAccuracy();

            if (source.GetSpeed() < battlers[targetIndex].GetShip().GetSpeed())
            {
                accuracy = Convert.ToInt32(accuracy / 1.5);
            }
            else if (speedDiff > battlers[targetIndex].GetShip().GetSpeed())
            {
                accuracy -= (speedDiff / 10);
            }

            if (hitChance <= accuracy)
            {
                Console.WriteLine("The " + source.GetName() + " has hit the " + battlers[targetIndex].GetShip().GetName() + " with the " + weapon.GetName() + ".");
                TakeDamage(targetIndex, weapon.GetPower());
            }
            else
            {
                Console.WriteLine("The " + source.GetName() + " has missed the " + battlers[targetIndex].GetShip().GetName() + " with the " + weapon.GetName() + "\n");
            }
        }

        public void Ram(int sourceIndex, int targetIndex)
        {
            if (rand.Next(2) == 1)
            {
                Console.WriteLine("The " + battlers[sourceIndex].GetShip().GetName() + " ramed The " + battlers[targetIndex].GetShip().GetName() + " and did damage");
                TakeDamage(targetIndex, battlers[sourceIndex].GetShip().GetWeight());
                Console.WriteLine("The " + battlers[sourceIndex].GetShip().GetName() + " sustained damage when it rammed The " + battlers[targetIndex].GetShip().GetName());
                TakeDamage(sourceIndex, battlers[targetIndex].GetShip().GetWeight());
            }
            else
            {
                Console.WriteLine("The " + battlers[sourceIndex].GetShip().GetName() + " has missed the " + battlers[targetIndex].GetShip().GetName() + " with it's ram attack\n");
            }
        }

        public void TakeDamage(int targetIndex, int damage)
        {
            if (battlers[targetIndex].GetShip().GetTempHealth() - damage <= 0)
            {
                Console.WriteLine("The " + battlers[targetIndex].GetShip().GetName() + " has been destroyed.\n");
                battlers.Remove(battlers[targetIndex]);
            }
            else
            {
                battlers[targetIndex].GetShip().SetTempHealth(battlers[targetIndex].GetShip().GetTempHealth() - damage);
                Console.WriteLine("The " + battlers[targetIndex].GetShip().GetName() + " had " + (battlers[targetIndex].GetShip().GetTempHealth() + damage) + " and now has " + battlers[targetIndex].GetShip().GetTempHealth() + " remaining.\n");
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
                if (player.GetShips()[int.Parse(chooseShipCommand) - 1] == null)
                {
                    Console.WriteLine("This ship does not exist\n");
                    return null;
                }
                Console.WriteLine("The " + player.GetShips()[int.Parse(chooseShipCommand) - 1].GetName() + " will fight in the next battle\n");
                return player.GetShips()[int.Parse(chooseShipCommand) - 1];
            }
            catch (Exception)
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
                Console.WriteLine("\nCaptain " + playerManager.GetPlayerList()[int.Parse(choosePlayerCommand) - 1].GetName() + " will be your opponent\n");
                return playerManager.GetPlayerList()[int.Parse(choosePlayerCommand) - 1];
            }
            catch (Exception)
            {
                Console.WriteLine("The input you entered did not match a player number in the database\n");
                return null;
            }
        }

        public int ChooseTarget()
        {
            if (battlers.Count == 2)
            {
                if (playerIndex == 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            Console.WriteLine("Available Targets:");
            for (int i = 0; i < battlers.Count; i++)
            {
                Console.WriteLine((i + 1) + ". Captain " + battlers[i].GetPlayer().GetName() + " and The " + battlers[i].GetShip().GetName());
            }
            Console.WriteLine("\nChoose Target:");
            if (int.TryParse(Console.ReadLine(), out int targetIndex))
            {
                Console.WriteLine("\nCaptain " + battlers[targetIndex - 1].GetPlayer().GetName() + " and The " + battlers[targetIndex - 1].GetShip().GetName() + " selected as target");
                return (targetIndex - 1);
            }
            else
            {
                InvalidInput();
                return ChooseTarget();
            }
        }

        public void SetTempHealthToFull()
        {
            for (int i = 0; i < battlers.Count; i++)
            {
                battlers[i].GetShip().SetTempHealth(battlers[i].GetShip().GetTotalHealth());
            }
        }

        public List<Battler> SelectBattlers(PlayerManager playerManager)
        {
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

            SetTempHealthToFull();

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
}
