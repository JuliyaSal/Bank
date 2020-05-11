using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Transaction
    {
        public string Source { get; set; }

        public string Destination { get; set; }

        public int Amount { get; set; }

        public DateTime TransactionDate { get; set; }

    }
}
