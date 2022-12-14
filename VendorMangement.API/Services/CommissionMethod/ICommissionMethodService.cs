
using ErrorOr;
using VendorManagement.DBclient.Models;

namespace VendorMangement.API.Services
{
    public interface ICommissionMethodService
    {
        ErrorOr<Created> CreateCommissionMethod(CommissionMethod commissionMethod);

        ErrorOr<CommissionMethod> GetCommissionMethod(Guid id);

        ErrorOr<Updated> UpdateCommissionMethod(Guid id, CommissionMethod commissionMethod);

        ErrorOr<Deleted> DeleteCommissionMethod(Guid id);
    }
}
