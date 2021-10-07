using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoopBreakers.Logic.Data;
using System.IO;
using Newtonsoft.Json;

namespace LoopBreakers.Logic.Static
{
    public static class TemporaryCollections
    {
        private static string _usersJsonFilePath = "DataSource/users.json";

        public static List<User> Users = new List<User>();

        public static void InitializeCollections()
        {
            if (File.Exists(_usersJsonFilePath))
            {
                string json = File.ReadAllText(_usersJsonFilePath);
                Users = JsonConvert.DeserializeObject<List<User>>(json);
                
            }
        }
    }



}
