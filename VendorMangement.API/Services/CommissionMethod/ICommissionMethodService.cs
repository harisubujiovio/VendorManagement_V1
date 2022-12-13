
using VendorManagement.DBclient.Models;

namespace VendorMangement.API.Services
{
    public interface ICommissionMethodService
    {
        void CreateCommissionMethod(CommissionMethod commissionMethod);

        CommissionMethod GetCommissionMethod(Guid id);

        void UpdateCommissionMethod(Guid id, CommissionMethod commissionMethod);

        void DeleteCommissionMethod(Guid id);
    }
}
