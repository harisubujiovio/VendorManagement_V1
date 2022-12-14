using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static VendorManagement.Contracts.ServiceErrors.Errors;

namespace VendorManagement.Contracts
{
    public class SalesRequest
    {
        public long EntryNo { get; set; }
        public string Source { get; set; }
        public string PartnerId { get; set; }
        public string DocumentType { get; set; }

        public string Code { get; set; }

        public int DocumentLineNo { get; set; }

        public DateTime Date { get; set; }

        public string No { get; set; }

        public string Quantity { get; set; }

        public string UOM { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal NetAmount { get; set; }

        public decimal GST { get; set; }

        public decimal Discount { get; set; }

        public decimal CardPaidAmount { get; set; }

        public int LoyaltyPoints { get; set; }

        public string PromotionTxrn { get; set; }

        public decimal CostShareOnDiscountAmount { get; set; }

        public decimal LoyaltyShareAmount { get; set; }

        public decimal CommissionValue { get; set; }

        public decimal PromoCommissionValue { get; set; }

        public decimal CommissionAmount { get; set; }

        public decimal CostShareAmount { get; set; }

    }
}
