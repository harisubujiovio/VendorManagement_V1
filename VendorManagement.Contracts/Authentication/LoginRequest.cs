using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorManagement.Contracts.Authentication
{
    public record LoginRequest
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }

    public record LoginResponse 
    {
        public Guid UserId { get; set; }  
        
        public string firstName { get; set; }

        public string lastName { get; set; }

        public string token { get; set; }
    }
}
