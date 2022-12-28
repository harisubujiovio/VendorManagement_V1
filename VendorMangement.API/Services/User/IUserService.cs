using ErrorOr;
using VendorManagement.Contracts;
using VendorManagement.DBclient.Models;

namespace VendorMangement.API.Services
{
    public interface IUserService
    {
        ErrorOr<User> GetUser(Guid id);

        ErrorOr<Updated> UpdateUser(Guid id, User user);

        ErrorOr<Deleted> DeleteUser(Guid id);
        ErrorOr<IEnumerable<ResourceDictionary>> GetDictionary();

        ErrorOr<UserResponseRoot> GetAll(int pageNo, int pageSize, string sortCol = "", string sortType = "");
    }
}
