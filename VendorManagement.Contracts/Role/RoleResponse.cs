using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorManagement.Contracts.Base;

namespace VendorManagement.Contracts
{
    public record RoleResponseRoot
    {
        public IEnumerable<RoleResponse> responses { get; set; }

        public int totalRows { get; set; }
    }
    public record RoleResponse : AuditTrialResponse
    {
        public string Name { get; set; }    
        public string Description { get; }

        public RoleResponse(Guid Id,string name, string description, string createdBy, DateTime createdDate, string lastModifiedBy, DateTime? lastModifiedDate)
        {
            this.Id = Id;
            this.Name = name;
            this.Description = description;
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate;
            this.LastModifiedBy = lastModifiedBy;
            this.LastModifiedDate = lastModifiedDate;
        }
    }
}
