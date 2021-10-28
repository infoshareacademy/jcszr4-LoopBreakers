using LoopBreakers.Logic.Data;
using LoopBreakers.Logic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopBreakers.ConsoleApp
{
    public static class Client
    {
        public static User AddNew()
        {
            Console.Clear();
            Console.WriteLine("Add new Client\n");
            Console.Write("First name: ");
            var firstName = Program.GetTextWithoutNumbers(2, 20);
            Console.Write("Last name: ");
            var lastName = Program.GetTextWithoutNumbers(2, 20);
            Console.Write("Age: ");
            var age = Program.GetNumber(18, 120);
            Console.Write("E-mail: ");
            var email = Program.GetEmail(5, 50);
            Console.Write("Company name: ");
            var company = Program.GetText(1, 100);
            Console.Write("Phone: ");
            var phone = Program.GetText(9, 20);
            Console.Write("Address: ");
            var address = Program.GetText(5, 100);
            Console.WriteLine("Select currency:");
            var currency = Program.GetEnum<Currency>().stringFromEnum.ToUpper();
            Console.WriteLine("Select gender:");
            Gender gender = (Gender)Program.GetEnum<Gender>().chosenOption;
            Console.WriteLine("Is Client active?:");
            var isActive = Program.GetBool();
            Console.Write("Balance: ");
            var balance = Program.GetDecimal();

            var client = new User();
            client.FirstName = firstName;
            client.LastName = lastName;
            client.Age = age;
            client.Email = email;
            client.Company = company;
            client.Phone = phone;
            client.Address = address;
            client.Currency = currency;
            client.Gender = gender;
            client.IsActive = isActive;
            client.Balance = balance;
            client.Id = Guid.NewGuid().ToString();
            client.Registered = DateTime.Now;
            return client;
        }

        public static void Edit(User client)
        {
            //TO DO
        }
    }
}