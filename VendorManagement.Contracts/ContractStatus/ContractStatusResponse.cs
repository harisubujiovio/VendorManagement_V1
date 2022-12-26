using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorManagement.Contracts.Base;

namespace VendorManagement.Contracts
{
    public record ContractStatusResponseRoot
    {
        public IEnumerable<ContractStatusResponse> responses { get; set; }

        public int totalRows { get; set; }
    }
    public record ContractStatusResponse : AuditTrialResponse
    {
        public string Code { get; }
        public string Description { get; }

        public ContractStatusResponse(Guid Id,string code, string description, string createdBy, DateTime createdDate, string lastModifiedBy, DateTime? lastModifiedDate)
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
