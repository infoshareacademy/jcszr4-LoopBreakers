﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using LoopBreakers.Logic.Data;
using LoopBreakers.Logic.Static;
using Newtonsoft.Json;

namespace LoopBreakers.ConsoleApp
{
    internal interface IUsersRepository
    {
        List<User> GetUsers { get; }
        List<User> GetFavoriteUsers { get; }

        void AddUser(User user);
        void AddFavoriteUser(User favoriteUser);
        List<User> GetUsersWithSurnameMatchingFilter(string filter);
        List<User> GetUsersWithFirstNameMatchingFilter(string filter);
    }

    class UsersLocalFileRepository : IUsersRepository
    {
        private readonly List<User> _users = new List<User>();

        private const string UsersJsonFilePath = "DataSource/users.json";

        private readonly List<User> _favoriteUsers = new List<User>();

        public UsersLocalFileRepository()
        {
            if (File.Exists(UsersJsonFilePath))
            {
                string json = File.ReadAllText(UsersJsonFilePath);
                _users = JsonConvert.DeserializeObject<List<User>>(json);

            }
        }

        public List<User> GetUsers {
            get { return _users; }
        }

        public List<User> GetFavoriteUsers
        {
            get { return _favoriteUsers; }
        }

        public void AddUser(User user)
        {
            _users.Add(user);
        }

        public void AddFavoriteUser(User favoriteUser)
        {
            _favoriteUsers.Add(favoriteUser);
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