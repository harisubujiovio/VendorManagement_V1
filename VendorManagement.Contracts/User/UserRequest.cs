﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorManagement.Contracts
{
    public class UserRequest
    {
        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public string MobileNumber { get; set; }

        public string Address { get; set; }

        public string role { get; set; }
    }
}
