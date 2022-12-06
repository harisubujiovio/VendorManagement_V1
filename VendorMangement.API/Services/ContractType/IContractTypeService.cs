using VendorMnagement.DBclient.Models;

namespace VendorMangement.API.Services
{
    public interface IContractTypeService
    {
        void CreateContractType(ContractType contractType);

        ContractType GetContractType(Guid id);

        void UpdateContractType(Guid id, ContractType contractType);

        void DeleteContractType(Guid id);
    }
}
