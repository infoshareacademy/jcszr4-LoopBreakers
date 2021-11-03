﻿using System;
using LoopBreakers.Logic.Enums;

namespace LoopBreakers.Logic.Data
{
    public class User
    {
        public string Id { get; set; }
        public bool IsActive { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime Registered { get; set; }
        public string Iban { get; } = GenerateIban();

        public void AddUserToFovouriteUsersList()
        {
        }
        private static string GenerateIban()
        {
            var random = new Random();
            return $"PL{random.Next(1000000, 9999999)}{random.Next(1000000, 9999999)}{random.Next(1000000, 9999999)}{random.Next(10000, 99999)}";
        }
    }
}