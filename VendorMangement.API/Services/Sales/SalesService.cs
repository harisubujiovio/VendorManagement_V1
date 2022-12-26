using ErrorOr;
using VendorManagement.DBclient.DBProvider;
using VendorManagement.DBclient.Models;
using VendorMnagement.DBclient.Data;
using VendorManagement.Contracts.ServiceErrors;
using System.Data;
using VendorManagement.Contracts;
using Microsoft.Data.SqlClient;

namespace VendorMangement.API.Services
{
    public class SalesService : Base, ISalesSevice
    {
        public readonly VendorManagementDbContext _vendorManagementDbContext;
        public readonly IVendorDbOperator _vendorDbOperator;
        public readonly IQueryExecutor _queryExecutor;
        public SalesService(VendorManagementDbContext vendorManagementDbContext, IVendorDbOperator vendorDbOperator, IQueryExecutor queryExecutor)
        {
            _vendorManagementDbContext = vendorManagementDbContext;
            _vendorDbOperator = vendorDbOperator;
            _queryExecutor = queryExecutor;
        }
        public ErrorOr<Created> CreateSales(Sales sales)
        {
            _vendorManagementDbContext.Sales.Add(sales);
            _vendorManagementDbContext.SaveChanges();

            return Result.Created;
        }

        public ErrorOr<Deleted> DeleteSales(Guid id)
        {
            var sales = _vendorManagementDbContext.Sales.Find(id);
            _vendorManagementDbContext.Sales.Remove(sales);

            return Result.Deleted;
        }
        public ErrorOr<Sales> GetSales(Guid id)
        {
            Sales sales = _vendorManagementDbContext.Sales.Find(id);
            if (sales != null)
                return sales;

            return Errors.Sales.NotFound;
        }
        public ErrorOr<Updated> UpdateSales(Guid id, Sales sales)
        {
            var dbsales = _vendorManagementDbContext.Sales.Find(id);
            dbsales.EntryNo = sales.EntryNo;
            dbsales.Source = sales.Source;
            dbsales.PartnerId = sales.PartnerId;
            dbsales.DocumentType = sales.DocumentType;
            dbsales.Code = sales.Code;
            dbsales.DocumentLineNo = sales.DocumentLineNo;
            dbsales.Date = sales.Date;
            dbsales.No = sales.No;
            dbsales.Quantity = sales.Quantity;
            dbsales.UOM = sales.UOM;
            dbsales.UnitPrice = sales.UnitPrice;
            dbsales.NetAmount = sales.NetAmount;
            dbsales.GST = sales.GST;
            dbsales.Discount = sales.Discount;
            dbsales.CardPaidAmount = sales.CardPaidAmount;
            dbsales.LoyaltyPoints = sales.LoyaltyPoints;
            dbsales.PromotionTxrn = sales.PromotionTxrn;
            dbsales.CostShareOnDiscountAmount = sales.CostShareOnDiscountAmount;
            dbsales.LoyaltyShareAmount = sales.LoyaltyShareAmount;
            dbsales.CommissionValue = sales.CommissionValue;
            dbsales.PromoCommissionValue = sales.PromoCommissionValue;
            dbsales.CommissionAmount = sales.CommissionAmount;
            dbsales.LastModifiedBy = sales.LastModifiedBy;
            dbsales.LastModifiedDate = sales.LastModifiedDate;
            _vendorManagementDbContext.SaveChanges();

            return Result.Updated;
        }
        public ErrorOr<Dictionary<Guid, string>> GetDictionary()
        {
            _vendorDbOperator.InitializeOperator("vm_sp_GetAllSales", CommandType.StoredProcedure, null);
            IDataReader dr = _queryExecutor.ExecuteReader();
            Dictionary<Guid, string> keyValues = new Dictionary<Guid, string>();
            while (dr.Read())
            {
                keyValues.Add(new Guid(dr["Guid"].ToString()), dr["DocumentNo"].ToString());
            }
            return keyValues;
        }
        public ErrorOr<SalesResponseRoot> GetAll(string partnerId,int pageNo, int pageSize, string sortCol = "", string sortType = "")
        {
            SalesResponseRoot salesResponseRoot = new();
            var parameters = this.GetPaginationParameters(pageNo, pageSize, sortCol, sortType);
            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@partnerId";
            sqlParameter.SqlDbType = SqlDbType.UniqueIdentifier;
            sqlParameter.Value = string.IsNullOrEmpty(partnerId) ? Guid.Empty : new Guid(partnerId);
            parameters.Add(sqlParameter);

            _vendorDbOperator.InitializeOperator("vm_sp_GetSales", CommandType.StoredProcedure, parameters);
            IDataReader dr = _queryExecutor.ExecuteReader();
            List<SalesResponse> salesResponses = new();
            while (dr.Read())
            {
                SalesResponse salesResponse =
                    new SalesResponse(
                          this.AgainstGUID(dr["Guid"]),
                          this.AgainstLong(dr["EntryNo"]),
                          this.AgainstString(dr["Source"]),
                          this.AgainstString(dr["PartnerId"]),
                          this.AgainstString(dr["DocumentType"]),
                          this.AgainstString(dr["Code"]),
                          this.AgainstInt(dr["DocumentLineNo"]),
                          this.AgainstDatetime(dr["Date"]),
                          this.AgainstString(dr["No"]),
                          this.AgainstString(dr["Quantity"]),
                          this.AgainstString(dr["UOM"]),
                          this.AgainstDecimal(dr["UnitPrice"]),
                          this.AgainstDecimal(dr["NetAmount"]),
                          this.AgainstDecimal(dr["GST"]),
                          this.AgainstDecimal(dr["Discount"]),
                          this.AgainstDecimal(dr["CardPaidAmount"]),
                          this.AgainstInt(dr["LoyaltyPoints"]),
                          this.AgainstString(dr["PromotionTxrn"]),
                          this.AgainstDecimal(dr["CostShareOnDiscountAmount"]),
                          this.AgainstDecimal(dr["LoyaltyShareAmount"]),
                          this.AgainstDecimal(dr["CommissionValue"]),
                          this.AgainstDecimal(dr["PromoCommissionValue"]),
                          this.AgainstDecimal(dr["CommissionAmount"]),
                          this.AgainstDecimal(dr["CostShareAmount"]),
                          this.AgainstString(dr["CreatedBy"]),
                          this.AgainstDatetime(dr["CreatedDate"]),
                          this.AgainstString(dr["lastModifiedBy"]),
                          this.AgainstNullableDatetime(dr["lastModifiedDate"])
                        );
                salesResponseRoot.totalRows = this.AgainstInt(dr["TotalCount"]);
                salesResponses.Add(salesResponse);
            }
            salesResponseRoot.responses = salesResponses;
            return salesResponseRoot;
        }
    }
}
