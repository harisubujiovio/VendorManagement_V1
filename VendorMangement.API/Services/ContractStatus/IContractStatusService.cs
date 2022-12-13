using VendorManagement.DBclient.Models;

namespace VendorMangement.API.Services
{
    public interface IContractStatusService
    {
        void CreateContractStatus(ContractStatus contractStatus);

       ContractStatus GetContractStatus(Guid id);

        void UpdateContractStatus(Guid id, ContractStatus contractStatus);

        void DeleteContractStatus(Guid id);
    }
}
