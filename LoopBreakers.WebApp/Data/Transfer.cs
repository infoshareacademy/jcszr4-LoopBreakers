using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.Data
{
    [Table("Transfers")]
    public class Transfer
    {
        public int Id { get; set; }

        public string Iban { get; set; }

        /// <summary>
        /// Recipient's first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Recipient's last name
        /// </summary>
        public string LastName { get; set; }
        public string FromId { get; set; }
        public TransferType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Created { get; set; }

        /// <summary>
        /// Text of the transfer.
        /// </summary>
        public string Reference { get; set; }
        public Currency Currency { get; set; }
    }
}
