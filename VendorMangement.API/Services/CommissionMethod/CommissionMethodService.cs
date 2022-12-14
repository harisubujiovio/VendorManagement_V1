using ErrorOr;
using VendorManagement.Contracts.ServiceErrors;
using VendorManagement.DBclient.Models;
using VendorMnagement.DBclient.Data;

namespace VendorMangement.API.Services
{
    public class CommissionMethodService : ICommissionMethodService
    {
        public IConfiguration _configuration { get; }
        public readonly VendorManagementDbContext _vendorManagementDbContext;

        public CommissionMethodService(VendorManagementDbContext vendorManagementDbContext, IConfiguration configuration)
        {
            _vendorManagementDbContext = vendorManagementDbContext;
            _configuration = configuration;
        }
        public ErrorOr<Created> CreateCommissionMethod(CommissionMethod commissionMethod)
        {
            string connectionString = _configuration["ConnectionStrings:VendorMgmtConnectionString"];
            _vendorManagementDbContext.CommissionMethods.Add(commissionMethod);
            _vendorManagementDbContext.SaveChanges();
            return Result.Created;
        }

        public ErrorOr<Deleted> DeleteCommissionMethod(Guid id)
        {
            var commissionMethod = _vendorManagementDbContext.CommissionMethods.Find(id);
            _vendorManagementDbContext.CommissionMethods.Remove(commissionMethod);
            return Result.Deleted;
        }

        public ErrorOr<CommissionMethod> GetCommissionMethod(Guid id)
        {
            var commissionMethod = _vendorManagementDbContext.CommissionMethods.Find(id);
            if (commissionMethod != null)
                return commissionMethod;

            return Errors.CommissionMethod.NotFound;
        }

        public ErrorOr<Updated> UpdateCommissionMethod(Guid id, CommissionMethod commissionMethod)
        {
            var dbcommissionMethod = _vendorManagementDbContext.CommissionMethods.Find(id);
            dbcommissionMethod.Description = commissionMethod.Description;
            dbcommissionMethod.LastModifiedBy = commissionMethod.LastModifiedBy;
            dbcommissionMethod.LastModifiedDate = commissionMethod.LastModifiedDate;
            _vendorManagementDbContext.SaveChanges();

            return Result.Updated;
        }

        public ErrorOr<Dictionary<Guid, string>> GetAllCommissionMethods()
        {
            throw new NotImplementedException();
        }
    }
}
