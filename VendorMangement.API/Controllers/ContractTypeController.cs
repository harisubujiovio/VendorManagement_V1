using Microsoft.AspNetCore.Mvc;
using VendorManagement.Contracts;
using VendorMangement.API.Services;
using VendorMnagement.DBclient.Models;

namespace VendorMangement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractTypeController : Controller
    {
        public readonly IContractTypeService _contractTypeService;

        public ContractTypeController(IContractTypeService contractTypeService)
        {
            _contractTypeService = contractTypeService;
        }
        [HttpPost]
        public IActionResult CreateContractTypeService(ContractTypeRequest contractTypeRequest)
        {
            var contractType = new ContractType();
            contractType.Description = contractTypeRequest.Description;
            contractType.CreatedDate = DateTime.UtcNow;
            contractType.CreatedBy = "System";

            _contractTypeService.CreateContractType(contractType);

            ContractTypeResponse contractTypeResponse = new ContractTypeResponse(
                  contractType.Guid,
                  contractType.Description,
                  contractType.CreatedBy,
                  contractType.CreatedDate,
                  contractType.LastModifiedBy,
                  contractType.LastModifiedDate
                );

            return CreatedAtAction(
                actionName: nameof(GetContractType),
                routeValues: new { id = contractType.Guid },
                value: contractTypeResponse
                );
        }
        [HttpGet("{id:guid}")]
        public IActionResult GetContractType(Guid id)
        {
            ContractType contractType = _contractTypeService.GetContractType(id);
            ContractTypeResponse contractTypeResponse = new ContractTypeResponse(
                 contractType.Guid,
                 contractType.Description,
                 contractType.CreatedBy,
                 contractType.CreatedDate,
                 contractType.LastModifiedBy,
                 contractType.LastModifiedDate
               );
            return Ok(contractTypeResponse);
        }
        [HttpPut("{id:guid}")]
        public IActionResult UpdateContractType(Guid id, ContractTypeRequest contractTypeRequest)
        {
            var contractType = new ContractType();
            contractType.Guid = id;
            contractType.Description = contractTypeRequest.Description;
            contractType.LastModifiedDate = DateTime.UtcNow;
            contractType.LastModifiedBy = "System";

            _contractTypeService.UpdateContractType(id, contractType);

            ContractTypeResponse contractTypeResponse = new ContractTypeResponse(
                     contractType.Guid,
                     contractType.Description,
                     contractType.CreatedBy,
                     contractType.CreatedDate,
                     contractType.LastModifiedBy,
                     contractType.LastModifiedDate
                   );

            return CreatedAtAction(
              actionName: nameof(GetContractType),
              routeValues: new { id = contractType.Guid },
              value: contractTypeResponse
              );
        }
        [HttpDelete("{id:guid}")]
        public IActionResult DeleteContractType(Guid id)
        {
            _contractTypeService.DeleteContractType(id);
            return NoContent();
        }
    }
}
