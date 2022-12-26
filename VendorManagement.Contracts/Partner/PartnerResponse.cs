using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorManagement.Contracts.Base;

namespace VendorManagement.Contracts
{
    public record PartnerResponseRoot
    {
        public IEnumerable<PartnerResponse> responses { get; set; }

        public int totalRows { get; set; }
    }
    public record PartnerResponse : AuditTrialResponse
    {
        public string PartnerNo { get; set; }

        public string PartnerName { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public Guid PartnerTypeId { get; set; }

        public PartnerResponse(Guid Id, string partnerNo, string partnerName, string email,string mobileNumber, Guid partnerTypeId,
            string createdBy, DateTime createdDate, string lastModifiedBy, DateTime? lastModifiedDate)
        {
            this.Id = Id;
            this.PartnerNo = partnerNo;
            this.PartnerName = partnerName;
            this.Email = email;
            this.MobileNumber = mobileNumber;
            this.PartnerTypeId = partnerTypeId;
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate;
            this.LastModifiedBy = lastModifiedBy;
            this.LastModifiedDate = lastModifiedDate;
        }
    }
}
