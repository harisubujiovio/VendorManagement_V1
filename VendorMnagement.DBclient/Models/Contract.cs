using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorManagement.DBclient.Models
{
    public class Contract : BaseEntity
    {
        public string ContractNo { get; set; }

        public Guid ContractTypeId { get; set; }

        public DateTime ContractDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? RenewalDate { get; set; }

        public Guid CommissionMethodId { get; set; }

        public Guid ContractStatusId { get; set; }

        public Guid PartnerId { get; set; } 

        [ForeignKey("ContractTypeId")]
        public ContractType ContractType { get; set; }

        [ForeignKey("CommissionMethodId")]
        public CommissionMethod CommissionMethod { get; set; }

        [ForeignKey("ContractStatusId")]
        public ContractStatus ContractStatus { get; set; }

        //[ForeignKey("PartnerId")]
        //public Partner Partner { get; set; }


    }
}
