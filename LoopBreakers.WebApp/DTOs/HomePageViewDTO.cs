using LoopBreakers.DAL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LoopBreakers.WebApp.DTOs
{
    public class HomePageViewDTO
    {
        public string AccountNumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public IEnumerable<TransferDTO> TransfersHistory { get; set; }
        public decimal Balance { get; set; }
        public Currency Currency { get; set; }

    }
}
