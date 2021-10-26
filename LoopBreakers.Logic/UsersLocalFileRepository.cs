﻿using System;
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
        List<User> GetUsersWithSurnameMatchingFilter(string filter);
        List<User> GetUsersWithFirstNameMatchingFilter(string filter);
    }

    public class UsersLocalFileRepository : IUsersRepository
    {
        private List<Transfer> DummyTransfers = new List<Transfer>
        {
            new Transfer() {Amount = 213, Iban = "123"},
            new Transfer() {Amount = 213, Iban = "123"},
            new Transfer() {Amount = 213, Iban = "534"},
            new Transfer() {Amount = 213},
            new Transfer() {Amount = 213},
            new Transfer() {Amount = 213},
        };
        private readonly List<User> _users = new List<User>();

        private const string UsersJsonFilePath = "DataSource/users.json";

        private List<Recipient> _recipientList = new List<Recipient>();

        public List<Transfer> SearchTransfersForUser(string userIban)
        {
            return this.DummyTransfers.Where(x => x.Iban == userIban).ToList();
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

        public List<Recipient> GetRecipient
        {
            get { return _recipientList; }
        }

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public void AddRecipient(Recipient recipient)
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



    }

}
