using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorManagement.Contracts.Base;

namespace VendorManagement.Contracts
{
    public record CommissionMethodResponseRoot
    {
        public IEnumerable<CommissionMethodResponse> responses { get; set; }

        public int totalRows { get; set; }  
    }
    public record CommissionMethodResponse : AuditTrialResponse
    {
        public string Code { get; set; }
        public string Description { get; }

        public CommissionMethodResponse(Guid Id,string code, string description, string createdBy, DateTime createdDate, string lastModifiedBy, DateTime? lastModifiedDate)
        {
            this.Id = Id;
            this.Code = code;
            this.Description = description;
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate;
            this.LastModifiedBy = lastModifiedBy;
            this.LastModifiedDate = lastModifiedDate;
        }
    }
}
