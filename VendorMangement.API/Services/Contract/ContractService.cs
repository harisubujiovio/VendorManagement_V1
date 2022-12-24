﻿using ErrorOr;
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
        public ErrorOr<Dictionary<Guid, string>> GetDictionary()
        {
            _vendorDbOperator.InitializeOperator("vm_sp_GetAllContracts", CommandType.StoredProcedure, null);
            IDataReader dr = _queryExecutor.ExecuteReader();
            Dictionary<Guid, string> keyValues = new Dictionary<Guid, string>();
            while (dr.Read())
            {
                keyValues.Add(new Guid(dr["Guid"].ToString()), dr["ContractNo"].ToString());
            }
            return keyValues;
        }
        public ErrorOr<IEnumerable<ContractResponse>> GetAllContracts(string partnerId,
            string contractTypeId, string commissionMethodId, string contractStatusId,
            int pageNo, int pageSize, string sortCol = "", string sortType = "")
        {
            var parameters = this.GetPaginationParameters(pageNo, pageSize, sortCol, sortType);
            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@partnerId";
            sqlParameter.SqlDbType = SqlDbType.UniqueIdentifier;
            sqlParameter.Value = string.IsNullOrEmpty(partnerId) ? Guid.Empty : new Guid(partnerId);
            parameters.Add(sqlParameter);

            sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@contractTypeId";
            sqlParameter.SqlDbType = SqlDbType.UniqueIdentifier;
            sqlParameter.Value = string.IsNullOrEmpty(contractTypeId) ? Guid.Empty : new Guid(contractTypeId);
            parameters.Add(sqlParameter);

            sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@commissionMethodId";
            sqlParameter.SqlDbType = SqlDbType.UniqueIdentifier;
            sqlParameter.Value = string.IsNullOrEmpty(commissionMethodId) ? Guid.Empty : new Guid(commissionMethodId);
            parameters.Add(sqlParameter);

            sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@contractStatusId";
            sqlParameter.SqlDbType = SqlDbType.UniqueIdentifier;
            sqlParameter.Value = string.IsNullOrEmpty(contractStatusId) ? Guid.Empty : new Guid(contractStatusId);
            parameters.Add(sqlParameter);

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
                          this.AgainstDatetime(dr["ContractDate"]),
                          this.AgainstNullableDatetime(dr["StartDate"]),
                          this.AgainstNullableDatetime(dr["EndDate"]),
                          this.AgainstNullableDatetime(dr["RenewalDate"]),
                          this.AgainstString(dr["CommissionMethodId"]),
                          this.AgainstString(dr["ContractStatusId"]),
                          this.AgainstString(dr["PartnerId"]),
                          this.AgainstString(dr["CreatedBy"]),
                          this.AgainstDatetime(dr["CreatedDate"]),
                          this.AgainstString(dr["lastModifiedBy"]),
                          this.AgainstNullableDatetime(dr["lastModifiedDate"])
                          
                        );

                contractResponses.Add(contractResponse);
            }

            return contractResponses;
        }
    }
}
