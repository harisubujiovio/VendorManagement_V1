using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using VendorManagement.Contracts;
using VendorManagement.DBclient.Models;
using VendorMangement.API.Services;

namespace VendorMangement.API.Controllers
{
    public class ContractTypeController : ApiController
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
                  contractType.Code,
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
                  contractType.Code,
                 contractType.Description,
                 contractType.CreatedBy,
                 contractType.CreatedDate,
                 contractType.LastModifiedBy,
                 contractType.LastModifiedDate
               );
            return Ok(contractTypeResponse);
        }
        [HttpGet()]
        [Route("GetDictionary")]
        public IActionResult GetDictionary()
        {
            ErrorOr<Dictionary<Guid, string>> getDictionaryResult = _contractTypeService.GetDictionary();
            return getDictionaryResult.Match(
                  contractType => Ok(contractType),
                  errors => Problem(errors)
                );
        }
        [HttpGet()]
        [Route("GetAllContractTypes")]
        public IActionResult GetAllContractTypes(int pageNo, int pageSize, string sortCol = "", string sortType = "")
        {
            ErrorOr<IEnumerable<ContractTypeResponse>> getAllContractTypeMethodResult = _contractTypeService.GetAllContractTypes(pageNo, pageSize, sortCol, sortType);
            return getAllContractTypeMethodResult.Match(
                  contractStatusResponses => Ok(contractStatusResponses),
                  errors => Problem(errors)
                );
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
                     contractType.Code,
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
