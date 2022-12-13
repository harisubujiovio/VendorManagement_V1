using ErrorOr;
using VendorMangement.API.ServiceErrors;
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
        public ErrorOr<Created> CreatePartnerType(PartnerType partnerType)
        {
            _vendorManagementDbContext.PartnerTypes.Add(partnerType);
            _vendorManagementDbContext.SaveChanges();

            return Result.Created;
        }

        public ErrorOr<Deleted> DeletePartnerType(Guid id)
        {
            var partnerType = _vendorManagementDbContext.PartnerTypes.Find(id);
            _vendorManagementDbContext.PartnerTypes.Remove(partnerType);

            return Result.Deleted;
        }

        public ErrorOr<PartnerType> GetPartnerType(Guid id)
        {
            PartnerType partnerType = _vendorManagementDbContext.PartnerTypes.Find(id);
            if (partnerType != null)
                return partnerType;

            return Errors.PartnerType.NotFound;
        }

        public ErrorOr<Updated> UpdatePartnerType(Guid id,PartnerType partnerType)
        {
            var dbpartnerType = _vendorManagementDbContext.PartnerTypes.Find(id);
            dbpartnerType.Description = partnerType.Description;
            dbpartnerType.LastModifiedBy = partnerType.LastModifiedBy;
            dbpartnerType.LastModifiedDate = partnerType.LastModifiedDate;
            _vendorManagementDbContext.SaveChanges();

            return Result.Updated;
        }
    }
}
