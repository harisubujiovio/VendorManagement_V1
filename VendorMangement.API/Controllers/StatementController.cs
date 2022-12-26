using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using VendorManagement.Contracts;
using VendorManagement.DBclient.Models;
using VendorManagement.Contracts.ServiceErrors;
using VendorMangement.API.Services;

namespace VendorMangement.API.Controllers
{
    public class StatementController : ApiController
    {
        public readonly IStatementService _statementService;

        public StatementController(IStatementService statementService)
        {
            _statementService = statementService;
        }
        [HttpPost]
        public IActionResult CreateStatement(StatementRequest statementRequest)
        {
            ErrorOr<Statement> requestToStatementResult = Statement.From(statementRequest);
            if (requestToStatementResult.IsError)
            {
                return Problem(requestToStatementResult.Errors);
            }
            var statement = requestToStatementResult.Value;
            ErrorOr<Created> createStatementResult = _statementService.CreateStatement(statement);
            return createStatementResult.Match(
                  created => Ok(MapStatementResponse(statement)),
                  errors => Problem(errors)
                );
        }
        [HttpGet("{id:guid}")]
        public IActionResult GetStatement(Guid id)
        {
            ErrorOr<Statement> getStatementResult = _statementService.GetStatement(id);
            return getStatementResult.Match(
                  statement => Ok(MapStatementResponse(statement)),
                  errors => Problem(errors)
                );

        }
        [HttpGet()]
        [Route("GetDictionary")]
        public IActionResult GetDictionary()
        {
            ErrorOr<Dictionary<Guid, string>> getDictionaryResult = _statementService.GetDictionary();
            return getDictionaryResult.Match(
                  statement => Ok(statement),
                  errors => Problem(errors)
                );
        }
        [HttpGet()]
        [Route("GetAll")]
        public IActionResult GetAll(string partnerId, string contractId,
            int pageNo, int pageSize, string sortCol = "", string sortType = "")
        {
            ErrorOr<StatementResponseRoot> getAllStatementMethodResult = _statementService.GetAll(partnerId, contractId, pageNo, pageSize, sortCol, sortType);
            return getAllStatementMethodResult.Match(
                  statementsResponses => Ok(statementsResponses),
                  errors => Problem(errors)
                );
        }
        [HttpPut("{id:guid}")]
        public IActionResult UpdateStatement(Guid id, StatementRequest statementRequest)
        {
            ErrorOr<Statement> requestToStatementResult = Statement.From(id, statementRequest);

            if (requestToStatementResult.IsError)
            {
                return Problem(requestToStatementResult.Errors);
            }
            var statement = requestToStatementResult.Value;

            ErrorOr<Updated> updateStatementResult = _statementService.UpdateStatement(id, statement);

            StatementResponse statementResponse = MapStatementResponse(statement);

            if (updateStatementResult.IsError)
            {
                return Problem(updateStatementResult.Errors);
            }

            return CreatedAtAction(
                actionName: nameof(GetStatement),
                routeValues: new { id = statement.Guid },
                value: statementResponse
                );

        }
        [HttpDelete("{id:guid}")]
        public IActionResult DeleteStatement(Guid id)
        {
            ErrorOr<Deleted> deleteStatementResult = _statementService.DeleteStatement(id);
            return deleteStatementResult.Match(deleted => NoContent(),
                errors => Problem(errors));
        }
        private static StatementResponse MapStatementResponse(Statement statement)
        {
            return new StatementResponse(
                 statement.Guid,
                 statement.StatementDate,
                 statement.StatementNo,
                 statement.StartDate,
                 statement.EndDate,
                 statement.Status,
                 statement.PartnerId.ToString(),
                 statement.ContractId.ToString(),
                 statement.CreatedBy,
                 statement.CreatedDate,
                 statement.LastModifiedBy,
                 statement.LastModifiedDate
               );
        }
    }
}
