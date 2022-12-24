using ErrorOr;
using ErrorOr;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorManagement.Contracts;
using VendorManagement.Contracts.ServiceErrors;

namespace VendorManagement.DBclient.Models
{
    public class Statement : BaseEntity
    {
        public DateTime StatementDate { get; set; }

        public string StatementNo { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Status { get; set; }

        public Guid PartnerId { get; set; }

        public Guid ContractId { get; set; }

        [ForeignKey("PartnerId")]
        public Partner Partner { get; set; }

        [ForeignKey("ContractId")]
        public Contract Contract { get; set; }
        public static ErrorOr<Statement> From(StatementRequest statementRequest)
        {
            return Create(statementRequest);
        }
        public static ErrorOr<Statement> From(Guid Id, StatementRequest statementRequest)
        {
            return Update(statementRequest);
        }
        private static ErrorOr<Statement> Create(StatementRequest statementRequest)
        {
            List<Error> errors = Validate(statementRequest);

            if (errors.Count > 0)
                return errors;

            Statement statement = new Statement();
            statement.StartDate = statementRequest.StartDate;
            statement.StatementNo = statementRequest.StatementNo;
            statement.StatementDate = statementRequest.StatementDate;
            statement.ContractId = new Guid(statementRequest.ContractId);
            statement.PartnerId = new Guid(statementRequest.PartnerId);
            statement.Status = statementRequest.Status;
            statement.CreatedDate = DateTime.UtcNow;
            statement.CreatedBy = "System";
            return statement;
        }
        private static ErrorOr<Statement> Update(StatementRequest statementRequest)
        {
            List<Error> errors = Validate(statementRequest);

            if (errors.Count > 0)
                return errors;

            Statement statement = new Statement();
            statement.StartDate = statementRequest.StartDate;
            statement.StatementNo = statementRequest.StatementNo;
            statement.StatementDate = statementRequest.StatementDate;
            statement.ContractId = new Guid(statementRequest.ContractId);
            statement.PartnerId = new Guid(statementRequest.PartnerId);
            statement.Status = statementRequest.Status;
            statement.LastModifiedDate = DateTime.UtcNow;
            statement.LastModifiedBy = "System";
            return statement;
        }
        private static List<Error> Validate(StatementRequest statementRequest)
        {
            List<Error> errors = new();
            if (string.IsNullOrEmpty(statementRequest.StatementNo))
            {
                errors.Add(Errors.Statement.InvalidStatementNo);
            }
            if (string.IsNullOrEmpty(statementRequest.PartnerId))
            {
                errors.Add(Errors.Statement.InvalidPartnerId);
            }
            if (string.IsNullOrEmpty(statementRequest.ContractId))
            {
                errors.Add(Errors.Statement.InvalidContractId);
            }

            return errors;
        }
    }
}
