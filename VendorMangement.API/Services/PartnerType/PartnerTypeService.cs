using VendorMnagement.DBclient.Data;
using VendorMnagement.DBclient.Models;
namespace VendorMangement.API.Services
{
    public class PartnerTypeService : IPartnerTypeService
    {
        public readonly VendorManagementDbContext _vendorManagementDbContext;

        public PartnerTypeService(VendorManagementDbContext vendorManagementDbContext)
        {
            _vendorManagementDbContext = vendorManagementDbContext;
        }
        public void CreatePartnerType(PartnerType partnerType)
        {
            _vendorManagementDbContext.PartnerTypes.Add(partnerType);
            _vendorManagementDbContext.SaveChanges();
        }

        public void DeletePartnerType(Guid id)
        {
            var partnerType = _vendorManagementDbContext.PartnerTypes.Find(id);
            _vendorManagementDbContext.PartnerTypes.Remove(partnerType);
        }

        public PartnerType GetPartnerType(Guid id)
        {
            return _vendorManagementDbContext.PartnerTypes.Find(id);   
        }

        public void UpdatePartnerType(Guid id,PartnerType partnerType)
        {
            var dbpartnerType = _vendorManagementDbContext.PartnerTypes.Find(id);
            dbpartnerType.Description = partnerType.Description;
            dbpartnerType.LastModifiedBy = partnerType.LastModifiedBy;
            dbpartnerType.LastModifiedDate = partnerType.LastModifiedDate;
            _vendorManagementDbContext.SaveChanges();
        }
    }
}
