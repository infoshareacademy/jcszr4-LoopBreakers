using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.Data
{
    public class SeedData
    {
        public static void Seed(ApplicationDbContext context)
        {
            var transfersDataNotExists = !context.Transfers.Any();
            if (transfersDataNotExists)
            {
                var transfers = new List<Transfer>
                {
                    new Transfer() { Amount = 213.530m, Iban = "PL52109024021938651953747779", Created = DateTime.Now.AddDays(-5),   FirstName ="Damian",   LastName = "Kowalski",   Currency = Logic.Enums.Currency.PLN, Reference = "JEDZENIE",        Type = Logic.Enums.TransferType.Payment},
                    new Transfer() { Amount = 414.43m,  Iban = "PL16109024022115541244392987", Created = DateTime.Now.AddDays(-10),  FirstName ="Maciej",   LastName = "Madejski",   Currency = Logic.Enums.Currency.PLN, Reference = "PIES",            Type = Logic.Enums.TransferType.Payment},
                    new Transfer() { Amount = 15.99m,   Iban = "PL98109024021184712588824539", Created = DateTime.Now.AddDays(-15),  FirstName ="Adam",     LastName = "Powarski",   Currency = Logic.Enums.Currency.PLN, Reference = "SAMOCHOD",        Type = Logic.Enums.TransferType.Payment},
                    new Transfer() { Amount = 266.22m,  Iban = "PL44109024026974239459632788", Created = DateTime.Now.AddDays(-88),  FirstName ="Dariusz",  LastName = "Nawojski",   Currency = Logic.Enums.Currency.PLN, Reference = "Przelew środków", Type = Logic.Enums.TransferType.Payment},
                    new Transfer() { Amount = 717.94m,  Iban = "PL03109024029617359544162388", Created = DateTime.Now.AddDays(-120), FirstName ="Damian",   LastName = "Wieniawski", Currency = Logic.Enums.Currency.PLN, Reference = "czynsz",          Type = Logic.Enums.TransferType.Payment},
                    new Transfer() { Amount = 2182.40m, Iban = "PL37109024021659358699489856", Created = DateTime.Now.AddDays(-150), FirstName ="Rafał" ,   LastName = "Szczerba",   Currency = Logic.Enums.Currency.PLN, Reference = "zakupy",          Type = Logic.Enums.TransferType.Payment},
                };
                context.Transfers.AddRange(transfers);
                context.SaveChanges();
            }

        }
    }
}
