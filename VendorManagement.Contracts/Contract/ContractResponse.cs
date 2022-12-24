using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorManagement.Contracts.Base;

namespace VendorManagement.Contracts
{
    public record ContractResponse : AuditTrialResponse
    {
        public string ContractNo { get; set; }

        public string ContractTypeId { get; set; }

        public DateTime ContractDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? RenewalDate { get; set; }

        public string CommissionMethodId { get; set; }

        public string ContractStatusId { get; set; }

        public ContractResponse(Guid Id, string contractNo, string contractTypeId, DateTime contractDate,
            DateTime? startDate, DateTime? endDate, DateTime? renewalDate,
            string commissionMethodId,string contractStatusId,string partnerId,
            string createdBy, DateTime createdDate, string lastModifiedBy, DateTime? lastModifiedDate)
        {
            this.Id = Id;
            this.ContractNo= contractNo;
            this.ContractTypeId= contractTypeId;
            this.ContractDate = contractDate;
            this.StartDate= startDate;
            this.EndDate= endDate;  
            this.RenewalDate= renewalDate;  
            this.ContractStatusId= contractStatusId;
            this.CommissionMethodId= commissionMethodId;
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate;
            this.LastModifiedBy = lastModifiedBy;
            this.LastModifiedDate = lastModifiedDate;
        }
    }
}
