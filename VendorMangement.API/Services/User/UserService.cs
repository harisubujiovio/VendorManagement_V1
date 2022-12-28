using ErrorOr;
using VendorManagement.DBclient.DBProvider;
using VendorManagement.DBclient.Models;
using VendorMnagement.DBclient.Data;
using VendorManagement.Contracts.ServiceErrors;
using System.Data;
using VendorManagement.Contracts;


namespace VendorMangement.API.Services
{
    public class UserService : Base, IUserService
    {
        public readonly VendorManagementDbContext _vendorManagementDbContext;
        public readonly IVendorDbOperator _vendorDbOperator;
        public readonly IQueryExecutor _queryExecutor;
        public UserService(VendorManagementDbContext vendorManagementDbContext, IVendorDbOperator vendorDbOperator, IQueryExecutor queryExecutor)
        {
            _vendorManagementDbContext = vendorManagementDbContext;
            _vendorDbOperator = vendorDbOperator;
            _queryExecutor = queryExecutor;
        }
        public ErrorOr<Deleted> DeleteUser(Guid id)
        {
            var user = _vendorManagementDbContext.Users.Find(id);
            _vendorManagementDbContext.Users.Remove(user);
            _vendorManagementDbContext.SaveChanges();
            return Result.Deleted;
        }

        public ErrorOr<UserResponseRoot> GetAll(int pageNo, int pageSize, string sortCol = "", string sortType = "")
        {
            UserResponseRoot userResponseRoot = new();
            var parameters = this.GetPaginationParameters(pageNo, pageSize, sortCol, sortType);
            
            _vendorDbOperator.InitializeOperator("vm_sp_GetUsers", CommandType.StoredProcedure, parameters);
            IDataReader dr = _queryExecutor.ExecuteReader();
            List<UserResponse> userResponses = new();
            while (dr.Read())
            {
                UserResponse userResponse =
                    new UserResponse(
                          this.AgainstGUID(dr["Guid"]),
                          this.AgainstString(dr["firstName"]),
                          this.AgainstString(dr["lastName"]),
                          this.AgainstString(dr["email"]),
                          this.AgainstString(dr["MobileNumber"]),
                          this.AgainstString(dr["Address"]),
                          this.AgainstString(dr["name"]),
                          this.AgainstString(dr["CreatedBy"]),
                          this.AgainstDatetime(dr["CreatedDate"]),
                          this.AgainstString(dr["lastModifiedBy"]),
                          this.AgainstNullableDatetime(dr["lastModifiedDate"])

                        );
                userResponseRoot.totalRows = this.AgainstInt(dr["TotalCount"]);
                userResponses.Add(userResponse);
            }
            userResponseRoot.responses = userResponses;
            return userResponseRoot;
        }

        public ErrorOr<IEnumerable<ResourceDictionary>> GetDictionary()
        {
            List<ResourceDictionary> resourceDictionaries = new();
            ResourceDictionary resourceDictionary = null;
            _vendorDbOperator.InitializeOperator("vm_sp_GetAllUsers", CommandType.StoredProcedure, null);
            IDataReader dr = _queryExecutor.ExecuteReader();
            while (dr.Read())
            {
                resourceDictionary = new(new Guid(dr["Guid"].ToString()), dr["Displayname"].ToString());
                resourceDictionaries.Add(resourceDictionary);
            }
            return resourceDictionaries;
        }

        public ErrorOr<User> GetUser(Guid id)
        {
            User user = _vendorManagementDbContext.Users.Find(id);
            if (user != null)
                return user;

            return Errors.User.NotFound;
        }

        public ErrorOr<Updated> UpdateUser(Guid id, User user)
        {
            var dbUser = _vendorManagementDbContext.Users.Find(id);
            dbUser.firstName = user.firstName;
            dbUser.lastName = user.lastName;
            dbUser.email = user.email;
            dbUser.MobileNumber = user.MobileNumber;
            dbUser.Address = user.Address;
            dbUser.LastModifiedBy = user.LastModifiedBy;
            dbUser.LastModifiedDate = user.LastModifiedDate;
            _vendorManagementDbContext.SaveChanges();

            return Result.Updated;
        }
    }
}
