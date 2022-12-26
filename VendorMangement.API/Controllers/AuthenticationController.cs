using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VendorManagement.Contracts.Authentication;
using VendorManagement.DBclient.Models;
using IAuthenticationService = VendorMangement.API.Services.Authentication.IAuthenticationService;
using UserModel = VendorManagement.DBclient.Models.User;
namespace VendorMangement.API.Controllers
{
    public class AuthenticationController : ApiController
    {
        public readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public IActionResult Login(LoginRequest loginRequest)
        {
            ErrorOr<LoginRequest> requestToLoginResult = UserModel.From(loginRequest);
            if (requestToLoginResult.IsError)
            {
                return Problem(requestToLoginResult.Errors);
            }
           
            ErrorOr<LoginResponse> signinLoginResult = _authenticationService.Login(loginRequest);

            return signinLoginResult.Match(
                  success => Ok(success),
                  errors => Problem(errors)
                );
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public IActionResult Register(RegisterRequest registerRequest)
        {
            ErrorOr<User> requestToRegisterResult = UserModel.From(registerRequest);
            if (requestToRegisterResult.IsError)
            {
                return Problem(requestToRegisterResult.Errors);
            }
            var user = requestToRegisterResult.Value;
            ErrorOr<Created> createUserResult = _authenticationService.Register(user);
            RegisterResponse response = new RegisterResponse();
            if (!createUserResult.IsError)
            {
                _authenticationService.AssignUserRole(user.Guid, new Guid(registerRequest.roleid));
                response.UserId = user.Guid;
                response.firstName = user.firstName; 
                response.lastName = user.lastName;
                response.email = user.email;
                response.address = user.Address;
                response.mobileNumber = user.MobileNumber;
                //response.token = _jwtTokenGenerator.GenerateJwtToken(response.UserId,
                //    response.firstName, response.lastName);
            }
            return createUserResult.Match(
                  success => Ok(response),
                  errors => Problem(errors)
                );
        }
    }
}
