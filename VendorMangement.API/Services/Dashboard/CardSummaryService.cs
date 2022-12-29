using ErrorOr;
using System.Data;
using VendorManagement.Contracts;
using VendorManagement.DBclient.DBProvider;
using VendorMnagement.DBclient.Data;

namespace VendorMangement.API.Services
{
    public class CardSummaryService : Base, ICardSummaryService
    {
        public readonly VendorManagementDbContext _vendorManagementDbContext;
        public readonly IVendorDbOperator _vendorDbOperator;
        public readonly IQueryExecutor _queryExecutor;
        public CardSummaryService(VendorManagementDbContext vendorManagementDbContext, IVendorDbOperator vendorDbOperator, IQueryExecutor queryExecutor)
        {
            _vendorManagementDbContext = vendorManagementDbContext;
            _vendorDbOperator = vendorDbOperator;
            _queryExecutor = queryExecutor;
        }
        public ErrorOr<IEnumerable<CardSummaryResponse>> GetAll()
        {
            _vendorDbOperator.InitializeOperator("vm_sp_CardSummaryData", CommandType.StoredProcedure, null);
            IDataReader dr = _queryExecutor.ExecuteReader();
            List<CardSummaryResponse> cardSummaryResponses = new();
            while (dr.Read())
            {
                CardSummaryResponse cardSummaryResponse =
                    new CardSummaryResponse(
                          this.AgainstInt(dr["data"]),
                          this.AgainstString(dr["label"]),
                          this.AgainstString(dr["name"]),
                          this.AgainstString(dr["filterKey"]),
                          this.AgainstString(dr["icon"])
                        );

                cardSummaryResponses.Add(cardSummaryResponse);
            }
            return cardSummaryResponses;
        }
    }
}
