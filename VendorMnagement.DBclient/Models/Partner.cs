using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorMnagement.DBclient.Models
{
    public class Partner : BaseEntity
    {
        public long PartnerNo { get; set; }

        public string PartnerName { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public Guid PartnerTypeId { get; set; }

        [ForeignKey("PartnerTypeId")]
        public PartnerType PartnerType { get; set; }
    }
}
