using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Transaction
    {
        public int Source { get; set; }

        public string Destination { get; set; }

        public string Amount { get; set; }

        public string TransactionDate { get; set; }

    }
}
