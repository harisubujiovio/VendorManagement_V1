using Microsoft.AspNetCore.Mvc;
using VendorManagement.Contracts;
using VendorMangement.API.Services;
using VendorMnagement.DBclient.Models;

namespace VendorMangement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommissionMethodController : Controller
    {
        public readonly ICommissionMethodService _commissionMethodeService;

        public CommissionMethodController(ICommissionMethodService commissionMethodeService)
        {
            _commissionMethodeService = commissionMethodeService;
        }
        [HttpPost]
        public IActionResult CreateCommissionMethod(CommissionMethodRequest commissionMethodRequest)
        {
            var commissionMethod = new CommissionMethod();
            commissionMethod.Description = commissionMethodRequest.Description;
            commissionMethod.CreatedDate = DateTime.UtcNow;
            commissionMethod.CreatedBy = "System";

            _commissionMethodeService.CreateCommissionMethod(commissionMethod);

            CommissionMethodResponse commissionMethodResponse = new CommissionMethodResponse(
                  commissionMethod.Guid,
                  commissionMethod.Description,
                  commissionMethod.CreatedBy,
                  commissionMethod.CreatedDate,
                  commissionMethod.LastModifiedBy,
                  commissionMethod.LastModifiedDate
                );

            return CreatedAtAction(
                actionName: nameof(GetCommissionMethod),
                routeValues: new { id = commissionMethod.Guid },
                value: commissionMethodResponse
                );
        }
        [HttpGet("{id:guid}")]
        public IActionResult GetCommissionMethod(Guid id)
        {
            CommissionMethod commissionMethod = _commissionMethodeService.GetCommissionMethod(id);
            CommissionMethodResponse commissionMethodResponse = new CommissionMethodResponse(
                 commissionMethod.Guid,
                 commissionMethod.Description,
                 commissionMethod.CreatedBy,
                 commissionMethod.CreatedDate,
                 commissionMethod.LastModifiedBy,
                 commissionMethod.LastModifiedDate
               );
            return Ok(commissionMethodResponse);
        }
        [HttpPut("{id:guid}")]
        public IActionResult UpdateCommissionMethod(Guid id, CommissionMethodRequest commissionMethodRequest)
        {
            var commissionMethod = new CommissionMethod();
            commissionMethod.Guid = id;
            commissionMethod.Description = commissionMethodRequest.Description;
            commissionMethod.LastModifiedDate = DateTime.UtcNow;
            commissionMethod.LastModifiedBy = "System";

            _commissionMethodeService.UpdateCommissionMethod(id, commissionMethod);

            CommissionMethodResponse commissionMethodResponse = new CommissionMethodResponse(
                     commissionMethod.Guid,
                     commissionMethod.Description,
                     commissionMethod.CreatedBy,
                     commissionMethod.CreatedDate,
                     commissionMethod.LastModifiedBy,
                     commissionMethod.LastModifiedDate
                   );

            return CreatedAtAction(
              actionName: nameof(GetCommissionMethod),
              routeValues: new { id = commissionMethod.Guid },
              value: commissionMethodResponse
              );
        }
        [HttpDelete("{id:guid}")]
        public IActionResult DeleteCommissionMethod(Guid id)
        {
            _commissionMethodeService.DeleteCommissionMethod(id);
            return NoContent();
        }

    }
}
