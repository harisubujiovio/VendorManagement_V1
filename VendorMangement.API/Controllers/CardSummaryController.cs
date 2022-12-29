using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using VendorManagement.Contracts;
using VendorMangement.API.Services;

namespace VendorMangement.API.Controllers
{
    public class CardSummaryController : ApiController
    {
        public readonly ICardSummaryService _cardSummaryService;

        public CardSummaryController(ICardSummaryService cardSummaryService)
        {
            _cardSummaryService = cardSummaryService;
        }

        [HttpGet()]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            ErrorOr<IEnumerable<CardSummaryResponse>> getAllCardSummaryMethodResult = _cardSummaryService.GetAll();
            return getAllCardSummaryMethodResult.Match(
                  cardSummaryResponses => Ok(cardSummaryResponses),
                  errors => Problem(errors)
                );
        }
    }
}
