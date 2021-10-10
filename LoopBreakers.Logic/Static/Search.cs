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
        public static void NameSearch(string searchNameValue)
        {
            var foundUsers = TemporaryCollections.Users.Where(Users => Users.LastName.ToLower()==searchNameValue.ToLower()); //.Contains(searchNameValue, StringComparison.InvariantCultureIgnoreCase));
            if (foundUsers.Any())
            {
                foreach (var user in foundUsers)
                {
                    Console.WriteLine($"{user.FirstName} {user.LastName}\r\nBalance: {user.Balance} {user.Currency}\r\nAddress: {user.Address}\r\nAge: {user.Age}\r\nCompany: {user.Company}\r\nE-mail: {user.Email}\r\nGender: {user.Gender}\r\nId: {user.Id}\r\nisActive?: {user.IsActive}\r\nPhone Number: {user.Phone}\r\nDate of Reg: {user.Registered}");
                }
            }
            else
            {
                Console.WriteLine("No one user has been found, try again");
            }
           
        }
    }
}
