using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using VendorManagement.Contracts;
using VendorManagement.Contracts.Authentication;
using VendorManagement.DBclient.Models;
using VendorMangement.API.Services;
using VendorMangement.API.Services.Authentication;

namespace VendorMangement.API.Controllers
{
    public class UserController : ApiController
    {
        public readonly IUserService _userService;
        public readonly IAuthenticationService _authenticationService;

        public UserController(IUserService userService, IAuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
        }
        [HttpGet("{id:guid}")]
        public IActionResult GetUser(Guid id)
        {
            ErrorOr<User> getUserResult = _userService.GetUser(id);
            return getUserResult.Match(
                  statement => Ok(MapUserResponse(statement)),
                  errors => Problem(errors)
                );

        }
        [HttpGet()]
        [Route("GetDictionary")]
        public IActionResult GetDictionary()
        {
            ErrorOr<IEnumerable<ResourceDictionary>> getDictionaryResult = _userService.GetDictionary();
            return getDictionaryResult.Match(
                  user => Ok(user),
                  errors => Problem(errors)
                );
        }
        [HttpGet()]
        [Route("GetAll")]
        public IActionResult GetAll(int pageNo, int pageSize, string sortCol = "", string sortType = "")
        {
            ErrorOr<UserResponseRoot> getAllUserMethodResult = _userService.GetAll(pageNo, pageSize, sortCol, sortType);
            return getAllUserMethodResult.Match(
                  usersResponses => Ok(usersResponses),
                  errors => Problem(errors)
                );
        }
        [HttpPut("{id:guid}")]
        public IActionResult UpdateUser(Guid id, UserRequest userRequest)
        {
            ErrorOr<User> requestToUserResult = VendorManagement.DBclient.Models.User.From(id, userRequest);

            if (requestToUserResult.IsError)
            {
                return Problem(requestToUserResult.Errors);
            }
            var user = requestToUserResult.Value;

            ErrorOr<Updated> updateUserResult = _userService.UpdateUser(id, user);

            UserResponse userResponse = MapUserResponse(user);

            if (updateUserResult.IsError)
            {
                return Problem(updateUserResult.Errors);
            }
            if(!string.IsNullOrEmpty(userRequest.role))
                _authenticationService.AssignUserRole(user.Guid, new Guid(userRequest.role));

            return CreatedAtAction(
                actionName: nameof(GetUser),
                routeValues: new { id = user.Guid },
                value: userResponse
                );

        }
        [HttpDelete("{id:guid}")]
        public IActionResult DeleteUser(Guid id)
        {
            ErrorOr<Deleted> deleteStatementResult = _userService.DeleteUser(id);
            return deleteStatementResult.Match(deleted => NoContent(),
                errors => Problem(errors));
        }

        private static UserResponse MapUserResponse(User user)
        {
            return new UserResponse(
                 user.Guid,
                 user.firstName,
                 user.lastName,
                 user.email,
                 user.MobileNumber,
                 user.Address,
                 "",
                 user.CreatedBy,
                 user.CreatedDate,
                 user.LastModifiedBy,
                 user.LastModifiedDate
               );
        }
    }
}
