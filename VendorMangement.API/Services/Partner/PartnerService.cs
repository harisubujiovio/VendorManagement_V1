using ErrorOr;
using VendorManagement.DBclient.DBProvider;
using VendorManagement.DBclient.Models;
using VendorMnagement.DBclient.Data;
using VendorManagement.Contracts.ServiceErrors;
using System.Data;
using VendorManagement.Contracts;

namespace VendorMangement.API.Services
{
    public class PartnerService : Base, IPartnerService
    {
        public readonly VendorManagementDbContext _vendorManagementDbContext;
        public readonly IVendorDbOperator _vendorDbOperator;
        public readonly IQueryExecutor _queryExecutor;
        public PartnerService(VendorManagementDbContext vendorManagementDbContext, IVendorDbOperator vendorDbOperator, IQueryExecutor queryExecutor)
        {
            _vendorManagementDbContext = vendorManagementDbContext;
            _vendorDbOperator = vendorDbOperator;
            _queryExecutor = queryExecutor;
        }
        public ErrorOr<Created> CreatePartner(Partner partner)
        {
            _vendorManagementDbContext.Partners.Add(partner);
            _vendorManagementDbContext.SaveChanges();

            return Result.Created;
        }

        public ErrorOr<Deleted> DeletePartner(Guid id)
        {
            var partner = _vendorManagementDbContext.Partners.Find(id);
            _vendorManagementDbContext.Partners.Remove(partner);

            return Result.Deleted;
        }

        public ErrorOr<Partner> GetPartner(Guid id)
        {
            Partner partner = _vendorManagementDbContext.Partners.Find(id);
            if (partner != null)
                return partner;

            return Errors.Partner.NotFound;
        }

        public ErrorOr<Updated> UpdatePartner(Guid id, Partner partner)
        {
            var dbpartner = _vendorManagementDbContext.Partners.Find(id);
            dbpartner.PartnerNo = partner.PartnerNo;
            dbpartner.PartnerName = partner.PartnerName;
            dbpartner.PartnerTypeId = partner.PartnerTypeId;
            dbpartner.Email = partner.Email;
            dbpartner.MobileNumber = partner.MobileNumber;
            dbpartner.LastModifiedBy = partner.LastModifiedBy;
            dbpartner.LastModifiedDate = partner.LastModifiedDate;
            _vendorManagementDbContext.SaveChanges();

            return Result.Updated;
        }
        public ErrorOr<Dictionary<Guid, string>> GetDictionary()
        {
            _vendorDbOperator.InitializeOperator("vm_sp_GetAllPartners", CommandType.StoredProcedure, null);
            IDataReader dr = _queryExecutor.ExecuteReader();
            Dictionary<Guid, string> keyValues = new Dictionary<Guid, string>();
            while (dr.Read())
            {
                keyValues.Add(new Guid(dr["Guid"].ToString()), dr["PartnerName"].ToString());
            }
            return keyValues;
        }
        public ErrorOr<PartnerResponseRoot> GetAll(int pageNo, int pageSize, string sortCol = "", string sortType = "")
        {
            PartnerResponseRoot partnerResponseRoot = new();
            var parameters = this.GetPaginationParameters(pageNo, pageSize, sortCol, sortType);
            _vendorDbOperator.InitializeOperator("vm_sp_GetPartners", CommandType.StoredProcedure, parameters);
            IDataReader dr = _queryExecutor.ExecuteReader();
            List<PartnerResponse> partnerResponses = new();
            while (dr.Read())
            {
                PartnerResponse partnerResponse =
                    new PartnerResponse(
                          this.AgainstGUID(dr["Guid"]),
                          this.AgainstString(dr["PartnerNo"]),
                          this.AgainstString(dr["PartnerName"]),
                          this.AgainstString(dr["Email"]),
                          this.AgainstString(dr["MobileNumber"]),
                          this.AgainstGUID(dr["PartnerTypeId"]),
                          this.AgainstString(dr["CreatedBy"]),
                          this.AgainstDatetime(dr["CreatedDate"]),
                          this.AgainstString(dr["lastModifiedBy"]),
                          this.AgainstNullableDatetime(dr["lastModifiedDate"])
                        );
                partnerResponseRoot.totalRows = this.AgainstInt(dr["TotalCount"]);
                partnerResponses.Add(partnerResponse);
            }
            partnerResponseRoot.responses = partnerResponses;
            return partnerResponseRoot;
        }
    }
}
