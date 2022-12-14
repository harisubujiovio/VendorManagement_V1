using ErrorOr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorManagement.Contracts.Base;

namespace VendorManagement.Contracts
{
    public record StatementResponseRoot
    {
        public IEnumerable<StatementResponse> responses { get; set; }

        public int totalRows { get; set; }
    }
    public record StatementResponse : AuditTrialResponse
    {
        public DateTime StatementDate { get; set; }

        public string StatementNo { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; }

        public string PartnerId { get; set; }

        public string Partner { get; set; }

        public string ContractId { get; set; }

        public string ContractNo { get; set; }    

        public StatementResponse(Guid Id, DateTime statementDate,string statementNo,
            DateTime startDate, DateTime endDate, string status, string partnerId, string partner,
            string contractId,string contractNo, string createdBy, DateTime createdDate, string lastModifiedBy,
            DateTime? lastModifiedDate)
        {
            this.Id = Id;
            this.StatementDate = statementDate;
            this.StatementNo = statementNo;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Status= status;
            this.PartnerId = partnerId;
            this.Partner = partner;
            this.ContractId = contractId;
            this.ContractNo = contractNo;
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate;
            this.LastModifiedBy = lastModifiedBy;
            this.LastModifiedDate = lastModifiedDate;
        }
    }
}
