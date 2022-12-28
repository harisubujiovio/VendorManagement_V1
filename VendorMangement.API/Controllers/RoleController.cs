using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using VendorManagement.Contracts;
using VendorManagement.DBclient.Models;
using VendorManagement.Contracts.ServiceErrors;
using VendorMangement.API.Services;

namespace VendorMangement.API.Controllers
{
    public class RoleController : ApiController
    {
        public readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpPost]
        public IActionResult CreateRole(RoleRequest roleRequest)
        {
            ErrorOr<Role> requestToRoleResult = Role.From(roleRequest);
            if (requestToRoleResult.IsError)
            {
                return Problem(requestToRoleResult.Errors);
            }
            var role = requestToRoleResult.Value;
            ErrorOr<Created> createRoleResult = _roleService.CreateRole(role);
            return createRoleResult.Match(
                  created => Ok(MapRoleResponse(role)),
                  errors => Problem(errors)
                );
        }
        [HttpGet("{id:guid}")]
        public IActionResult GetRole(Guid id)
        {
            ErrorOr<Role> getRoleResult = _roleService.GetRole(id);

            return getRoleResult.Match(
                  role => Ok(MapRoleResponse(role)),
                  errors => Problem(errors)
                );

        }
        [HttpGet()]
        [Route("GetDictionary")]
        public IActionResult GetDictionary()
        {
            ErrorOr<IEnumerable<ResourceDictionary>> getDictionaryResult = _roleService.GetDictionary();
            return getDictionaryResult.Match(
                  commissionMethod => Ok(commissionMethod),
                  errors => Problem(errors)
                );
        }
        [HttpGet()]
        [Route("GetAll")]
        public IActionResult GetAll(int pageNo, int pageSize, string sortCol = "", string sortType = "")
        {
            ErrorOr<RoleResponseRoot> getAllRoleMethodResult = _roleService.GetAll(pageNo, pageSize, sortCol, sortType);
            return getAllRoleMethodResult.Match(
                  roleResponses => Ok(roleResponses),
                  errors => Problem(errors)
                );
        }
        [HttpPut("{id:guid}")]
        public IActionResult UpdatePartnerType(Guid id, RoleRequest roleRequest)
        {
            ErrorOr<Role> requestToRoleResult = Role.From(id, roleRequest);

            if (requestToRoleResult.IsError)
            {
                return Problem(requestToRoleResult.Errors);
            }
            var role = requestToRoleResult.Value;

            ErrorOr<Updated> updateRoleResult = _roleService.UpdateRole(id, role);

            RoleResponse roleResponse = new RoleResponse(
                 role.Guid,
                 role.Name,
                 role.Description,
                 role.CreatedBy,
                 role.CreatedDate,
                 role.LastModifiedBy,
                 role.LastModifiedDate
               );

            if (updateRoleResult.IsError)
            {
                return Problem(updateRoleResult.Errors);
            }

            return CreatedAtAction(
                actionName: nameof(GetRole),
                routeValues: new { id = role.Guid },
                value: roleResponse
                );

        }
        [HttpDelete("{id:guid}")]
        public IActionResult DeleteRole(Guid id)
        {
            ErrorOr<Deleted> deleteRoleResult = _roleService.DeleteRole(id);
            return deleteRoleResult.Match(deleted => NoContent(),
                errors => Problem(errors));
        }
        private static RoleResponse MapRoleResponse(Role role)
        {
            return new RoleResponse(
                 role.Guid,
                 role.Name,
                 role.Description,
                 role.CreatedBy,
                 role.CreatedDate,
                 role.LastModifiedBy,
                 role.LastModifiedDate
               );
        }
    }
}
