using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoopBreakers.Logic.Data;

namespace LoopBreakers.Logic.Static
{
   public static class Search
    {
        public static List<User> NameSearch(string searchNameValue)
        {
            return TemporaryCollections.Users.Where(Users => Users.LastName.Contains(searchNameValue, StringComparison.InvariantCultureIgnoreCase)).ToList();     
        }
    }
}
