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
    public class ContractService : Base, IContractService
    {
        public readonly VendorManagementDbContext _vendorManagementDbContext;
        public readonly IVendorDbOperator _vendorDbOperator;
        public readonly IQueryExecutor _queryExecutor;
        public ContractService(VendorManagementDbContext vendorManagementDbContext, IVendorDbOperator vendorDbOperator, IQueryExecutor queryExecutor)
        {
            _vendorManagementDbContext = vendorManagementDbContext;
            _vendorDbOperator = vendorDbOperator;
            _queryExecutor = queryExecutor;
        }
        public ErrorOr<Created> CreateContract(Contract contract)
        {
            _vendorManagementDbContext.Contracts.Add(contract);
            _vendorManagementDbContext.SaveChanges();

            return Result.Created;
        }

        public ErrorOr<Deleted> DeleteContract(Guid id)
        {
            var contract = _vendorManagementDbContext.Contracts.Find(id);
            _vendorManagementDbContext.Contracts.Remove(contract);
            _vendorManagementDbContext.SaveChanges();
            return Result.Deleted;
        }
        public ErrorOr<Contract> GetContract(Guid id)
        {
            Contract contract = _vendorManagementDbContext.Contracts.Find(id);
            if (contract != null)
                return contract;

            return Errors.Contract.NotFound;
        }
        public ErrorOr<Updated> UpdateContract(Guid id, Contract contract)
        {
            var dbContracts = _vendorManagementDbContext.Contracts.Find(id);
            dbContracts.ContractNo = contract.ContractNo;
            dbContracts.ContractTypeId = contract.ContractTypeId;
            dbContracts.ContractDate = contract.ContractDate;
            dbContracts.StartDate = contract.StartDate;
            dbContracts.EndDate = contract.EndDate;
            dbContracts.RenewalDate = contract.RenewalDate;
            dbContracts.CommissionMethodId = contract.CommissionMethodId;
            dbContracts.ContractStatusId = contract.ContractStatusId;
            dbContracts.PartnerId = contract.PartnerId;
            dbContracts.LastModifiedBy = contract.LastModifiedBy;
            dbContracts.LastModifiedDate = contract.LastModifiedDate;
            _vendorManagementDbContext.SaveChanges();

            return Result.Updated;
        }
        public ErrorOr<IEnumerable<ResourceDictionary>> GetDictionary()
        {
            List<ResourceDictionary> resourceDictionaries = new();
            ResourceDictionary resourceDictionary = null;
            _vendorDbOperator.InitializeOperator("vm_sp_GetAllContracts", CommandType.StoredProcedure, null);
            IDataReader dr = _queryExecutor.ExecuteReader();
            Dictionary<Guid, string> keyValues = new Dictionary<Guid, string>();
            while (dr.Read())
            {
                resourceDictionary = new(new Guid(dr["Guid"].ToString()), dr["Description"].ToString());
                resourceDictionaries.Add(resourceDictionary);
            }
            return resourceDictionaries;
        }
     
        public ErrorOr<ContractResponseRoot> GetAll(string partnerId,
            string contractTypeId, string commissionMethodId, string contractStatusId,
            int pageNo, int pageSize, string sortCol = "", string sortType = "", string filterKey = "")
        {
            ContractResponseRoot contractResponseRoot = new();

            this.AddFilters("partnerId", SqlDbType.UniqueIdentifier, string.IsNullOrEmpty(partnerId) ? Guid.Empty : new Guid(partnerId));
            this.AddFilters("contractTypeId", SqlDbType.UniqueIdentifier, string.IsNullOrEmpty(contractTypeId) ? Guid.Empty : new Guid(contractTypeId));
            this.AddFilters("commissionMethodId", SqlDbType.UniqueIdentifier, string.IsNullOrEmpty(commissionMethodId) ? Guid.Empty : new Guid(commissionMethodId));
            this.AddFilters("contractStatusId", SqlDbType.UniqueIdentifier, string.IsNullOrEmpty(contractStatusId) ? Guid.Empty : new Guid(contractStatusId));
            this.AddFilters("filterKey", SqlDbType.VarChar, string.IsNullOrEmpty(filterKey) ? string.Empty : filterKey);
            var parameters = this.GetPaginationParameters(pageNo, pageSize, sortCol, sortType);
            
            _vendorDbOperator.InitializeOperator("vm_sp_GetContracts", CommandType.StoredProcedure, parameters);
            IDataReader dr = _queryExecutor.ExecuteReader();
            List<ContractResponse> contractResponses = new();
            while (dr.Read())
            {
                ContractResponse contractResponse =
                    new ContractResponse(
                          this.AgainstGUID(dr["Guid"]),
                          this.AgainstString(dr["ContractNo"]),
                          this.AgainstString(dr["ContractTypeId"]),
                          this.AgainstString(dr["ContractType"]),
                          this.AgainstDatetime(dr["ContractDate"]),
                          this.AgainstNullableDatetime(dr["StartDate"]),
                          this.AgainstNullableDatetime(dr["EndDate"]),
                          this.AgainstNullableDatetime(dr["RenewalDate"]),
                          this.AgainstString(dr["CommissionMethodId"]),
                          this.AgainstString(dr["CommissionMethod"]),
                          this.AgainstString(dr["ContractStatusId"]),
                          this.AgainstString(dr["ContractStatus"]),
                          this.AgainstString(dr["PartnerId"]),
                          this.AgainstString(dr["Partner"]),
                          this.AgainstString(dr["CreatedBy"]),
                          this.AgainstDatetime(dr["CreatedDate"]),
                          this.AgainstString(dr["lastModifiedBy"]),
                          this.AgainstNullableDatetime(dr["lastModifiedDate"])
                        );
                contractResponseRoot.totalRows = this.AgainstInt(dr["TotalCount"]);
                contractResponses.Add(contractResponse);
            }
            contractResponseRoot.responses = contractResponses;
            return contractResponseRoot;
        }
    }
}
