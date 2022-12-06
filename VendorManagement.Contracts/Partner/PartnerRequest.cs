using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorManagement.Contracts
{
    public record PartnerRequest
    {
        public long PartnerNo { get; set; }

        public string PartnerName { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public Guid PartnerTypeId { get; set; }
    }
}
