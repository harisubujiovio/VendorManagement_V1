using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using VendorManagement.Contracts;
using VendorManagement.DBclient.Models;
using VendorMangement.API.Services;
using static VendorManagement.Contracts.ServiceErrors.Errors;
using ContractStatus = VendorManagement.DBclient.Models.ContractStatus;

namespace VendorMangement.API.Controllers
{
    public class ContractStatusController : ApiController
    {
        public readonly IContractStatusService _contractStatusService;

        public ContractStatusController(IContractStatusService contractStatusService)
        {
            _contractStatusService = contractStatusService;
        }
        [HttpPost]
        public IActionResult CreateContractStatusService(ContractStatusRequest contractStatusRequest)
        {
            ErrorOr<ContractStatus> requestToContractStatusResult = ContractStatus.From(contractStatusRequest);
            if (requestToContractStatusResult.IsError)
            {
                return Problem(requestToContractStatusResult.Errors);
            }
            var contractStatus = requestToContractStatusResult.Value;
           
            ErrorOr<Created> createPartnerTypeResult = _contractStatusService.CreateContractStatus(contractStatus);
            return createPartnerTypeResult.Match(
                  created => Ok(MapContractStatusResponse(contractStatus)),
                  errors => Problem(errors)
                );
        }
        [HttpGet("{id:guid}")]
        public IActionResult GetContractStatus(Guid id)
        {
            ErrorOr<ContractStatus> getContractStatusResult = _contractStatusService.GetContractStatus(id);
            return getContractStatusResult.Match(
                  contractStatus => Ok(MapContractStatusResponse(contractStatus)),
                  errors => Problem(errors)
                );

        }
        [HttpGet()]
        [Route("GetDictionary")]
        public IActionResult GetDictionary()
        {
            ErrorOr<IEnumerable<ResourceDictionary>> getDictionaryResult = _contractStatusService.GetDictionary();
            return getDictionaryResult.Match(
                  contractStatus => Ok(contractStatus),
                  errors => Problem(errors)
                );
        }
        [HttpGet()]
        [Route("GetAll")]
        public IActionResult GetAll(int pageNo, int pageSize, string sortCol = "", string sortType = "")
        {
            ErrorOr<ContractStatusResponseRoot> getAllContractStatusMethodResult = _contractStatusService.GetAll(pageNo, pageSize, sortCol, sortType);
            return getAllContractStatusMethodResult.Match(
                  contractStatusResponses => Ok(contractStatusResponses),
                  errors => Problem(errors)
                );
        }
        [HttpPut("{id:guid}")]
        public IActionResult UpdateContractStatus(Guid id, ContractStatusRequest contractStatusRequest)
        {
            ErrorOr<ContractStatus> requestToContractStatusResult = ContractStatus.From(id, contractStatusRequest);

            if (requestToContractStatusResult.IsError)
            {
                return Problem(requestToContractStatusResult.Errors);
            }
            var contractStatus = requestToContractStatusResult.Value;
            ErrorOr<Updated> updateContractStatusResult = _contractStatusService.UpdateContractStatus(id, contractStatus);
            ContractStatusResponse contractStatusResponse = new ContractStatusResponse(
                             contractStatus.Guid,
                             contractStatus.Code,
                             contractStatus.Description,
                             contractStatus.CreatedBy,
                             contractStatus.CreatedDate,
                             contractStatus.LastModifiedBy,
                             contractStatus.LastModifiedDate
                           );

            if (updateContractStatusResult.IsError)
            {
                return Problem(updateContractStatusResult.Errors);
            }

            return CreatedAtAction(
                actionName: nameof(GetContractStatus),
                routeValues: new { id = contractStatus.Guid },
                value: contractStatusResponse
                );
        }
        [HttpDelete("{id:guid}")]
        public IActionResult DeleteContractStatus(Guid id)
        {
            _contractStatusService.DeleteContractStatus(id);
            return NoContent();
        }
        private static ContractStatusResponse MapContractStatusResponse(ContractStatus contractStatus)
        {
            return new ContractStatusResponse(
                 contractStatus.Guid,
                 contractStatus.Code,
                 contractStatus.Description,
                 contractStatus.CreatedBy,
                 contractStatus.CreatedDate,
                 contractStatus.LastModifiedBy,
                 contractStatus.LastModifiedDate
               );
        }
    }
}
