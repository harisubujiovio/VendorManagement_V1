using Microsoft.AspNetCore.Mvc;
using VendorManagement.Contracts;
using VendorMangement.API.Services;
using VendorMnagement.DBclient.Models;

namespace VendorMangement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractStatusController : Controller
    {
        public readonly IContractStatusService _contractStatusService;

        public ContractStatusController(IContractStatusService contractStatusService)
        {
            _contractStatusService = contractStatusService;
        }
        [HttpPost]
        public IActionResult CreateContractStatusService(ContractStatusRequest contractStatusRequest)
        {
            var contractStatus = new ContractStatus();
            contractStatus.Description = contractStatusRequest.Description;
            contractStatus.CreatedDate = DateTime.UtcNow;
            contractStatus.CreatedBy = "System";

            _contractStatusService.CreateContractStatus(contractStatus);

            ContractStatusResponse contractStatusResponse = new ContractStatusResponse(
                  contractStatus.Guid,
                  contractStatus.Description,
                  contractStatus.CreatedBy,
                  contractStatus.CreatedDate,
                  contractStatus.LastModifiedBy,
                  contractStatus.LastModifiedDate
                );

            return CreatedAtAction(
                actionName: nameof(GetContractStatus),
                routeValues: new { id = contractStatus.Guid },
                value: contractStatusResponse
                );
        }
        [HttpGet("{id:guid}")]
        public IActionResult GetContractStatus(Guid id)
        {
            ContractStatus contractStatus = _contractStatusService.GetContractStatus(id);
            ContractStatusResponse contractTypeResponse = new ContractStatusResponse(
                 contractStatus.Guid,
                 contractStatus.Description,
                 contractStatus.CreatedBy,
                 contractStatus.CreatedDate,
                 contractStatus.LastModifiedBy,
                 contractStatus.LastModifiedDate
               );
            return Ok(contractTypeResponse);
        }
        [HttpPut("{id:guid}")]
        public IActionResult UpdateContractStatus(Guid id, ContractStatusRequest contractStatusRequest)
        {
            var contractStatus = new ContractStatus();
            contractStatus.Guid = id;
            contractStatus.Description = contractStatusRequest.Description;
            contractStatus.LastModifiedDate = DateTime.UtcNow;
            contractStatus.LastModifiedBy = "System";

            _contractStatusService.UpdateContractStatus(id, contractStatus);

            ContractStatusResponse contractStatusResponse = new ContractStatusResponse(
                     contractStatus.Guid,
                     contractStatus.Description,
                     contractStatus.CreatedBy,
                     contractStatus.CreatedDate,
                     contractStatus.LastModifiedBy,
                     contractStatus.LastModifiedDate
                   );

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
    }
}
