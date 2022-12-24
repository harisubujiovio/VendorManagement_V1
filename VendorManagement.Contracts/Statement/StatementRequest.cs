using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorManagement.Contracts
{
    public class StatementRequest
    {
        public DateTime StatementDate { get; set; }

        public string StatementNo { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; }

        public string PartnerId { get; set; }

        public string ContractId { get; set; }
    }
}
