using VendorMnagement.DBclient.Data;
using VendorMnagement.DBclient.Models;

namespace VendorMangement.API.Services
{
    public class ContractStatusService : IContractStatusService
    {
        public readonly VendorManagementDbContext _vendorManagementDbContext;

        public ContractStatusService(VendorManagementDbContext vendorManagementDbContext)
        {
            _vendorManagementDbContext = vendorManagementDbContext;
        }
        public void CreateContractStatus(ContractStatus contractStatus)
        {
            _vendorManagementDbContext.ContractStatus.Add(contractStatus);
            _vendorManagementDbContext.SaveChanges();
        }

        public void DeleteContractStatus(Guid id)
        {
            var contractStatus = _vendorManagementDbContext.ContractStatus.Find(id);
            _vendorManagementDbContext.ContractStatus.Remove(contractStatus);
        }

        public ContractStatus GetContractStatus(Guid id)
        {
            return _vendorManagementDbContext.ContractStatus.Find(id);
        }

        public void UpdateContractStatus(Guid id, ContractStatus contractStatus)
        {
            var dbContractStatus = _vendorManagementDbContext.ContractStatus.Find(id);
            dbContractStatus.Description = contractStatus.Description;
            dbContractStatus.LastModifiedBy = contractStatus.LastModifiedBy;
            dbContractStatus.LastModifiedDate = contractStatus.LastModifiedDate;
            _vendorManagementDbContext.SaveChanges();
        }
    }
}
