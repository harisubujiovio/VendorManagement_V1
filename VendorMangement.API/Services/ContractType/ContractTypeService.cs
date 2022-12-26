using ErrorOr;
using System.Data;
using VendorManagement.Contracts;
using VendorManagement.DBclient.DBProvider;
using VendorManagement.DBclient.Models;
using VendorMnagement.DBclient.Data;

namespace VendorMangement.API.Services
{
    public class ContractTypeService : Base, IContractTypeService
    {
        public readonly VendorManagementDbContext _vendorManagementDbContext;
        public readonly IVendorDbOperator _vendorDbOperator;
        public readonly IQueryExecutor _queryExecutor;
        public ContractTypeService(VendorManagementDbContext vendorManagementDbContext, IVendorDbOperator vendorDbOperator, IQueryExecutor queryExecutor)
        {
            _vendorManagementDbContext = vendorManagementDbContext;
            _vendorDbOperator = vendorDbOperator;
            _queryExecutor = queryExecutor;
        }
        public void CreateContractType(ContractType contractType)
        {
            _vendorManagementDbContext.ContractTypes.Add(contractType);
            _vendorManagementDbContext.SaveChanges();
        }

        public void DeleteContractType(Guid id)
        {
            var contractType = _vendorManagementDbContext.ContractTypes.Find(id);
            _vendorManagementDbContext.ContractTypes.Remove(contractType);
        }

        public ContractType GetContractType(Guid id)
        {
            return _vendorManagementDbContext.ContractTypes.Find(id);
        }

        public ContractType GetContractTypeByCode(string code)
        {
            throw new NotImplementedException();
        }

        public void UpdateContractType(Guid id, ContractType contractType)
        {
            var dbContractType = _vendorManagementDbContext.ContractTypes.Find(id);
            dbContractType.Description = contractType.Description;
            dbContractType.LastModifiedBy = contractType.LastModifiedBy;
            dbContractType.LastModifiedDate = contractType.LastModifiedDate;
            _vendorManagementDbContext.SaveChanges();
        }
        public ErrorOr<Dictionary<Guid, string>> GetDictionary()
        {
            _vendorDbOperator.InitializeOperator("vm_sp_GetAllContractTypes", CommandType.StoredProcedure, null);
            IDataReader dr = _queryExecutor.ExecuteReader();
            Dictionary<Guid, string> keyValues = new Dictionary<Guid, string>();
            while (dr.Read())
            {
                keyValues.Add(new Guid(dr["Guid"].ToString()), dr["Description"].ToString());
            }
            return keyValues;
        }
        public ErrorOr<ContractTypeResponseRoot> GetAllContractTypes(int pageNo, int pageSize, string sortCol = "", string sortType = "")
        {
            ContractTypeResponseRoot contractTypeResponseRoot = new();
            var parameters = this.GetPaginationParameters(pageNo, pageSize, sortCol, sortType);
            _vendorDbOperator.InitializeOperator("vm_sp_GetContractTypes", CommandType.StoredProcedure, parameters);
            IDataReader dr = _queryExecutor.ExecuteReader();
            List<ContractTypeResponse> contractTypeResponses = new();
            while (dr.Read())
            {
                ContractTypeResponse contractTypeResponse =
                    new ContractTypeResponse(
                          this.AgainstGUID(dr["Guid"]),
                          this.AgainstString(dr["Code"]),
                          this.AgainstString(dr["Description"]),
                          this.AgainstString(dr["CreatedBy"]),
                          this.AgainstDatetime(dr["CreatedDate"]),
                          this.AgainstString(dr["lastModifiedBy"]),
                          this.AgainstNullableDatetime(dr["lastModifiedDate"])
                        );
                contractTypeResponseRoot.totalRows = this.AgainstInt(dr["TotalCount"]);
                contractTypeResponses.Add(contractTypeResponse);
            }
            contractTypeResponseRoot.contractTypeResponses = contractTypeResponses;
            return contractTypeResponseRoot;
        }
    }
}
