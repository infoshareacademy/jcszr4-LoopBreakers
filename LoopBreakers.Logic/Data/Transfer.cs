using System;
using LoopBreakers.Logic.Enums;

namespace LoopBreakers.Logic.Data
{
    public class Transfer
    {
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