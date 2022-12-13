using ErrorOr;
using VendorMnagement.DBclient.Models;
namespace VendorMangement.API.Services
{
    public interface IPartnerTypeService
    {
        ErrorOr<Created> CreatePartnerType(PartnerType partnerTypeRequest);

        ErrorOr<PartnerType> GetPartnerType(Guid id);

        ErrorOr<Updated> UpdatePartnerType(Guid id,PartnerType partnerTypeRequest);

        ErrorOr<Deleted> DeletePartnerType(Guid id);


    }
}
