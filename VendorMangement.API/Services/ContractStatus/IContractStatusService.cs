using VendorMnagement.DBclient.Models;

namespace VendorMangement.API.Services
{
    public interface IContractStatusService
    {
        void CreateContractStatus(VendorMnagement.DBclient.Models.ContractStatus contractStatus);

        VendorMnagement.DBclient.Models.ContractStatus GetContractStatus(Guid id);

        void UpdateContractStatus(Guid id, VendorMnagement.DBclient.Models.ContractStatus contractStatus);

        void DeleteContractStatus(Guid id);
    }
}
