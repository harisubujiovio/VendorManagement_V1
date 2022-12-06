using Microsoft.AspNetCore.Mvc;
using VendorManagement.Contracts;
using VendorMangement.API.Services;
using VendorMnagement.DBclient.Models;

namespace VendorMangement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PartnerTypeController : ControllerBase
    {
        public readonly IPartnerTypeService _partnerTypeService;

        public PartnerTypeController(IPartnerTypeService partnerTypeService)
        {
            _partnerTypeService = partnerTypeService;
        }

        [HttpPost]
        public IActionResult CreatePartnerType(PartnerTypeRequest partnerTypeRequest)
        {
            var partnerType = new PartnerType();
            partnerType.Description = partnerTypeRequest.Description;
            partnerType.CreatedDate = DateTime.UtcNow;
            partnerType.CreatedBy = "System";

            _partnerTypeService.CreatePartnerType(partnerType);

            PartnerTypeResponse partnerTypeResponse = new PartnerTypeResponse(
                  partnerType.Guid,
                  partnerType.Description,
                  partnerType.CreatedBy,
                  partnerType.CreatedDate,
                  partnerType.LastModifiedBy,
                  partnerType.LastModifiedDate
                );

            return CreatedAtAction(
                actionName: nameof(GetPartnerType),
                routeValues: new {id = partnerType.Guid },
                value: partnerTypeResponse
                );
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetPartnerType(Guid id)
        {
            PartnerType partnerType = _partnerTypeService.GetPartnerType(id);
            PartnerTypeResponse partnerTypeResponse = new PartnerTypeResponse(
                 partnerType.Guid,
                 partnerType.Description,
                 partnerType.CreatedBy,
                 partnerType.CreatedDate,
                 partnerType.LastModifiedBy,
                 partnerType.LastModifiedDate
               );
            return Ok(partnerTypeResponse);
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdatePartnerType(Guid id, PartnerTypeRequest partnerTypeRequest)
        {
            var partnerType = new PartnerType();
            partnerType.Guid = id;
            partnerType.Description = partnerTypeRequest.Description;
            partnerType.LastModifiedDate = DateTime.UtcNow;
            partnerType.LastModifiedBy = "System";

            _partnerTypeService.UpdatePartnerType(id,partnerType);

            PartnerTypeResponse partnerTypeResponse = new PartnerTypeResponse(
                 partnerType.Guid,
                 partnerType.Description,
                 partnerType.CreatedBy,
                 partnerType.CreatedDate,
                 partnerType.LastModifiedBy,
                 partnerType.LastModifiedDate
               );

            return CreatedAtAction(
              actionName: nameof(GetPartnerType),
              routeValues: new { id = partnerType.Guid },
              value: partnerTypeResponse
              );
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeletePartnerType(Guid id)
        {
            _partnerTypeService.DeletePartnerType(id);
            return NoContent();
        }
    }
}
