using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using VendorManagement.Contracts;
using VendorManagement.DBclient.Models;
using VendorManagement.Contracts.ServiceErrors;
using VendorMangement.API.Services;

namespace VendorMangement.API.Controllers
{
    public class ContractController : ApiController
    {
        public readonly IContractService _contractService;

        public ContractController(IContractService contractService)
        {
            _contractService = contractService;
        }
        [HttpPost]
        public IActionResult CreateContract(ContractRequest contractRequest)
        {
            ErrorOr<Contract> requestToContractResult = Contract.From(contractRequest);
            if (requestToContractResult.IsError)
            {
                return Problem(requestToContractResult.Errors);
            }
            var contract = requestToContractResult.Value;
            ErrorOr<Created> createContractResult = _contractService.CreateContract(contract);
            return createContractResult.Match(
                  created => Ok(MapContractResponse(contract)),
                  errors => Problem(errors)
                );
        }
        [HttpGet("{id:guid}")]
        public IActionResult GetContract(Guid id)
        {
            ErrorOr<Contract> getContractResult = _contractService.GetContract(id);

            return getContractResult.Match(
                  contract => Ok(MapContractResponse(contract)),
                  errors => Problem(errors)
                );

        }
        [HttpGet()]
        [Route("GetDictionary")]
        public IActionResult GetDictionary()
        {
            ErrorOr<IEnumerable<ResourceDictionary>> getDictionaryResult = _contractService.GetDictionary();
            return getDictionaryResult.Match(
                  contract => Ok(contract),
                  errors => Problem(errors)
                );
        }
        [HttpGet()]
        [Route("GetAll")]
        public IActionResult GetAll(string partnerId,
            string contractTypeId, string commissionMethodId, string contractStatusId,
            int pageNo, int pageSize, string sortCol = "", string sortType = "")
        {
            ErrorOr<ContractResponseRoot> getAllContractMethodResult = _contractService.GetAll(partnerId, contractTypeId, commissionMethodId, contractStatusId, pageNo, pageSize, sortCol, sortType);
            return getAllContractMethodResult.Match(
                  contractsResponses => Ok(contractsResponses),
                  errors => Problem(errors)
                );
        }
        [HttpPut("{id:guid}")]
        public IActionResult UpdateSales(Guid id, ContractRequest contractRequest)
        {
            ErrorOr<Contract> requestToContractResult = Contract.From(id, contractRequest);

            if (requestToContractResult.IsError)
            {
                return Problem(requestToContractResult.Errors);
            }
            var contract = requestToContractResult.Value;

            ErrorOr<Updated> updateContractResult = _contractService.UpdateContract(id, contract);

            ContractResponse contractResponse = MapContractResponse(contract);

            if (updateContractResult.IsError)
            {
                return Problem(updateContractResult.Errors);
            }

            return CreatedAtAction(
                actionName: nameof(GetContract),
                routeValues: new { id = contract.Guid },
                value: contractResponse
                );

        }
        [HttpDelete("{id:guid}")]
        public IActionResult DeleteContract(Guid id)
        {
            ErrorOr<Deleted> deleteContractResult = _contractService.DeleteContract(id);
            return deleteContractResult.Match(deleted => NoContent(),
                errors => Problem(errors));
        }
        private static ContractResponse MapContractResponse(Contract contract)
        {
            return new ContractResponse(
                 contract.Guid,
                 contract.ContractNo,
                 contract.ContractTypeId.ToString(),
                 "",
                 contract.ContractDate,
                 contract.StartDate,
                 contract.EndDate,
                 contract.RenewalDate,
                 contract.CommissionMethodId.ToString(),
                 "",
                 contract.ContractStatusId.ToString(),
                 "",
                 contract.PartnerId.ToString(),
                 "",
                 contract.CreatedBy,
                 contract.CreatedDate,
                 contract.LastModifiedBy,
                 contract.LastModifiedDate
               );
        }
    }
}
