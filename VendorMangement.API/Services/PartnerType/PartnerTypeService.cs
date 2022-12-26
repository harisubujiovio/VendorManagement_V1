using ErrorOr;
using VendorManagement.DBclient.Models;
using VendorManagement.Contracts.ServiceErrors;
using VendorMnagement.DBclient.Data;
using VendorManagement.DBclient.DBProvider;
using System.Data;
using VendorManagement.Contracts;

namespace VendorMangement.API.Services
{
    public class PartnerTypeService : Base, IPartnerTypeService
    {
        public readonly VendorManagementDbContext _vendorManagementDbContext;
        public readonly IVendorDbOperator _vendorDbOperator;
        public readonly IQueryExecutor _queryExecutor;
        public PartnerTypeService(VendorManagementDbContext vendorManagementDbContext, IVendorDbOperator vendorDbOperator, IQueryExecutor queryExecutor)
        {
            _vendorManagementDbContext = vendorManagementDbContext;
            _vendorDbOperator = vendorDbOperator;
            _queryExecutor = queryExecutor;
        }
        public ErrorOr<Created> CreatePartnerType(PartnerType partnerType)
        {
            _vendorManagementDbContext.PartnerTypes.Add(partnerType);
            _vendorManagementDbContext.SaveChanges();

            return Result.Created;
        }

        public ErrorOr<Deleted> DeletePartnerType(Guid id)
        {
            var partnerType = _vendorManagementDbContext.PartnerTypes.Find(id);
            _vendorManagementDbContext.PartnerTypes.Remove(partnerType);

            return Result.Deleted;
        }

        public ErrorOr<PartnerType> GetPartnerType(Guid id)
        {
            PartnerType partnerType = _vendorManagementDbContext.PartnerTypes.Find(id);
            if (partnerType != null)
                return partnerType;

            return Errors.PartnerType.NotFound;
        }

        public ErrorOr<Updated> UpdatePartnerType(Guid id,PartnerType partnerType)
        {
            var dbpartnerType = _vendorManagementDbContext.PartnerTypes.Find(id);
            dbpartnerType.Description = partnerType.Description;
            dbpartnerType.LastModifiedBy = partnerType.LastModifiedBy;
            dbpartnerType.LastModifiedDate = partnerType.LastModifiedDate;
            _vendorManagementDbContext.SaveChanges();

            return Result.Updated;
        }
        public ErrorOr<Dictionary<Guid, string>> GetDictionary()
        {
            _vendorDbOperator.InitializeOperator("vm_sp_GetAllPartnerTypes", CommandType.StoredProcedure, null);
            IDataReader dr = _queryExecutor.ExecuteReader();
            Dictionary<Guid, string> keyValues = new Dictionary<Guid, string>();
            while (dr.Read())
            {
                keyValues.Add(new Guid(dr["Guid"].ToString()), dr["Description"].ToString());
            }
            return keyValues;
        }
        public ErrorOr<PartnerTypeResponseRoot> GetAllPartnerTypes(int pageNo, int pageSize, string sortCol = "", string sortType = "")
        {
            PartnerTypeResponseRoot partnerTypeResponseRoot = new();
            var parameters = this.GetPaginationParameters(pageNo, pageSize, sortCol, sortType);
            _vendorDbOperator.InitializeOperator("vm_sp_GetPartnerTypes", CommandType.StoredProcedure, parameters);
            IDataReader dr = _queryExecutor.ExecuteReader();
            List<PartnerTypeResponse> partnerTypeResponses = new();
            while (dr.Read())
            {
                PartnerTypeResponse partnerTypeResponse = 
                    new PartnerTypeResponse(
                          this.AgainstGUID(dr["Guid"]),
                          this.AgainstString(dr["Code"]),
                          this.AgainstString(dr["Description"]),
                          this.AgainstString(dr["CreatedBy"]),
                          this.AgainstDatetime(dr["CreatedDate"]),
                          this.AgainstString(dr["lastModifiedBy"]),
                          this.AgainstNullableDatetime(dr["lastModifiedDate"])
                        );
                partnerTypeResponseRoot.totalRows = this.AgainstInt(dr["TotalCount"]);
                partnerTypeResponses.Add(partnerTypeResponse);
            }
            partnerTypeResponseRoot.partnerTypeResponses = partnerTypeResponses;
            return partnerTypeResponseRoot;
        }
    }
}
