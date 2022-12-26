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
        public string token { get; set; }   
    }
    public record UserDetail 
    {
        public Guid userid { get; set; }

        public string displayName { get; set; }

        public Guid partnerid { get; set; }

        public Guid roleid { get; set; }    

        public UserDetail(Guid guid, string displayName,Guid partnerid, Guid roleid)
        {
            this.userid = guid;
            this.displayName= displayName;
            this.partnerid = partnerid;
            this.roleid = roleid;
        }
    }
}
