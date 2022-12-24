using ErrorOr;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using VendorManagement.Contracts;
using VendorManagement.Contracts.ServiceErrors;

namespace VendorManagement.DBclient.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public class ContractType : BaseEntity
    {
        public string Code { get; set; }
        public string Description { get; set; }

        public const int MinDescriptionLength = 4;

        public const int MaxDescriptionLength = 150;

        public const int MinCodeLength = 4;
        public const int MaxCodeLength = 50;

        public static ErrorOr<ContractType> From(ContractTypeRequest contractTypeRequest)
        {
            return Create(contractTypeRequest.Code, contractTypeRequest.Description);
        }
        public static ErrorOr<ContractType> From(Guid Id, ContractTypeRequest contractTypeRequest)
        {
            return Update(Id, contractTypeRequest.Code, contractTypeRequest.Description);
        }
        private static ErrorOr<ContractType> Create(string code, string description)
        {
            List<Error> errors = Validate(code, description);

            if (errors.Count > 0)
                return errors;

            ContractType contractType = new ContractType();
            contractType.Code = code;
            contractType.Description = description;
            contractType.CreatedDate = DateTime.UtcNow;
            contractType.CreatedBy = "System";
            return contractType;
        }
        private static ErrorOr<ContractType> Update(Guid Id, string code, string description)
        {
            List<Error> errors = Validate(code, description);

            if (errors.Count > 0)
                return errors;

            ContractType contractType = new ContractType();
            contractType.Description = description;
            contractType.LastModifiedDate = DateTime.UtcNow;
            contractType.LastModifiedBy = "System";
            return contractType;
        }
        private static List<Error> Validate(string code, string description)
        {
            List<Error> errors = new();
            if (code.Length is < MinDescriptionLength or > MaxDescriptionLength)
            {
                errors.Add(Errors.ContractType.InvalidCode);
            }

            if (description.Length is < MinDescriptionLength or > MaxDescriptionLength)
            {
                errors.Add(Errors.ContractType.InvalidDescription);
            }

            return errors;
        }
    }
}
