using System;
using System.Threading.Tasks;
using LoopBreakers.Logic.Data;
using System.Collections;
using System.Collections.Generic;
using LoopBreakers.Logic.Static;
using System.Linq;

namespace LoopBreakers.ConsoleApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            TemporaryCollections.InitializeCollections();

            //Option 1 - SearchByName
            int chosenOption = 1;   //should be comment
            if (chosenOption == 1)
            {
                
                Console.WriteLine("Introduce the surname of user which you wish to find");
                var entryName1 = Console.ReadLine();
                if (TemporaryCollections.Users.Any())
                {
                    Search.NameSearch(entryName1);
                }
                else
                {
                    Console.WriteLine("No users in the scope");
                }

            }
        }
    }
}
