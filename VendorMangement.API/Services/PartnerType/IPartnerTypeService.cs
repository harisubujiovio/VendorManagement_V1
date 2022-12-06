using VendorMnagement.DBclient.Models;
namespace VendorMangement.API.Services
{
    public interface IPartnerTypeService
    {
        void CreatePartnerType(PartnerType partnerTypeRequest);

        PartnerType GetPartnerType(Guid id);

        void UpdatePartnerType(Guid id,PartnerType partnerTypeRequest);

        void DeletePartnerType(Guid id);


    }
}
