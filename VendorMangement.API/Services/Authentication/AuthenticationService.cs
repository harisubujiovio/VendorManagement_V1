using ErrorOr;
using Microsoft.Data.SqlClient;
using System.Data;
using VendorManagement.Contracts.Authentication;
using VendorManagement.Contracts.Common;
using VendorManagement.Contracts.ServiceErrors;
using VendorManagement.DBclient.DBProvider;
using VendorManagement.DBclient.Models;
using VendorMnagement.DBclient.Data;
using static VendorManagement.Contracts.ServiceErrors.Errors;

namespace VendorMangement.API.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public readonly VendorManagementDbContext _vendorManagementDbContext;
        public readonly IVendorDbOperator _vendorDbOperator;
        public readonly IQueryExecutor _queryExecutor;
        public AuthenticationService(VendorManagementDbContext vendorManagementDbContext, IVendorDbOperator vendorDbOperator, IQueryExecutor queryExecutor)
        {
            _vendorManagementDbContext = vendorManagementDbContext;
            _vendorDbOperator = vendorDbOperator;
            _queryExecutor = queryExecutor;
        }
        public ErrorOr<LoginResponse> Login(LoginRequest loginRequest)
        {
            loginRequest.Password = HashPassword.GetPasswordHash(loginRequest.Password);
            var user = _vendorManagementDbContext.Users.Where(f => f.email == loginRequest.Username 
            && f.password == loginRequest.Password).FirstOrDefault();

            if(user == null)
            {
                return Errors.Login.InvalidCredentials;
            }
            LoginResponse loginResponse = new LoginResponse();
            loginResponse.firstName = user.firstName;
            loginResponse.lastName = user.lastName;
            loginResponse.UserId = user.Guid;
            return loginResponse;
        }

        public ErrorOr<Created> Register(User user)
        {
            _vendorManagementDbContext.Users.Add(user);
            _vendorManagementDbContext.SaveChanges();

            return Result.Created;
        }
        public ErrorOr<bool> AssignUserRole(Guid userid, Guid roleid)
        {
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@userId";
            sqlParameter.SqlDbType = SqlDbType.UniqueIdentifier;
            sqlParameter.Value = userid;
            parameters.Add(sqlParameter);

            sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@roleId";
            sqlParameter.SqlDbType = SqlDbType.UniqueIdentifier;
            sqlParameter.Value = roleid;
            parameters.Add(sqlParameter);

            _vendorDbOperator.InitializeOperator("vm_sp_AssignUserRole", CommandType.StoredProcedure, parameters);
            int rowsAffected = _queryExecutor.ExecuteQuery();
            return rowsAffected > 0;
        }
    }
}
