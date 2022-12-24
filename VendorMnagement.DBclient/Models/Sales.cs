using ErrorOr;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorManagement.Contracts;
using VendorManagement.Contracts.ServiceErrors;
namespace VendorManagement.DBclient.Models
{
    public class Sales : BaseEntity
    {
        public long EntryNo { get; set; }   
        public string Source { get; set; }
        public Guid PartnerId { get; set; }
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

        [ForeignKey("PartnerId")]
        public Partner Partner { get; set; }
        public static ErrorOr<Sales> From(SalesRequest salesRequest)
        {
            return Create(salesRequest);
        }
        public static ErrorOr<Sales> From(Guid Id, SalesRequest salesRequest)
        {
            return Update(salesRequest);
        }
        private static ErrorOr<Sales> Create(SalesRequest salesRequest)
        {
            List<Error> errors = Validate(salesRequest);

            if (errors.Count > 0)
                return errors;

            Sales sales = new Sales();
            sales.EntryNo= salesRequest.EntryNo;
            sales.Source = salesRequest.Source;
            sales.PartnerId = new Guid(salesRequest.PartnerId);
            sales.DocumentType = salesRequest.DocumentType;
            sales.DocumentNo= salesRequest.DocumentNo;  
            sales.DocumentLineNo= salesRequest.DocumentLineNo;
            sales.Date = salesRequest.Date;
            sales.No= salesRequest.No;
            sales.Quantity= salesRequest.Quantity;
            sales.UOM = salesRequest.UOM;
            sales.UnitPrice = salesRequest.UnitPrice;
            sales.NetAmount = salesRequest.NetAmount;
            sales.GST = salesRequest.GST;
            sales.Discount= salesRequest.Discount;
            sales.CardPaidAmount=salesRequest.CardPaidAmount;
            sales.LoyaltyPoints= salesRequest.LoyaltyPoints;
            sales.PromotionTxrn= salesRequest.PromotionTxrn;
            sales.CostShareOnDiscountAmount=salesRequest.CostShareOnDiscountAmount;
            sales.LoyaltyShareAmount=salesRequest.LoyaltyShareAmount;
            sales.CommissionValue = salesRequest.CommissionValue;
            sales.PromoCommissionValue = salesRequest.PromoCommissionValue;
            sales.CommissionAmount = salesRequest.CommissionAmount;
            sales.CreatedDate = DateTime.UtcNow;
            sales.CreatedBy = "System";
            return sales;
        }
        private static ErrorOr<Sales> Update(SalesRequest salesRequest)
        {
            List<Error> errors = Validate(salesRequest);

            if (errors.Count > 0)
                return errors;

            Sales sales = new Sales();
            sales.EntryNo = salesRequest.EntryNo;
            sales.Source = salesRequest.Source;
            sales.PartnerId = new Guid(salesRequest.PartnerId);
            sales.DocumentType = salesRequest.DocumentType;
            sales.DocumentNo = salesRequest.DocumentNo;
            sales.DocumentLineNo = salesRequest.DocumentLineNo;
            sales.Date = salesRequest.Date;
            sales.No = salesRequest.No;
            sales.Quantity = salesRequest.Quantity;
            sales.UOM = salesRequest.UOM;
            sales.UnitPrice = salesRequest.UnitPrice;
            sales.NetAmount = salesRequest.NetAmount;
            sales.GST = salesRequest.GST;
            sales.Discount = salesRequest.Discount;
            sales.CardPaidAmount = salesRequest.CardPaidAmount;
            sales.LoyaltyPoints = salesRequest.LoyaltyPoints;
            sales.PromotionTxrn = salesRequest.PromotionTxrn;
            sales.CostShareOnDiscountAmount = salesRequest.CostShareOnDiscountAmount;
            sales.LoyaltyShareAmount = salesRequest.LoyaltyShareAmount;
            sales.CommissionValue = salesRequest.CommissionValue;
            sales.PromoCommissionValue = salesRequest.PromoCommissionValue;
            sales.CommissionAmount = salesRequest.CommissionAmount;
            sales.LastModifiedDate = DateTime.UtcNow;
            sales.LastModifiedBy = "System";
            return sales;
        }
        private static List<Error> Validate(SalesRequest salesRequest)
        {
            List<Error> errors = new();
            if (salesRequest.EntryNo < 1)
            {
                errors.Add(Errors.Sales.InvalidEntryNo);
            }
            if (string.IsNullOrEmpty(salesRequest.Source))
            {
                errors.Add(Errors.Sales.InvalidSource);
            }
            if (string.IsNullOrEmpty(salesRequest.DocumentType))
            {
                errors.Add(Errors.Sales.InvalidDocumentType);
            }
            if (salesRequest.DocumentNo < 1)
            {
                errors.Add(Errors.Sales.InvalidDocumentNo);
            }
            if (salesRequest.DocumentLineNo < 1)
            {
                errors.Add(Errors.Sales.InvalidDocumentLineNo);
            }

            return errors;
        }
    }
}
