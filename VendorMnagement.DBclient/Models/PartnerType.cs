using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorOr;
using VendorManagement.Contracts;
using VendorManagement.Contracts.ServiceErrors;

namespace VendorManagement.DBclient.Models
{
    public class PartnerType : BaseEntity
    {
        public const int MinDescriptionLength = 3;

        public const int MaxDescriptionLength = 150;
        public string Description { get; set; }

        private PartnerType()
        {

        }
        public static ErrorOr<PartnerType> From(PartnerTypeRequest partnerTypeRequest)
        {
            return Create(partnerTypeRequest.Description);
        }
        public static ErrorOr<PartnerType> From(Guid Id, PartnerTypeRequest partnerTypeRequest)
        {
            return Update(Id, partnerTypeRequest.Description);
        }
        public static ErrorOr<PartnerType> Create(string description)
        {
            List<Error> errors = Validate(description);

            if (errors.Count > 0)
                return errors;

            PartnerType partnerType = new PartnerType();
            partnerType.Description = description;
            partnerType.CreatedDate = DateTime.UtcNow;
            partnerType.CreatedBy = "System";
            return partnerType;
        }
        public static ErrorOr<PartnerType> Update(Guid Id,string description)
        {
            List<Error> errors = Validate(description);
           
            if (errors.Count > 0)
                return errors;

            PartnerType partnerType = new PartnerType();
            partnerType.Description = description;
            partnerType.LastModifiedDate = DateTime.UtcNow;
            partnerType.LastModifiedBy = "System";
            return partnerType;
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
