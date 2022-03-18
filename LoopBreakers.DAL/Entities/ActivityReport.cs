using LoopBreakers.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LoopBreakers.DAL.Entities
{
    [Table("ActivityReport")]
    public class ActivityReport : Entity
    {
        public string Description { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ActivityEvents ActivityType { get; set; }
    }
}
