using ErrorOr;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorManagement.Contracts.ServiceErrors;
using VendorManagement.Contracts;

namespace VendorManagement.DBclient.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public class ContractStatus : BaseEntity
    {
        public const int MinDescriptionLength = 4;

        public const int MaxDescriptionLength = 150;

        public const int MinCodeLength = 4;
        public const int MaxCodeLength = 50;

        public string Code { get; set; }
        public string Description { get; set; }

        private ContractStatus()
        {

        }

        public static ErrorOr<ContractStatus> From(ContractStatusRequest contractStatusRequest)
        {
            return Create(contractStatusRequest.Code, contractStatusRequest.Description);
        }
        public static ErrorOr<ContractStatus> From(Guid Id, ContractStatusRequest contractStatusRequest)
        {
            return Update(Id, contractStatusRequest.Code, contractStatusRequest.Description);
        }
        private static ErrorOr<ContractStatus> Create(string code, string description)
        {
            List<Error> errors = Validate(code,description);

            if (errors.Count > 0)
                return errors;

            ContractStatus contractStatus = new ContractStatus();
            contractStatus.Code = code;
            contractStatus.Description = description;
            contractStatus.CreatedDate = DateTime.UtcNow;
            contractStatus.CreatedBy = "System";
            return contractStatus;
        }
        private static ErrorOr<ContractStatus> Update(Guid Id,string code, string description)
        {
            List<Error> errors = Validate(code, description);

            if (errors.Count > 0)
                return errors;

            ContractStatus contractStatus = new ContractStatus();
            contractStatus.Description = description;
            contractStatus.LastModifiedDate = DateTime.UtcNow;
            contractStatus.LastModifiedBy = "System";
            return contractStatus;
        }
        private static List<Error> Validate(string code, string description)
        {
            List<Error> errors = new();
            if (code.Length is < MinDescriptionLength or > MaxDescriptionLength)
            {
                errors.Add(Errors.ContractStatus.InvalidCode);
            }

            if (description.Length is < MinDescriptionLength or > MaxDescriptionLength)
            {
                errors.Add(Errors.ContractStatus.InvalidDescription);
            }

            return errors;
        }
    }
}
