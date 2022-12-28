using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using VendorManagement.Contracts;
using VendorManagement.DBclient.Models;
using VendorManagement.Contracts.ServiceErrors;
using VendorMangement.API.Services;

namespace VendorMangement.API.Controllers
{
    public class SalesController : ApiController
    {
        public readonly ISalesSevice _salesSevice;

        public SalesController(ISalesSevice salesSevice)
        {
            _salesSevice = salesSevice;
        }
        [HttpPost]
        public IActionResult CreateSales(SalesRequest salesRequest)
        {
            ErrorOr<Sales> requestToSalesResult = Sales.From(salesRequest);
            if (requestToSalesResult.IsError)
            {
                return Problem(requestToSalesResult.Errors);
            }
            var sales = requestToSalesResult.Value;
            ErrorOr<Created> createSalesResult = _salesSevice.CreateSales(sales);
            return createSalesResult.Match(
                  created => Ok(MapSalesResponse(sales)),
                  errors => Problem(errors)
                );
        }
        [HttpGet("{id:guid}")]
        public IActionResult GetSales(Guid id)
        {
            ErrorOr<Sales> getSalesResult = _salesSevice.GetSales(id);

            return getSalesResult.Match(
                  sales => Ok(MapSalesResponse(sales)),
                  errors => Problem(errors)
                );

        }
        [HttpGet()]
        [Route("GetDictionary")]
        public IActionResult GetDictionary()
        {
            ErrorOr<Dictionary<Guid, string>> getDictionaryResult = _salesSevice.GetDictionary();
            return getDictionaryResult.Match(
                  salesMethod => Ok(salesMethod),
                  errors => Problem(errors)
                );
        }
        [HttpGet()]
        [Route("GetAll")]
        public IActionResult GetAll(string partnerId,int pageNo, int pageSize, string sortCol = "", string sortType = "")
        {
            ErrorOr<SalesResponseRoot> getAllSalesMethodResult = _salesSevice.GetAll(partnerId, pageNo, pageSize, sortCol, sortType);
            return getAllSalesMethodResult.Match(
                  salesResponses => Ok(salesResponses),
                  errors => Problem(errors)
                );
        }
        [HttpPut("{id:guid}")]
        public IActionResult UpdateSales(Guid id, SalesRequest salesRequest)
        {
            ErrorOr<Sales> requestToSalesResult = Sales.From(id, salesRequest);

            if (requestToSalesResult.IsError)
            {
                return Problem(requestToSalesResult.Errors);
            }
            var sales = requestToSalesResult.Value;

            ErrorOr<Updated> updateSalesResult = _salesSevice.UpdateSales(id, sales);

            SalesResponse salesResponse = MapSalesResponse(sales);

            if (updateSalesResult.IsError)
            {
                return Problem(updateSalesResult.Errors);
            }

            return CreatedAtAction(
                actionName: nameof(GetSales),
                routeValues: new { id = sales.Guid },
                value: salesResponse
                );

        }
        [HttpDelete("{id:guid}")]
        public IActionResult DeleteSales(Guid id)
        {
            ErrorOr<Deleted> deleteSalesResult = _salesSevice.DeleteSales(id);
            return deleteSalesResult.Match(deleted => NoContent(),
                errors => Problem(errors));
        }
        private static SalesResponse MapSalesResponse(Sales sales)
        {
            return new SalesResponse(
                 sales.Guid,
                 sales.EntryNo,
                 sales.Source,
                 sales.PartnerId.ToString(),
                 "",
                 sales.DocumentType,
                 sales.Code,
                 sales.DocumentLineNo,
                 sales.Date,
                 sales.No,
                 sales.Quantity,
                 sales.UOM,
                 sales.UnitPrice,
                 sales.NetAmount,
                 sales.GST,
                 sales.Discount,
                 sales.CardPaidAmount,
                 sales.LoyaltyPoints,
                 sales.PromotionTxrn,
                 sales.CostShareOnDiscountAmount,
                 sales.LoyaltyShareAmount,
                 sales.CommissionValue,
                 sales.PromoCommissionValue,
                 sales.CommissionAmount,
                 sales.CostShareAmount,
                 sales.CreatedBy,
                 sales.CreatedDate,
                 sales.LastModifiedBy,
                 sales.LastModifiedDate
               );
        }
    }
}
