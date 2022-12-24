using ErrorOr;
using VendorManagement.Contracts;
using VendorManagement.DBclient.Models;

namespace VendorMangement.API.Services
{
    public interface IRoleService
    {
        ErrorOr<Created> CreateRole(Role partnerTypeRequest);

        ErrorOr<Role> GetRole(Guid id);

        ErrorOr<Updated> UpdateRole(Guid id, Role partnerTypeRequest);

        ErrorOr<Deleted> DeleteRole(Guid id);
        ErrorOr<Dictionary<Guid, string>> GetDictionary();

        ErrorOr<IEnumerable<RoleResponse>> GetAllRoles(int pageNo, int pageSize, string sortCol = "", string sortType = "");
    }
}
