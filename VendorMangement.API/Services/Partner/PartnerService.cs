using VendorManagement.DBclient.Models;
using VendorMnagement.DBclient.Data;

namespace VendorMangement.API.Services
{
    public class PartnerService : IPartnerService
    {
        public readonly VendorManagementDbContext _vendorManagementDbContext;
        public PartnerService(VendorManagementDbContext vendorManagementDbContext)
        {
            _vendorManagementDbContext = vendorManagementDbContext;
        }
        public void CreatePartner(Partner partner)
        {
            _vendorManagementDbContext.Partners.Add(partner);
            _vendorManagementDbContext.SaveChanges();
        }

        public void DeletePartner(Guid id)
        {
            var partner = _vendorManagementDbContext.Partners.Find(id);
            _vendorManagementDbContext.Partners.Remove(partner);
        }

        public Partner GetPartner(Guid id)
        {
            return _vendorManagementDbContext.Partners.Find(id);
        }

        public void UpdatePartner(Guid id, Partner partner)
        {
            var dbpartner = _vendorManagementDbContext.Partners.Find(id);
            dbpartner.PartnerNo = partner.PartnerNo;
            dbpartner.PartnerName = partner.PartnerName;
            dbpartner.PartnerTypeId = partner.PartnerTypeId;
            dbpartner.Email = partner.Email;
            dbpartner.MobileNumber = partner.MobileNumber;
            dbpartner.LastModifiedBy = partner.LastModifiedBy;
            dbpartner.LastModifiedDate = partner.LastModifiedDate;
            _vendorManagementDbContext.SaveChanges();
        }
    }
}
