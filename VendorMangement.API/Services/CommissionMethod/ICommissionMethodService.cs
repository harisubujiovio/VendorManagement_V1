
using ErrorOr;
using VendorManagement.Contracts;
using VendorManagement.DBclient.Models;

namespace VendorMangement.API.Services
{
    public interface ICommissionMethodService
    {
        ErrorOr<Created> CreateCommissionMethod(CommissionMethod commissionMethod);

        ErrorOr<CommissionMethod> GetCommissionMethod(Guid id);

        ErrorOr<Updated> UpdateCommissionMethod(Guid id, CommissionMethod commissionMethod);

        ErrorOr<Deleted> DeleteCommissionMethod(Guid id);

        ErrorOr<IEnumerable<ResourceDictionary>> GetDictionary();

        ErrorOr<CommissionMethodResponseRoot> GetAll(int pageNo, int pageSize,string sortCol = "", string sortType = "");
    }
}
