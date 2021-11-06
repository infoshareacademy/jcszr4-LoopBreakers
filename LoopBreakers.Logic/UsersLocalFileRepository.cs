using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LoopBreakers.Logic.Data;
using Newtonsoft.Json;

namespace LoopBreakers.Logic
{
    internal interface IUsersRepository
    {
        List<User> GetUsers { get; }
        void AddUser(User user);
        void EditUser(User user, int userIndex);
        List<User> GetUsersWithSurnameMatchingFilter(string filter);
        List<User> GetUsersWithFirstNameMatchingFilter(string filter);
    }

    public class UsersLocalFileRepository : IUsersRepository
    {
        private readonly List<Transfer> _transfers = new List<Transfer>
        {
            new Transfer() { Amount = 213, Iban = "PL1234567890123", Created = DateTime.Now.AddDays(-5), LastName = "Szczerba"},
            new Transfer() { Amount = 214, Iban = "PL098765432112345678904", Created = DateTime.Now.AddDays(-10), LastName = "Szczerba"},
            new Transfer() { Amount = 215, Iban = "PL53465464654646464667876834", Created = DateTime.Now.AddDays(-15), LastName = "Szczerba"},
            new Transfer() { Amount = 216, Iban = "PL983752365436386545457555", Created = DateTime.Now.AddDays(-88), LastName = "Szczerba"},
            new Transfer() { Amount = 217, Iban = "PL02857934785397468527454666", Created = DateTime.Now.AddDays(-120), LastName = "Szczerba"},
            new Transfer() { Amount = 218, Iban = "PL73478346342564365345332", Created = DateTime.Now.AddDays(-150), LastName = "Szczerba"},
        };

        private readonly List<User> _users = new List<User>();

        private const string UsersJsonFilePath = "DataSource/users.json";

        private List<RecipientStructure> _recipientList = new List<RecipientStructure>();

        public List<Transfer> GetTransfersForUserByIban(string userIban)
        {
            return this._transfers.Where(x => x.Iban == userIban).ToList();
        }

        public List<Transfer> GetTransfersForUserBySurname(string userSurname)
        {
            return this._transfers.Where(x => x.LastName.ToLower() == userSurname.ToLower()).ToList();
        }
        public List<Transfer> GetTransfersByDate(List<Transfer> transferList, DateTime searchFromDate)
        {
            return this._transfers.Where(x => x.Created > searchFromDate).ToList();
        }
        public List<Transfer> SortTransfersByDate(DateTime startDate, DateTime endDate)
        {
            return this._transfers.Where(x=>x.Created >= startDate && x.Created <= endDate).OrderBy(x => x.Created).ToList();
        }
        public UsersLocalFileRepository()
        {
            if (File.Exists(UsersJsonFilePath))
            {
                string json = File.ReadAllText(UsersJsonFilePath);
                _users = JsonConvert.DeserializeObject<List<User>>(json);
            }
        }

        public List<User> GetUsers 
        {
            get { return _users; }
        }

        public List<RecipientStructure> GetRecipient
        {
            get { return _recipientList; }
        }

        public List<Transfer> GetTransfers
        {
            get { return _transfers; }
        }

        public void AddTransfer(Transfer transfer)
        {
            _transfers.Add(transfer);
        }

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public void EditUser(User user, int userIndex)
        {
            _users[userIndex] = user;
        }

        public void AddRecipient(RecipientStructure recipient)
        {
            _recipientList.Add(recipient);
        }

        public void EditRecipient(int chosenRecipient, string newFirstName, string newLastName, string newAddress, string newIban)
        {
            chosenRecipient--;
            _recipientList[chosenRecipient].FirstName = newFirstName;
            _recipientList[chosenRecipient].LastName = newLastName;
            _recipientList[chosenRecipient].Address = newAddress;
            _recipientList[chosenRecipient].Iban = newIban;
        }

        public void RemoveRecipient(int chosenRecipient)
        {
            _recipientList.RemoveAt(chosenRecipient-1);
        }

        public List<User> GetUsersWithSurnameMatchingFilter(string filter)
        {
            return _users.Where(user => user.LastName.Contains(filter, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<User> GetUsersWithFirstNameMatchingFilter(string filter)
        {
            return _users.Where(user => user.FirstName.Contains(filter, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public string GetGuidOfRecipient(int choosenRecipient)
        {
            return _recipientList[choosenRecipient].Guid.ToString();
        }
    }
}