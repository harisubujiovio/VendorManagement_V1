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
    public class StatementService : Base, IStatementService
    {
        public readonly VendorManagementDbContext _vendorManagementDbContext;
        public readonly IVendorDbOperator _vendorDbOperator;
        public readonly IQueryExecutor _queryExecutor;
        public StatementService(VendorManagementDbContext vendorManagementDbContext, IVendorDbOperator vendorDbOperator, IQueryExecutor queryExecutor)
        {
            _vendorManagementDbContext = vendorManagementDbContext;
            _vendorDbOperator = vendorDbOperator;
            _queryExecutor = queryExecutor;
        }
        public ErrorOr<Created> CreateStatement(Statement statement)
        {
            _vendorManagementDbContext.Statements.Add(statement);
            _vendorManagementDbContext.SaveChanges();

            return Result.Created;
        }
        public ErrorOr<Deleted> DeleteStatement(Guid id)
        {
            var statement = _vendorManagementDbContext.Statements.Find(id);
            _vendorManagementDbContext.Statements.Remove(statement);

            return Result.Deleted;
        }
        public ErrorOr<Statement> GetStatement(Guid id)
        {
            Statement statement = _vendorManagementDbContext.Statements.Find(id);
            if (statement != null)
                return statement;

            return Errors.Statement.NotFound;
        }
        public ErrorOr<Updated> UpdateStatement(Guid id, Statement statement)
        {
            var dbStatement = _vendorManagementDbContext.Statements.Find(id);
            dbStatement.StatementNo = statement.StatementNo;
            dbStatement.StatementDate = statement.StatementDate;
            dbStatement.StartDate = statement.StartDate;
            dbStatement.Status = statement.Status;
            dbStatement.PartnerId = statement.PartnerId;
            dbStatement.ContractId = statement.ContractId;
            dbStatement.LastModifiedBy = statement.LastModifiedBy;
            dbStatement.LastModifiedDate = statement.LastModifiedDate;
            _vendorManagementDbContext.SaveChanges();

            return Result.Updated;
        }
        public ErrorOr<Dictionary<Guid, string>> GetDictionary()
        {
            _vendorDbOperator.InitializeOperator("vm_sp_GetAllStatements", CommandType.StoredProcedure, null);
            IDataReader dr = _queryExecutor.ExecuteReader();
            Dictionary<Guid, string> keyValues = new Dictionary<Guid, string>();
            while (dr.Read())
            {
                keyValues.Add(new Guid(dr["Guid"].ToString()), dr["StatementNo"].ToString());
            }
            return keyValues;
        }
        public ErrorOr<IEnumerable<StatementResponse>> GetAllStatements(string partnerId, string contractId, 
            int pageNo, int pageSize, string sortCol = "", string sortType = "")
        {
            var parameters = this.GetPaginationParameters(pageNo, pageSize, sortCol, sortType);
            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@partnerId";
            sqlParameter.SqlDbType = SqlDbType.UniqueIdentifier;
            sqlParameter.Value = string.IsNullOrEmpty(partnerId) ? Guid.Empty : new Guid(partnerId);
            parameters.Add(sqlParameter);

            sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@contractId";
            sqlParameter.SqlDbType = SqlDbType.UniqueIdentifier;
            sqlParameter.Value = string.IsNullOrEmpty(contractId) ? Guid.Empty : new Guid(contractId);
            parameters.Add(sqlParameter);


            _vendorDbOperator.InitializeOperator("vm_sp_GetStatements", CommandType.StoredProcedure, parameters);
            IDataReader dr = _queryExecutor.ExecuteReader();
            List<StatementResponse> statementResponses = new();
            while (dr.Read())
            {
                StatementResponse statementResponse =
                    new StatementResponse(
                          this.AgainstGUID(dr["Guid"]),
                          this.AgainstDatetime(dr["StatementDate"]),
                          this.AgainstString(dr["StatementNo"]),
                          this.AgainstDatetime(dr["StartDate"]),
                          this.AgainstDatetime(dr["EndDate"]),
                          this.AgainstString(dr["Status"]),
                          this.AgainstString(dr["PartnerId"]),
                          this.AgainstString(dr["ContractId"]),
                          this.AgainstString(dr["CreatedBy"]),
                          this.AgainstDatetime(dr["CreatedDate"]),
                          this.AgainstString(dr["lastModifiedBy"]),
                          this.AgainstNullableDatetime(dr["lastModifiedDate"])

                        );

                statementResponses.Add(statementResponse);
            }

            return statementResponses;
        }
    }
}
