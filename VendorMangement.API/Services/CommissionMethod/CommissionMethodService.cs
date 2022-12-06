using VendorMnagement.DBclient.Data;
using VendorMnagement.DBclient.Models;

namespace VendorMangement.API.Services
{
    public class CommissionMethodService : ICommissionMethodService
    {
        public readonly VendorManagementDbContext _vendorManagementDbContext;

        public CommissionMethodService(VendorManagementDbContext vendorManagementDbContext)
        {
            _vendorManagementDbContext = vendorManagementDbContext;
        }
        public void CreateCommissionMethod(CommissionMethod commissionMethod)
        {
            _vendorManagementDbContext.CommissionMethods.Add(commissionMethod);
            _vendorManagementDbContext.SaveChanges();
        }

        public void DeleteCommissionMethod(Guid id)
        {
            var commissionMethod = _vendorManagementDbContext.CommissionMethods.Find(id);
            _vendorManagementDbContext.CommissionMethods.Remove(commissionMethod);
        }

        public CommissionMethod GetCommissionMethod(Guid id)
        {
            return _vendorManagementDbContext.CommissionMethods.Find(id);
        }

        public void UpdateCommissionMethod(Guid id, CommissionMethod commissionMethod)
        {
            var dbcommissionMethod = _vendorManagementDbContext.CommissionMethods.Find(id);
            dbcommissionMethod.Description = commissionMethod.Description;
            dbcommissionMethod.LastModifiedBy = commissionMethod.LastModifiedBy;
            dbcommissionMethod.LastModifiedDate = commissionMethod.LastModifiedDate;
            _vendorManagementDbContext.SaveChanges();
        }
    }
}
