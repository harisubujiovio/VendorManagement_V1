using ErrorOr;
using VendorManagement.Contracts;
using VendorManagement.DBclient.Models;

namespace VendorMangement.API.Services
{
    public interface IContractService
    {
        ErrorOr<Created> CreateContract(Contract contract);

        ErrorOr<Contract> GetContract(Guid id);

        ErrorOr<Updated> UpdateContract(Guid id, Contract contract);

        ErrorOr<Deleted> DeleteContract(Guid id);

        ErrorOr<IEnumerable<ResourceDictionary>> GetDictionary();

        ErrorOr<ContractResponseRoot> GetAll(string partnerId,
            string contractTypeId = "",string commissionMethodId = "",string contractStatusId = "",
            int pageNo = 0, int pageSize = 10, string sortCol = "", string sortType = "", string filterKey = "");
    }
}
