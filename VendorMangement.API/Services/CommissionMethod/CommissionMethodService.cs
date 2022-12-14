using ErrorOr;
using Microsoft.Data.SqlClient;
using System.Data;
using VendorManagement.Contracts;
using VendorManagement.Contracts.ServiceErrors;
using VendorManagement.DBclient.DBProvider;
using VendorManagement.DBclient.Models;
using VendorMnagement.DBclient.Data;

namespace VendorMangement.API.Services
{
    public class CommissionMethodService : Base, ICommissionMethodService
    {
        public readonly VendorManagementDbContext _vendorManagementDbContext;
        public readonly IVendorDbOperator _vendorDbOperator;
        public readonly IQueryExecutor _queryExecutor;
        public CommissionMethodService(VendorManagementDbContext vendorManagementDbContext, IVendorDbOperator vendorDbOperator, IQueryExecutor queryExecutor)
        {
            _vendorManagementDbContext = vendorManagementDbContext;
            _vendorDbOperator = vendorDbOperator;
            _queryExecutor = queryExecutor;
        }
        public ErrorOr<Created> CreateCommissionMethod(CommissionMethod commissionMethod)
        {
            _vendorManagementDbContext.CommissionMethods.Add(commissionMethod);
            _vendorManagementDbContext.SaveChanges();
            return Result.Created;
        }

        public ErrorOr<Deleted> DeleteCommissionMethod(Guid id)
        {
            var commissionMethod = _vendorManagementDbContext.CommissionMethods.Find(id);
            _vendorManagementDbContext.CommissionMethods.Remove(commissionMethod);
            _vendorManagementDbContext.SaveChanges();
            return Result.Deleted;
        }

        public ErrorOr<CommissionMethod> GetCommissionMethod(Guid id)
        {
            var commissionMethod = _vendorManagementDbContext.CommissionMethods.Find(id);
            if (commissionMethod != null)
                return commissionMethod;

            return Errors.CommissionMethod.NotFound;
        }

        public ErrorOr<Updated> UpdateCommissionMethod(Guid id, CommissionMethod commissionMethod)
        {
            var dbcommissionMethod = _vendorManagementDbContext.CommissionMethods.Find(id);
            dbcommissionMethod.Description = commissionMethod.Description;
            dbcommissionMethod.LastModifiedBy = commissionMethod.LastModifiedBy;
            dbcommissionMethod.LastModifiedDate = commissionMethod.LastModifiedDate;
            _vendorManagementDbContext.SaveChanges();

            return Result.Updated;
        }
        public ErrorOr<IEnumerable<ResourceDictionary>> GetDictionary()
        {
            List<ResourceDictionary> resourceDictionaries = new();
            ResourceDictionary resourceDictionary = null;
            _vendorDbOperator.InitializeOperator("vm_sp_GetAllCommissionMethods", CommandType.StoredProcedure, null);
            IDataReader dr = _queryExecutor.ExecuteReader();
            Dictionary<Guid, string> keyValues = new Dictionary<Guid, string>();
            while (dr.Read())
            {
                resourceDictionary = new(new Guid(dr["Guid"].ToString()), dr["Description"].ToString());
                resourceDictionaries.Add(resourceDictionary);
            }
            return resourceDictionaries;
        }
    

        public ErrorOr<CommissionMethodResponseRoot> GetAll(int pageNo, int pageSize, string sortCol = "", string sortType = "")
        {
            CommissionMethodResponseRoot commissionMethod = new CommissionMethodResponseRoot();
            var parameters = this.GetPaginationParameters(pageNo, pageSize, sortCol, sortType);
            _vendorDbOperator.InitializeOperator("vm_sp_GetCommissionMethods", CommandType.StoredProcedure, parameters);
            IDataReader dr = _queryExecutor.ExecuteReader();
            List<CommissionMethodResponse> commissionMethods = new();
            while (dr.Read())
            {
                CommissionMethodResponse commissionMethodResponse = new CommissionMethodResponse(
                          this.AgainstGUID(dr["Guid"]),
                          this.AgainstString(dr["Code"]),
                          this.AgainstString(dr["Description"]),
                          this.AgainstString(dr["CreatedBy"]),
                          this.AgainstDatetime(dr["CreatedDate"]),
                          this.AgainstString(dr["lastModifiedBy"]),
                          this.AgainstNullableDatetime(dr["lastModifiedDate"])
                    );
                commissionMethods.Add(commissionMethodResponse);
                commissionMethod.totalRows = this.AgainstInt(dr["TotalCount"]);
            }
            commissionMethod.responses = commissionMethods;
            return commissionMethod;
        }

       
    }
}
