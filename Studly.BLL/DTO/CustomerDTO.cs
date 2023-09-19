﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Studly.BLL.DTO
{
    public class CustomerDTO
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime RegistrationDate { get; set; }

    }
}