using ErrorOr;
using VendorManagement.Contracts;
using VendorManagement.DBclient.Models;

namespace VendorMangement.API.Services
{
    public interface IContractTypeService
    {
        void CreateContractType(ContractType contractType);

        ContractType GetContractType(Guid id);

        ContractType GetContractTypeByCode(string code);

        void UpdateContractType(Guid id, ContractType contractType);

        void DeleteContractType(Guid id);

        ErrorOr<Dictionary<Guid, string>> GetDictionary();

        ErrorOr<ContractTypeResponseRoot> GetAll(int pageNo, int pageSize, string sortCol = "", string sortType = "");
    }
}
