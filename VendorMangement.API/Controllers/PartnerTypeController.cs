using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using VendorManagement.Contracts;
using VendorManagement.DBclient.Models;
using VendorManagement.Contracts.ServiceErrors;
using VendorMangement.API.Services;

namespace VendorMangement.API.Controllers
{
    public class PartnerTypeController : ApiController
    {
        public readonly IPartnerTypeService _partnerTypeService;

        public PartnerTypeController(IPartnerTypeService partnerTypeService)
        {
            _partnerTypeService = partnerTypeService;
        }

        [HttpPost]
        public IActionResult CreatePartnerType(PartnerTypeRequest partnerTypeRequest)
        {
            ErrorOr<PartnerType> requestToPartnerTypeResult = PartnerType.From(partnerTypeRequest);
            if(requestToPartnerTypeResult.IsError)
            {
                return Problem(requestToPartnerTypeResult.Errors);
            }
            var partnerType = requestToPartnerTypeResult.Value;
            ErrorOr<Created> createPartnerTypeResult = _partnerTypeService.CreatePartnerType(partnerType);
            return createPartnerTypeResult.Match(
                  created => Ok(MapPartnerTypeResponse(partnerType)),
                  errors => Problem(errors)
                );
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetPartnerType(Guid id)
        {
            ErrorOr<PartnerType> getPartnerTypeResult = _partnerTypeService.GetPartnerType(id);

            return getPartnerTypeResult.Match(
                  partnerType => Ok(MapPartnerTypeResponse(partnerType)),
                  errors => Problem(errors)
                );
            
        }
        [HttpGet()]
        [Route("GetDictionary")]
        public IActionResult GetDictionary()
        {
            ErrorOr<Dictionary<Guid, string>> getDictionaryResult = _partnerTypeService.GetDictionary();
            return getDictionaryResult.Match(
                  commissionMethod => Ok(commissionMethod),
                  errors => Problem(errors)
                );
        }
        [HttpGet()]
        [Route("GetAllPartnerTypes")]
        public IActionResult GetAllPartnerTypes(int pageNo, int pageSize, string sortCol = "", string sortType = "")
        {
            ErrorOr<IEnumerable<PartnerTypeResponse>> getAllPartnerTypeMethodResult = _partnerTypeService.GetAllPartnerTypes(pageNo, pageSize, sortCol, sortType);
            return getAllPartnerTypeMethodResult.Match(
                  partnerTypeResponses => Ok(partnerTypeResponses),
                  errors => Problem(errors)
                );
        }
        [HttpPut("{id:guid}")]
        public IActionResult UpdatePartnerType(Guid id, PartnerTypeRequest partnerTypeRequest)
        {
            ErrorOr<PartnerType> requestToPartnerTypeResult = PartnerType.From(id,partnerTypeRequest);
            
            if(requestToPartnerTypeResult.IsError)
            {
                return Problem(requestToPartnerTypeResult.Errors);
            }
            var partnerType = requestToPartnerTypeResult.Value;
          
            ErrorOr<Updated> updatePartnerTypeResult = _partnerTypeService.UpdatePartnerType(id,partnerType);

            PartnerTypeResponse partnerTypeResponse = new PartnerTypeResponse(
                 partnerType.Guid,
                 partnerType.Description,
                 partnerType.CreatedBy,
                 partnerType.CreatedDate,
                 partnerType.LastModifiedBy,
                 partnerType.LastModifiedDate
               );

            if (updatePartnerTypeResult.IsError)
            {
                return Problem(updatePartnerTypeResult.Errors);
            }

            return CreatedAtAction(
                actionName: nameof(GetPartnerType),
                routeValues: new { id = partnerType.Guid },
                value: partnerTypeResponse
                );

        }

        [HttpDelete("{id:guid}")]
        public IActionResult DeletePartnerType(Guid id)
        {
            ErrorOr<Deleted> deletePartnerTypeResult = _partnerTypeService.DeletePartnerType(id);
            return deletePartnerTypeResult.Match(deleted => NoContent(),
                errors => Problem(errors));
        }

        private static PartnerTypeResponse MapPartnerTypeResponse(PartnerType partnerType)
        {
            return new PartnerTypeResponse(
                 partnerType.Guid,
                 partnerType.Description,
                 partnerType.CreatedBy,
                 partnerType.CreatedDate,
                 partnerType.LastModifiedBy,
                 partnerType.LastModifiedDate
               );
        }
    }
}
