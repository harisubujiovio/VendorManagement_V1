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
        public IEnumerable<StatementResponse> statementResponses { get; set; }

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

        public string ContractId { get; set; }

        public StatementResponse(Guid Id, DateTime statementDate,string statementNo,
            DateTime startDate, DateTime endDate, string status, string partnerId, 
            string contractId, string createdBy, DateTime createdDate, string lastModifiedBy,
            DateTime? lastModifiedDate)
        {
            this.Id = Id;
            this.StatementDate = statementDate;
            this.StatementNo = statementNo;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Status= status;
            this.PartnerId = partnerId;
            this.ContractId = contractId;
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate;
            this.LastModifiedBy = lastModifiedBy;
            this.LastModifiedDate = lastModifiedDate;
        }
    }
}
