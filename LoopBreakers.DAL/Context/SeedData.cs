using LoopBreakers.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LoopBreakers.DAL.Enums;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;

namespace LoopBreakers.DAL.Context
{
    public class SeedData
    {
        private static  List<ApplicationUser> _users = new List<ApplicationUser>();
        private const string UsersJsonFilePath = "DataSource/users.json";

        public static void SeedTransfer(ApplicationDbContext context)
        {
            var transfersDataNotExists = !context.Transfers.Any();
            if (transfersDataNotExists)
            {
                var transfers = new List<Transfer>
                {
                    new Transfer() { Amount = 213.530m, Iban = "PL52109024021938651953747779", Created = DateTime.Now.AddDays(-5),   FirstName ="Damian",   LastName = "Kowalski",   Currency = Currency.PLN, Reference = "JEDZENIE",        Type = TransferType.Payment},
                    new Transfer() { Amount = 414.43m,  Iban = "PL16109024022115541244392987", Created = DateTime.Now.AddDays(-10),  FirstName ="Maciej",   LastName = "Madejski",   Currency = Currency.PLN, Reference = "PIES",            Type = TransferType.Payment},
                    new Transfer() { Amount = 15.99m,   Iban = "PL98109024021184712588824539", Created = DateTime.Now.AddDays(-15),  FirstName ="Adam",     LastName = "Powarski",   Currency = Currency.PLN, Reference = "SAMOCHOD",        Type = TransferType.Payment},
                    new Transfer() { Amount = 266.22m,  Iban = "PL44109024026974239459632788", Created = DateTime.Now.AddDays(-88),  FirstName ="Dariusz",  LastName = "Nawojski",   Currency = Currency.PLN, Reference = "Przelew środków", Type = TransferType.Payment},
                    new Transfer() { Amount = 717.94m,  Iban = "PL03109024029617359544162388", Created = DateTime.Now.AddDays(-120), FirstName ="Damian",   LastName = "Wieniawski", Currency = Currency.PLN, Reference = "czynsz",          Type = TransferType.Payment},
                    new Transfer() { Amount = 2182.40m, Iban = "PL37109024021659358699489856", Created = DateTime.Now.AddDays(-150), FirstName ="Rafał" ,   LastName = "Szczerba",   Currency = Currency.PLN, Reference = "zakupy",          Type = TransferType.Payment}
                };
                context.Transfers.AddRange(transfers);
                context.SaveChanges();
            }
        }
        public static async void SeedClient(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRoles> roleManager) 
        {
            if (context.Users.Any())
            {
                return;
            }

            var admin = new ApplicationUser()
            {
                UserName = "admin@admin.eu",
                Email = "admin@admin.eu",
                FirstName = "Admin",
                IsActive = true,
                Company = "LoopBreakers",
                Iban = "PL00000000000000000000000000"
            };

            var result = await userManager.CreateAsync(admin, "superSecretAdminPassword123@");
            if (result.Succeeded)
            {
                await roleManager.CreateAsync(new ApplicationRoles() { Name = "Admin" });
                await roleManager.CreateAsync(new ApplicationRoles() { Name = "User" });
                await userManager.AddToRoleAsync(admin, "Admin");
            }

            //if (File.Exists(UsersJsonFilePath))
            //{
            //    string json = File.ReadAllText(UsersJsonFilePath).Replace("Id", "IdentityNumber");
            //    _users = JsonConvert.DeserializeObject<List<ApplicationUser>>(json);
            //    context.Users.AddRange(_users);
            //    context.SaveChanges();
            //}
        }
        public static string GenerateIban()
        {
            var random = new Random();
            return $"PL{random.Next(1000000, 9999999)}{random.Next(1000000, 9999999)}{random.Next(1000000, 9999999)}{random.Next(10000, 99999)}";
        }
    }
}





