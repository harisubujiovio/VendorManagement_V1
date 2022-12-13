using ErrorOr;
using VendorManagement.Contracts.Authentication;
using VendorManagement.Contracts.Common;
using VendorManagement.Contracts.ServiceErrors;
using VendorManagement.DBclient.Models;
using VendorMnagement.DBclient.Data;
using static VendorManagement.Contracts.ServiceErrors.Errors;

namespace VendorMangement.API.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        public readonly VendorManagementDbContext _vendorManagementDbContext;

        public AuthenticationService(VendorManagementDbContext vendorManagementDbContext)
        {
            _vendorManagementDbContext = vendorManagementDbContext;
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
    }
}
