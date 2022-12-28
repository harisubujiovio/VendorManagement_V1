using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorManagement.Contracts.Base;

namespace VendorManagement.Contracts
{
    public record ContractResponseRoot
    {
        public IEnumerable<ContractResponse> responses { get; set; }

        public int totalRows { get; set; }
    }
    public record ContractResponse : AuditTrialResponse
    {
        public string ContractNo { get; set; }

        public string ContractTypeId { get; set; }

        public string ContractType { get; set; }

        public DateTime ContractDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? RenewalDate { get; set; }

        public string CommissionMethodId { get; set; }

        public string CommissionMethod { get; set; }

        public string ContractStatusId { get; set; }

        public string ContractStatus { get; set; }

        public string PartnerId { get; set; }

        public string Partner { get; set; }

        public ContractResponse(Guid Id, string contractNo, string contractTypeId,string contractType, DateTime contractDate,
            DateTime? startDate, DateTime? endDate, DateTime? renewalDate,
            string commissionMethodId, string commissionMethod, string contractStatusId,string contractStatus, 
            string partnerId, string partner,
            string createdBy, DateTime createdDate, string lastModifiedBy, DateTime? lastModifiedDate)
        {
            this.Id = Id;
            this.ContractNo= contractNo;
            this.ContractTypeId= contractTypeId;
            this.ContractType= contractType;
            this.ContractDate = contractDate;
            this.StartDate= startDate;
            this.EndDate= endDate;  
            this.RenewalDate= renewalDate;  
            this.ContractStatusId= contractStatusId;
            this.ContractStatus = contractStatus;
            this.CommissionMethodId= commissionMethodId;
            this.CommissionMethod = commissionMethod;
            this.PartnerId = partnerId;
            this.Partner = partner;
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate;
            this.LastModifiedBy = lastModifiedBy;
            this.LastModifiedDate = lastModifiedDate;
        }
    }
}
