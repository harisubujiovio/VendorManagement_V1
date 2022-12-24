using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorManagement.Contracts.Authentication
{
    public record RegisterResponse 
    {
        public Guid UserId { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string mobileNumber { get; set; }

        public string address { get; set; }

        public string token { get; set; }   

    }
    public record RegisterRequest
    {
        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string password { get; set; }

        public string mobileNumber { get; set; }

        public string address { get; set; }

        public string roleid { get; set; }
    }

}
