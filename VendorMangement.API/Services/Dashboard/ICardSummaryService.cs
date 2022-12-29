using ErrorOr;
using VendorManagement.Contracts;

namespace VendorMangement.API.Services
{
    public interface ICardSummaryService
    {
        public ErrorOr<IEnumerable<CardSummaryResponse>> GetAll();
    }
}
