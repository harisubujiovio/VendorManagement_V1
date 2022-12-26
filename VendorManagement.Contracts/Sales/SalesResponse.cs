using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorManagement.Contracts.Base;

namespace VendorManagement.Contracts
{
    public record SalesResponseRoot
    {
        public IEnumerable<SalesResponse> salesResponses { get; set; }

        public int totalRows { get; set; }
    }
    public record SalesResponse : AuditTrialResponse
    {
        public long EntryNo { get; set; }
        public string Source { get; set; }
        public string PartnerId { get; set; }
        public string DocumentType { get; set; }

        public int DocumentNo { get; set; }

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
        public SalesResponse(Guid Id, long entryNo,string source,string partnerId, string documentType,
           int documentNo,int documentLineNo, DateTime date,
           string no, string quantity, string uom, decimal unitPrice,
           decimal netAmount, decimal GST, decimal discount, decimal cardPaidAmount,
           int loyaltyPoints, string promotionTxrn, decimal costShareOnDiscountAmount,
           decimal loyaltyShareAmount, decimal commissionValue, decimal promoCommissionValue,
           decimal commissionAmount, decimal costShareAmount,
           string createdBy, DateTime createdDate, string lastModifiedBy, 
            DateTime? lastModifiedDate)
        {
            this.Id = Id;
            this.EntryNo = entryNo;
            this.Source = source;
            this.PartnerId = partnerId;
            this.DocumentType = documentType;
            this.DocumentNo = documentNo;
            this.DocumentLineNo = documentLineNo;
            this.Date = date;
            this.No = no;
            this.Quantity = quantity;
            this.UOM= uom;  
            this.GST= GST;
            this.Discount = discount;
            this.UnitPrice= unitPrice;
            this.NetAmount= netAmount;
            this.PromotionTxrn = promotionTxrn;
            this.CardPaidAmount= cardPaidAmount;
            this.CommissionAmount= commissionAmount;
            this.CostShareOnDiscountAmount = costShareOnDiscountAmount;
            this.CostShareAmount = costShareAmount;
            this.CommissionValue = commissionValue;
            this.PromoCommissionValue = promoCommissionValue;
            this.LoyaltyPoints= loyaltyPoints;  
            this.LoyaltyShareAmount= loyaltyShareAmount;
            this.CreatedBy = createdBy;
            this.CreatedDate = createdDate;
            this.LastModifiedBy = lastModifiedBy;
            this.LastModifiedDate = lastModifiedDate;
        }
    }
}
