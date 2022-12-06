using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendorMnagement.DBclient.Models
{
    public class ContractType : BaseEntity
    {
        public string Code { get; set; }

        public string Description { get; set; }
    }
}
