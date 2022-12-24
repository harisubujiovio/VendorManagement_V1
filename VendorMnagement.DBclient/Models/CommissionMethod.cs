using ErrorOr;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendorManagement.Contracts;
using VendorManagement.Contracts.ServiceErrors;
namespace VendorManagement.DBclient.Models
{
    [Index(nameof(Code), IsUnique = true)]
    public class CommissionMethod : BaseEntity
    {
        public const int MinDescriptionLength = 3;

        public const int MaxDescriptionLength = 150;

        public string Code { get; set; }
        public string Description { get; set; }

        private CommissionMethod()
        {

        }

        public static ErrorOr<CommissionMethod> From(CommissionMethodRequest commissionMethodRequest)
        {
            return Create(commissionMethodRequest.Code,commissionMethodRequest.Description);
        }
        public static ErrorOr<CommissionMethod> From(Guid Id, CommissionMethodRequest commissionMethodRequest)
        {
            return Update(Id, commissionMethodRequest.Description);
        }
        private static ErrorOr<CommissionMethod> Create(string code, string description)
        {
            List<Error> errors = Validate(description);

            if (errors.Count > 0)
                return errors;

            CommissionMethod commissionMethod = new CommissionMethod();
            commissionMethod.Code = code;
            commissionMethod.Description = description;
            commissionMethod.CreatedDate = DateTime.UtcNow;
            commissionMethod.CreatedBy = "System";
            return commissionMethod;
        }
        private static ErrorOr<CommissionMethod> Update(Guid Id, string description)
        {
            List<Error> errors = Validate(description);

            if (errors.Count > 0)
                return errors;

            CommissionMethod commissionMethod = new CommissionMethod();
            commissionMethod.Description = description;
            commissionMethod.LastModifiedDate = DateTime.UtcNow;
            commissionMethod.LastModifiedBy = "System";
            return commissionMethod;
        }
        private static List<Error> Validate(string description)
        {
            List<Error> errors = new();
            if (description.Length is < MinDescriptionLength or > MaxDescriptionLength)
            {
                errors.Add(Errors.PartnerType.InvalidDescription);
            }

            return errors;
        }
    }
}
