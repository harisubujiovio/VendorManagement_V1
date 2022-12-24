using ErrorOr;
using System.Data;
using VendorManagement.Contracts;
using VendorManagement.DBclient.DBProvider;
using VendorManagement.DBclient.Models;
using VendorMnagement.DBclient.Data;
using VendorManagement.Contracts.ServiceErrors;

namespace VendorMangement.API.Services
{
    public class ContractStatusService : Base,IContractStatusService
    {
        public readonly VendorManagementDbContext _vendorManagementDbContext;
        public readonly IVendorDbOperator _vendorDbOperator;
        public readonly IQueryExecutor _queryExecutor;
        public ContractStatusService(VendorManagementDbContext vendorManagementDbContext, IVendorDbOperator vendorDbOperator, IQueryExecutor queryExecutor)
        {
            _vendorManagementDbContext = vendorManagementDbContext;
            _vendorDbOperator = vendorDbOperator;
            _queryExecutor = queryExecutor;
        }
        public ErrorOr<Created> CreateContractStatus(ContractStatus contractStatus)
        {
            _vendorManagementDbContext.ContractStatus.Add(contractStatus);
            _vendorManagementDbContext.SaveChanges();

            return Result.Created;
        }

        public ErrorOr<Deleted> DeleteContractStatus(Guid id)
        {
            var contractStatus = _vendorManagementDbContext.ContractStatus.Find(id);
            _vendorManagementDbContext.ContractStatus.Remove(contractStatus);

            return Result.Deleted;
        }

        public ErrorOr<ContractStatus> GetContractStatus(Guid id)
        {
            ContractStatus contractStatus = _vendorManagementDbContext.ContractStatus.Find(id);
            if (contractStatus != null)
                return contractStatus;

            return Errors.PartnerType.NotFound;

        }

        public ErrorOr<Updated> UpdateContractStatus(Guid id, ContractStatus contractStatus)
        {
            var dbContractStatus = _vendorManagementDbContext.ContractStatus.Find(id);
            dbContractStatus.Description = contractStatus.Description;
            dbContractStatus.LastModifiedBy = contractStatus.LastModifiedBy;
            dbContractStatus.LastModifiedDate = contractStatus.LastModifiedDate;
            _vendorManagementDbContext.SaveChanges();

            return Result.Updated;

        }
        public ErrorOr<Dictionary<Guid, string>> GetDictionary()
        {
            _vendorDbOperator.InitializeOperator("vm_sp_GetAllContractStatus", CommandType.StoredProcedure, null);
            IDataReader dr = _queryExecutor.ExecuteReader();
            Dictionary<Guid, string> keyValues = new Dictionary<Guid, string>();
            while (dr.Read())
            {
                keyValues.Add(new Guid(dr["Guid"].ToString()), dr["Description"].ToString());
            }
            return keyValues;
        }
        public ErrorOr<IEnumerable<ContractStatusResponse>> GetAllContractStatus(int pageNo, int pageSize, string sortCol = "", string sortType = "")
        {
            var parameters = this.GetPaginationParameters(pageNo, pageSize, sortCol, sortType);
            _vendorDbOperator.InitializeOperator("vm_sp_GetContractStatus", CommandType.StoredProcedure, parameters);
            IDataReader dr = _queryExecutor.ExecuteReader();
            List<ContractStatusResponse> contractStatusResponses = new();
            while (dr.Read())
            {
                ContractStatusResponse contractStatusResponse =
                    new ContractStatusResponse(
                          this.AgainstGUID(dr["Guid"]),
                          this.AgainstString(dr["Code"]),
                          this.AgainstString(dr["Description"]),
                          this.AgainstString(dr["CreatedBy"]),
                          this.AgainstDatetime(dr["CreatedDate"]),
                          this.AgainstString(dr["lastModifiedBy"]),
                          this.AgainstNullableDatetime(dr["lastModifiedDate"])
                        );

                contractStatusResponses.Add(contractStatusResponse);
            }

            return contractStatusResponses;
        }
    }
}
