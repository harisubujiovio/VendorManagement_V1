using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VendorManagement.Contracts;
using VendorManagement.DBclient.Models;
using VendorMangement.API.Services;
using CommissionMethod = VendorManagement.DBclient.Models.CommissionMethod;

namespace VendorMangement.API.Controllers
{
    public class CommissionMethodController : ApiController
    {
        public readonly ICommissionMethodService _commissionMethodeService;
       
        public CommissionMethodController(ICommissionMethodService commissionMethodeService)
        {
            _commissionMethodeService = commissionMethodeService;
        }
        [HttpPost]
        public IActionResult CreateCommissionMethod(CommissionMethodRequest commissionMethodRequest)
        {
            ErrorOr<CommissionMethod> requestToCommissionMethodResult = CommissionMethod.From(commissionMethodRequest);
            if (requestToCommissionMethodResult.IsError)
            {
                return Problem(requestToCommissionMethodResult.Errors);
            }
            var commissionMethod = requestToCommissionMethodResult.Value;
            ErrorOr<Created> createPartnerTypeResult = _commissionMethodeService.CreateCommissionMethod(commissionMethod);
            return createPartnerTypeResult.Match(
                  created => Ok(MapCommissionMethodResponse(commissionMethod)),
                  errors => Problem(errors)
                );
        }
        [HttpGet("{id:guid}")]
        public IActionResult GetCommissionMethod(Guid id)
        {
            ErrorOr<CommissionMethod> getCommissionMethodResult =  _commissionMethodeService.GetCommissionMethod(id);
            return getCommissionMethodResult.Match(
                  commissionMethod => Ok(MapCommissionMethodResponse(commissionMethod)),
                  errors => Problem(errors)
                );

           
        }
        [HttpGet()]
        [Route("GetDictionary")]
        public IActionResult GetDictionary()
        {
            ErrorOr<Dictionary<Guid, string>> getDictionaryResult = _commissionMethodeService.GetDictionary();
            return getDictionaryResult.Match(
                  commissionMethod => Ok(commissionMethod),
                  errors => Problem(errors)
                );
        }
        [HttpGet()]
        [Route("GetAllCommissionMethods")]
        public IActionResult GetAllCommissionMethods(int pageNo, int pageSize, string sortCol = "", string sortType = "")
        {
            ErrorOr<CommissionMethodResponseRoot> getAllCommissionMethodResult  = _commissionMethodeService.GetAllCommissionMethods(pageNo, pageSize, sortCol, sortType);
            return getAllCommissionMethodResult.Match(
                  commissionMethodResponses => Ok(commissionMethodResponses),
                  errors => Problem(errors)
                );
        }
        [HttpPut("{id:guid}")]
        public IActionResult UpdateCommissionMethod(Guid id, CommissionMethodRequest commissionMethodRequest)
        {
            ErrorOr<CommissionMethod> requestToCommissionMethodResult = CommissionMethod.From(id, commissionMethodRequest);

            if (requestToCommissionMethodResult.IsError)
            {
                return Problem(requestToCommissionMethodResult.Errors);
            }
            var commissionMethod = requestToCommissionMethodResult.Value;

            ErrorOr<Updated> updateCommissionMethodResult = _commissionMethodeService.UpdateCommissionMethod(id, commissionMethod);
            if (updateCommissionMethodResult.IsError)
            {
                return Problem(updateCommissionMethodResult.Errors);
            }

            CommissionMethodResponse commissionMethodResponse = new CommissionMethodResponse(
                commissionMethod.Guid,
                commissionMethod.Code,
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
            ErrorOr<Deleted> deleteCommissionMethodResult = _commissionMethodeService.DeleteCommissionMethod(id);
            return deleteCommissionMethodResult.Match(deleted => NoContent(),
                errors => Problem(errors));
        }

        private static CommissionMethodResponse MapCommissionMethodResponse(CommissionMethod commissionMethod)
        {
            return new CommissionMethodResponse(
                 commissionMethod.Guid,
                 commissionMethod.Code,
                 commissionMethod.Description,
                 commissionMethod.CreatedBy,
                 commissionMethod.CreatedDate,
                 commissionMethod.LastModifiedBy,
                 commissionMethod.LastModifiedDate
               );
        }
        //private static IEnumerable<CommissionMethodResponse> MapCommissionMethodResponse(IEnumerable<CommissionMethod> commissionMethods)
        //{
        //    foreach(CommissionMethod commissionMethod in commissionMethods)
        //    {
        //        yield return MapCommissionMethodResponse(commissionMethod);
        //    } 
        //}

    }
}
