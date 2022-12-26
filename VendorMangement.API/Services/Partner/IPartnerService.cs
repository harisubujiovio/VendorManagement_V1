

using ErrorOr;
using VendorManagement.Contracts;
using VendorManagement.DBclient.Models;

namespace VendorMangement.API.Services
{
    public interface IPartnerService
    {
        ErrorOr<Created> CreatePartner(Partner partner);

        ErrorOr<Partner> GetPartner(Guid id);

        ErrorOr<Updated> UpdatePartner(Guid id, Partner partner);

        ErrorOr<Deleted> DeletePartner(Guid id);

        ErrorOr<Dictionary<Guid, string>> GetDictionary();

        ErrorOr<PartnerResponseRoot> GetAllPartners(int pageNo, int pageSize, string sortCol = "", string sortType = "");
    }
}
