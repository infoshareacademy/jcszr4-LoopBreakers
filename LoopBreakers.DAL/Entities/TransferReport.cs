using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using LoopBreakers.DAL.Enums;

namespace LoopBreakers.DAL.Entities
{
    [Table("TransferReport")]
    public class TransferReport : Entity
    {
        public Currency Currency { get; set; }
        public Decimal Amount { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
