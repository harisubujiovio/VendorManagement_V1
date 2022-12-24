using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using VendorManagement.Contracts;
using VendorManagement.DBclient.Models;
using VendorMangement.API.Services;

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
            ErrorOr<Partner> requestToPartnerResult = Partner.From(partnerRequest);
            if (requestToPartnerResult.IsError)
            {
                return Problem(requestToPartnerResult.Errors);
            }
            var partner = requestToPartnerResult.Value;
            ErrorOr<Created> createPartnerResult = _partnerService.CreatePartner(partner);
            return createPartnerResult.Match(
                  created => Ok(MapPartnerResponse(partner)),
                  errors => Problem(errors)
                );

          
        }
        [HttpGet("{id:guid}")]
        public IActionResult GetPartner(Guid id)
        {
            ErrorOr<Partner> getPartnerResult = _partnerService.GetPartner(id);

            return getPartnerResult.Match(
                  partner => Ok(MapPartnerResponse(partner)),
                  errors => Problem(errors)
                );
            
        }
        [HttpGet()]
        [Route("GetDictionary")]
        public IActionResult GetDictionary()
        {
            ErrorOr<Dictionary<Guid, string>> getDictionaryResult = _partnerService.GetDictionary();
            return getDictionaryResult.Match(
                  commissionMethod => Ok(commissionMethod),
                  errors => Problem(errors)
                );
        }
        [HttpGet()]
        [Route("GetAllPartnerTypes")]
        public IActionResult GetAllPartnerTypes(int pageNo, int pageSize, string sortCol = "", string sortType = "")
        {
            ErrorOr<IEnumerable<PartnerResponse>> getAllPartnerMethodResult = _partnerService.GetAllPartners(pageNo, pageSize, sortCol, sortType);
            return getAllPartnerMethodResult.Match(
                  partnerResponses => Ok(partnerResponses),
                  errors => Problem(errors)
                );
        }

        [HttpPut("{id:guid}")]
        public IActionResult UpdatePartnerType(Guid id, PartnerRequest partnerRequest)
        {
            ErrorOr<Partner> requestToPartnerResult = Partner.From(id, partnerRequest);

            if (requestToPartnerResult.IsError)
            {
                return Problem(requestToPartnerResult.Errors);
            }
            var partner = requestToPartnerResult.Value;

            ErrorOr<Updated> updatePartnerResult = _partnerService.UpdatePartner(id, partner);

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

            if (updatePartnerResult.IsError)
            {
                return Problem(updatePartnerResult.Errors);
            }

            return CreatedAtAction(
                actionName: nameof(GetPartner),
                routeValues: new { id = partner.Guid },
                value: partnerResponse
                );
        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeletePartner(Guid id)
        {
            ErrorOr<Deleted> deletePartnerResult = _partnerService.DeletePartner(id);
            return deletePartnerResult.Match(deleted => NoContent(),
                errors => Problem(errors));
        }

        private static PartnerResponse MapPartnerResponse(Partner partner)
        {
            return new PartnerResponse(
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
        }
    }
}
