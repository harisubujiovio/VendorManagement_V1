using ErrorOr;
using VendorManagement.Contracts;
using VendorManagement.DBclient.Models;

namespace VendorMangement.API.Services
{
    public interface ISalesSevice
    {
        ErrorOr<Created> CreateSales(Sales sales);

        ErrorOr<Sales> GetSales(Guid id);

        ErrorOr<Updated> UpdateSales(Guid id, Sales sales);

        ErrorOr<Deleted> DeleteSales(Guid id);
        ErrorOr<Dictionary<Guid, string>> GetDictionary();

        ErrorOr<SalesResponseRoot> GetAll(string partnerId,int pageNo, int pageSize, string sortCol = "", string sortType = "", string filterKey = "");
    }
}
