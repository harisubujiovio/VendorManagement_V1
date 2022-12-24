using ErrorOr;
using VendorManagement.Contracts;
using VendorManagement.DBclient.Models;

namespace VendorMangement.API.Services
{
    public interface IContractStatusService
    {
        ErrorOr<Created> CreateContractStatus(ContractStatus contractStatus);

        ErrorOr<ContractStatus> GetContractStatus(Guid id);

        ErrorOr<Updated> UpdateContractStatus(Guid id, ContractStatus contractStatus);

        ErrorOr<Deleted> DeleteContractStatus(Guid id);

        ErrorOr<Dictionary<Guid, string>> GetDictionary();

        ErrorOr<IEnumerable<ContractStatusResponse>> GetAllContractStatus(int pageNo, int pageSize, string sortCol = "", string sortType = "");
    }
}
