﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Status { get; set; }

        /*public override string ToString()
        {
            return $"Id:{Id} Name:{Name} Email:{Email} Status:{Status}";
        }*/
    }
}
