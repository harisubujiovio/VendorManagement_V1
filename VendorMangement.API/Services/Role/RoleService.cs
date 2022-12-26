using ErrorOr;
using VendorManagement.DBclient.Models;
using VendorManagement.DBclient.DBProvider;
using VendorMnagement.DBclient.Data;
using VendorManagement.Contracts.ServiceErrors;
using System.Data;
using VendorManagement.Contracts;

namespace VendorMangement.API.Services
{
    public class RoleService : Base, IRoleService
    {
        public readonly VendorManagementDbContext _vendorManagementDbContext;
        public readonly IVendorDbOperator _vendorDbOperator;
        public readonly IQueryExecutor _queryExecutor;
        public RoleService(VendorManagementDbContext vendorManagementDbContext, IVendorDbOperator vendorDbOperator, IQueryExecutor queryExecutor)
        {
            _vendorManagementDbContext = vendorManagementDbContext;
            _vendorDbOperator = vendorDbOperator;
            _queryExecutor = queryExecutor;
        }
        public ErrorOr<Created> CreateRole(Role role)
        {
            _vendorManagementDbContext.Roles.Add(role);
            _vendorManagementDbContext.SaveChanges();

            return Result.Created;
        }

        public ErrorOr<Deleted> DeleteRole(Guid id)
        {
            var role = _vendorManagementDbContext.Roles.Find(id);
            _vendorManagementDbContext.Roles.Remove(role);

            return Result.Deleted;
        }
        public ErrorOr<Role> GetRole(Guid id)
        {
            Role role = _vendorManagementDbContext.Roles.Find(id);
            if (role != null)
                return role;

            return Errors.Role.NotFound;
        }

        public ErrorOr<Updated> UpdateRole(Guid id, Role role)
        {
            var dbrole = _vendorManagementDbContext.Roles.Find(id);
            dbrole.Name = role.Name;
            dbrole.Description = role.Description;
            dbrole.LastModifiedBy = role.LastModifiedBy;
            dbrole.LastModifiedDate = role.LastModifiedDate;
            _vendorManagementDbContext.SaveChanges();

            return Result.Updated;
        }
        public ErrorOr<Dictionary<Guid, string>> GetDictionary()
        {
            _vendorDbOperator.InitializeOperator("vm_sp_GetAllRoles", CommandType.StoredProcedure, null);
            IDataReader dr = _queryExecutor.ExecuteReader();
            Dictionary<Guid, string> keyValues = new Dictionary<Guid, string>();
            while (dr.Read())
            {
                keyValues.Add(new Guid(dr["Guid"].ToString()), dr["Name"].ToString());
            }
            return keyValues;
        }
        public ErrorOr<RoleResponseRoot> GetAllRoles(int pageNo, int pageSize, string sortCol = "", string sortType = "")
        {
            RoleResponseRoot roleResponseRoot = new();
            var parameters = this.GetPaginationParameters(pageNo, pageSize, sortCol, sortType);
            _vendorDbOperator.InitializeOperator("vm_sp_GetRoles", CommandType.StoredProcedure, parameters);
            IDataReader dr = _queryExecutor.ExecuteReader();
            List<RoleResponse> roleResponses = new();
            while (dr.Read())
            {
                RoleResponse roleResponse =
                    new RoleResponse(
                          this.AgainstGUID(dr["Guid"]),
                          this.AgainstString(dr["Name"]),
                          this.AgainstString(dr["Description"]),
                          this.AgainstString(dr["CreatedBy"]),
                          this.AgainstDatetime(dr["CreatedDate"]),
                          this.AgainstString(dr["lastModifiedBy"]),
                          this.AgainstNullableDatetime(dr["lastModifiedDate"])
                        );
                roleResponseRoot.totalRows = this.AgainstInt(dr["TotalCount"]);
                roleResponses.Add(roleResponse);
            }
            roleResponseRoot.roleResponses = roleResponses;
            return roleResponseRoot;
        }
    }
}
