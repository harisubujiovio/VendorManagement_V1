using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorManagement.Contracts.Base;

namespace VendorManagement.Contracts
{
    public record UserResponseRoot
    {
        public IEnumerable<UserResponse> responses { get; set; }

        public int totalRows { get; set; }
    }
    public record UserResponse : AuditTrialResponse
    {
        public string firstName { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string MobileNumber { get; set; }

        public string Address { get; set; }

        public string role { get; set; }

        public UserResponse(Guid Id,string firstname, string lastname, string email, string mobilenumber,
            string address, string role, string createdBy, DateTime createdDate, string lastModifiedBy,
            DateTime? lastModifiedDate)
        {
            this.Id = Id;
            this.firstName = firstname;
            this.lastName = lastname;
            this.email = email;
            this.MobileNumber= mobilenumber;
            this.Address= address;
            this.role = role;
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate;
            this.LastModifiedBy = lastModifiedBy;
            this.LastModifiedDate = lastModifiedDate;
        }
    }
}
