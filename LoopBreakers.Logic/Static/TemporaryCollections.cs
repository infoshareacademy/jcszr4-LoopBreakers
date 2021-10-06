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
        private static string _usersJsonFilePath = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.Parent.FullName + @"\users.json";
        private static IList<User> _users = new List<User>();
        public static IList<User> Users
        {
            get
            {
                return _users;
            }
        }

        public static void Initialize()
        {
            _users = ReadJsonFile<User>(_usersJsonFilePath);
        }
        private static IList<T> ReadJsonFile<T>(string fileName)
        {
            IList<T> result = new List<T>();

            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                result = JsonConvert.DeserializeObject<IList<T>>(json);
            }
            return result;
        }
        
    }



}
