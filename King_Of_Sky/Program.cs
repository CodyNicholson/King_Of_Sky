using KingOfTheSky.src;
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
}
