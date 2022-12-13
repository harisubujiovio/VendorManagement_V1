using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorManagement.DBclient.Models
{
    public class Statement : BaseEntity
    {
        public DateTime StatementDate { get; set; }

        public string StatementNo { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; }

        public Guid PartnerId { get; set; }

        public Guid ContractId { get; set; }

        [ForeignKey("PartnerId")]
        public Partner Partner { get; set; }

        [ForeignKey("ContractId")]
        public Contract Contract { get; set; }
    }
}
