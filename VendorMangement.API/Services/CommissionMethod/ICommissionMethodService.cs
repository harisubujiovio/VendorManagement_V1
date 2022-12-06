using VendorMnagement.DBclient.Models;

namespace VendorMangement.API.Services
{
    public interface ICommissionMethodService
    {
        void CreateCommissionMethod(CommissionMethod comissionMethodRequest);

        CommissionMethod GetCommissionMethod(Guid id);

        void UpdateCommissionMethod(Guid id, CommissionMethod comissionMethodRequest);

        void DeleteCommissionMethod(Guid id);
    }
}
