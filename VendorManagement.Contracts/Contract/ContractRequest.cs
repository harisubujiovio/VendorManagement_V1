using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorManagement.Contracts
{
    public class ContractRequest
    {
        public string ContractNo { get; set; }

        public string ContractTypeId { get; set; }

        public DateTime ContractDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? RenewalDate { get; set; }

        public string CommissionMethodId { get; set; }

        public string ContractStatusId { get; set; }

        public string PartnerId { get; set; }
    }
}
