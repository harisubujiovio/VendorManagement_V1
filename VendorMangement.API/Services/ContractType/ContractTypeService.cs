using VendorManagement.DBclient.Models;
using VendorMnagement.DBclient.Data;

namespace VendorMangement.API.Services
{
    public class ContractTypeService : IContractTypeService
    {
        public readonly VendorManagementDbContext _vendorManagementDbContext;

        public ContractTypeService(VendorManagementDbContext vendorManagementDbContext)
        {
            _vendorManagementDbContext = vendorManagementDbContext;
        }
        public void CreateContractType(ContractType contractType)
        {
            _vendorManagementDbContext.ContractTypes.Add(contractType);
            _vendorManagementDbContext.SaveChanges();
        }

        public void DeleteContractType(Guid id)
        {
            var contractType = _vendorManagementDbContext.ContractTypes.Find(id);
            _vendorManagementDbContext.ContractTypes.Remove(contractType);
        }

        public ContractType GetContractType(Guid id)
        {
            return _vendorManagementDbContext.ContractTypes.Find(id);
        }

        public void UpdateContractType(Guid id, ContractType contractType)
        {
            var dbContractType = _vendorManagementDbContext.ContractTypes.Find(id);
            dbContractType.Description = contractType.Description;
            dbContractType.LastModifiedBy = contractType.LastModifiedBy;
            dbContractType.LastModifiedDate = contractType.LastModifiedDate;
            _vendorManagementDbContext.SaveChanges();
        }
    }
}
