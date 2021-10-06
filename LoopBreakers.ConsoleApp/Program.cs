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
            TemporaryCollections.Initialize();

            var findName = "Wendi";
            foreach (var user in TemporaryCollections.Users
                .Where(user => user.FirstName == findName))
            {
                Console.WriteLine(user.FirstName+ user.LastName);
            }

            Console.ReadKey();
        }
    }
}
