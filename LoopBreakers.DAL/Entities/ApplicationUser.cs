using LoopBreakers.DAL.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LoopBreakers.DAL.Entities
{
    [Table("Users")]
    public class ApplicationUser : IdentityUser<int>, IEntity
    {
        public DateTime Created { get; set; }
        public string IdentityNumber { get; set; }
        public bool IsActive { get; set; }
        public decimal Balance { get; set; }
        public string Currency { get; set; }
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public string Company { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime Registered { get; set; }
        public string Iban { get; set; }
    }
}
