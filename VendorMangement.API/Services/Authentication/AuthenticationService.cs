using Azure;
using ErrorOr;
using Microsoft.Data.SqlClient;
using System.Data;
using VendorManagement.Contracts;
using VendorManagement.Contracts.Authentication;
using VendorManagement.Contracts.Common;
using VendorManagement.Contracts.ServiceErrors;
using VendorManagement.DBclient.DBProvider;
using VendorManagement.DBclient.Models;
using VendorMnagement.DBclient.Data;

namespace VendorMangement.API.Services.Authentication
{
    public class AuthenticationService : Base, IAuthenticationService
    {
        public readonly IJwtTokenGenerator _jwtTokenGenerator;
        public readonly VendorManagementDbContext _vendorManagementDbContext;
        public readonly IVendorDbOperator _vendorDbOperator;
        public readonly IQueryExecutor _queryExecutor;
        public AuthenticationService(VendorManagementDbContext vendorManagementDbContext, IJwtTokenGenerator jwtTokenGenerator,
            IVendorDbOperator vendorDbOperator, IQueryExecutor queryExecutor)
        {
            _vendorManagementDbContext = vendorManagementDbContext;
            _vendorDbOperator = vendorDbOperator;
            _queryExecutor = queryExecutor;
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public ErrorOr<LoginResponse> Login(LoginRequest loginRequest)
        {
            loginRequest.Password = HashPassword.GetPasswordHash(loginRequest.Password);
            ErrorOr<UserDetail> loginUserDetailMethodResponse = GetUserDetail(loginRequest.Username, loginRequest.Password);
            if(loginUserDetailMethodResponse.IsError)
            {
                return Errors.Login.InvalidCredentials;
            }
            if(loginUserDetailMethodResponse.Value == null)
            {
                return Errors.Login.InvalidCredentials;
            }

            UserDetail user = loginUserDetailMethodResponse.Value;
            LoginResponse loginResponse = new LoginResponse();
            loginResponse.token = _jwtTokenGenerator.GenerateJwtToken(user.userid, user.displayName,
            user.partnerid, user.roleid);

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
        public ErrorOr<UserDetail> GetUserDetail(string email, string password)
        {
            UserDetail user = null;
            List<SqlParameter> parameters = new List<SqlParameter>();
            SqlParameter sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@email";
            sqlParameter.SqlDbType = SqlDbType.VarChar;
            sqlParameter.Value = email;
            parameters.Add(sqlParameter);

            sqlParameter = new SqlParameter();
            sqlParameter.ParameterName = "@password";
            sqlParameter.SqlDbType = SqlDbType.VarChar;
            sqlParameter.Value = password;
            parameters.Add(sqlParameter);

            _vendorDbOperator.InitializeOperator("vm_sp_GetUserDetail", CommandType.StoredProcedure, parameters);
            IDataReader dr = _queryExecutor.ExecuteReader();
            while (dr.Read())
            {
                user = new UserDetail(
                          this.AgainstGUID(dr["UserId"]),
                          this.AgainstString(dr["DisplayName"]),
                          this.AgainstNullableGUID(dr["PartnerId"]),
                          this.AgainstGUID(dr["RoleId"])
                    );
            }

            return user;
        }
    }
}
