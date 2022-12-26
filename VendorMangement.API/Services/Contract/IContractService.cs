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

        ErrorOr<Dictionary<Guid, string>> GetDictionary();

        ErrorOr<ContractResponseRoot> GetAllContracts(string partnerId,
            string contractTypeId = "",string commissionMethodId = "",string contractStatusId = "",
            int pageNo = 0, int pageSize = 10, string sortCol = "", string sortType = "");
    }
}
