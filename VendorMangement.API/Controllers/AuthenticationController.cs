using ErrorOr;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VendorManagement.Contracts;
using VendorManagement.Contracts.Authentication;
using VendorManagement.DBclient.Models;
using VendorMangement.API.Services;
using VendorMangement.API.Services.Authentication;
using IAuthenticationService = VendorMangement.API.Services.Authentication.IAuthenticationService;
using UserModel = VendorManagement.DBclient.Models.User;
namespace VendorMangement.API.Controllers
{
    public class AuthenticationController : ApiController
    {
        public readonly IAuthenticationService _authenticationService;
        public readonly IJwtTokenGenerator _jwtTokenGenerator;

        public AuthenticationController(IAuthenticationService authenticationService, IJwtTokenGenerator jwtTokenGenerator)
        {
            _authenticationService = authenticationService;
            _jwtTokenGenerator = jwtTokenGenerator;
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
            if (!signinLoginResult.IsError)
            {
                LoginResponse response = signinLoginResult.Value;
                response.token = _jwtTokenGenerator.GenerateJwtToken(response.UserId,
                    response.firstName, response.lastName);
            }
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
                response.token = _jwtTokenGenerator.GenerateJwtToken(response.UserId,
                    response.firstName, response.lastName);
            }
            return createUserResult.Match(
                  success => Ok(response),
                  errors => Problem(errors)
                );
        }
    }
}
