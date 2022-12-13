using ErrorOr;
using Microsoft.Identity.Client;
using VendorManagement.Contracts.Authentication;
using VendorManagement.DBclient.Models;

namespace VendorMangement.API.Services.Authentication
{
    public interface IAuthenticationService
    {
        ErrorOr<Created> Register(User user);

        ErrorOr<LoginResponse> Login(LoginRequest loginRequest);
    }
}
