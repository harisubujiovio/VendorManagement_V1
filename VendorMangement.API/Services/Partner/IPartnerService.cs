using VendorMnagement.DBclient.Models;

namespace VendorMangement.API.Services
{
    public interface IPartnerService
    {
        void CreatePartner(Partner partner);

        Partner GetPartner(Guid id);

        void UpdatePartner(Guid id, Partner partner);

        void DeletePartner(Guid id);
    }
}
