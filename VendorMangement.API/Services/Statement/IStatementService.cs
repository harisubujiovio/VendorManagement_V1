using ErrorOr;
using VendorManagement.Contracts;
using VendorManagement.DBclient.Models;

namespace VendorMangement.API.Services
{
    public interface IStatementService
    {
        ErrorOr<Created> CreateStatement(Statement statement);

        ErrorOr<Statement> GetStatement(Guid id);

        ErrorOr<Updated> UpdateStatement(Guid id, Statement statement);

        ErrorOr<Deleted> DeleteStatement(Guid id);
        ErrorOr<IEnumerable<ResourceDictionary>> GetDictionary();

        ErrorOr<StatementResponseRoot> GetAll(string partnerId,string contractId, int pageNo, int pageSize, string sortCol = "", string sortType = "");
    }
}
