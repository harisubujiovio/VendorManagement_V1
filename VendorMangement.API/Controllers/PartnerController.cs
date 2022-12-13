using Microsoft.AspNetCore.Mvc;
using VendorManagement.Contracts;
using VendorMangement.API.Services;
using VendorMnagement.DBclient.Models;

namespace VendorMangement.API.Controllers
{
    public class PartnerController : ApiController
    {
        public readonly IPartnerService _partnerService;

        public PartnerController(IPartnerService partnerService)
        {
            _partnerService = partnerService;
        }
        [HttpPost]
        public IActionResult CreatePartner(PartnerRequest partnerRequest)
        {
            var partner = new Partner();
            partner.PartnerNo = partnerRequest.PartnerNo;
            partner.PartnerName = partnerRequest.PartnerName;
            partner.Email = partnerRequest.Email;
            partner.MobileNumber = partnerRequest.MobileNumber;
            partner.PartnerTypeId = partnerRequest.PartnerTypeId;
            partner.PartnerNo = partnerRequest.PartnerNo;
            partner.CreatedDate = DateTime.UtcNow;
            partner.CreatedBy = "System";

            _partnerService.CreatePartner(partner);

            PartnerResponse partnerTypeResponse = new PartnerResponse(
                  partner.Guid,
                  partner.PartnerNo,
                  partner.PartnerName,
                  partner.Email,
                  partner.MobileNumber,
                  partner.PartnerTypeId,
                  partner.CreatedBy,
                  partner.CreatedDate,
                  partner.LastModifiedBy,
                  partner.LastModifiedDate
                );

            return CreatedAtAction(
                actionName: nameof(GetPartner),
                routeValues: new { id = partner.Guid },
                value: partnerTypeResponse
                );
        }
        [HttpGet("{id:guid}")]
        public IActionResult GetPartner(Guid id)
        {
            Partner partner = _partnerService.GetPartner(id);
            PartnerResponse partnerTypeResponse = new PartnerResponse(
                   partner.Guid,
                   partner.PartnerNo,
                   partner.PartnerName,
                   partner.Email,
                   partner.MobileNumber,
                   partner.PartnerTypeId,
                   partner.CreatedBy,
                   partner.CreatedDate,
                   partner.LastModifiedBy,
                   partner.LastModifiedDate
                 );
            return Ok(partnerTypeResponse);
        }
        [HttpPut("{id:guid}")]
        public IActionResult UpdatePartnerType(Guid id, PartnerRequest partnerRequest)
        {
            var partner = new Partner();
            partner.PartnerNo = partnerRequest.PartnerNo;
            partner.PartnerName = partnerRequest.PartnerName;
            partner.Email = partnerRequest.Email;
            partner.MobileNumber = partnerRequest.MobileNumber;
            partner.PartnerTypeId = partnerRequest.PartnerTypeId;
            partner.PartnerNo = partnerRequest.PartnerNo;
            partner.LastModifiedDate = DateTime.UtcNow;
            partner.LastModifiedBy = "System";

            _partnerService.UpdatePartner(id, partner);

            PartnerResponse partnerResponse = new PartnerResponse(
                   partner.Guid,
                   partner.PartnerNo,
                   partner.PartnerName,
                   partner.Email,
                   partner.MobileNumber,
                   partner.PartnerTypeId,
                   partner.CreatedBy,
                   partner.CreatedDate,
                   partner.LastModifiedBy,
                   partner.LastModifiedDate
                 );

            return CreatedAtAction(
              actionName: nameof(GetPartner),
              routeValues: new { id = partner.Guid },
              value: partnerResponse
              );
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeletePartner(Guid id)
        {
            _partnerService.DeletePartner(id);
            return NoContent();
        }
    }
}
