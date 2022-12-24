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
    public class Partner : BaseEntity
    {
        public const int MinPartnerNoLength = 4;
        public const int MaxPartnerNoLength = 150;

        public const int MinPartnerNameLength = 4;
        public const int MaxPartnerNameLength = 50;
        public string PartnerNo { get; set; }

        public string PartnerName { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public Guid PartnerTypeId { get; set; }

        [ForeignKey("PartnerTypeId")]
        public PartnerType PartnerType { get; set; }

        public List<User> Users { get; set; }
        public static ErrorOr<Partner> From(PartnerRequest partnerRequest)
        {
            return Create(partnerRequest);
        }
        public static ErrorOr<Partner> From(Guid Id, PartnerRequest partnerRequest)
        {
            return Update(partnerRequest);
        }
        private static ErrorOr<Partner> Create(PartnerRequest partnerRequest)
        {
            List<Error> errors = Validate(partnerRequest);

            if (errors.Count > 0)
                return errors;

            Partner partner = new Partner();
            partner.PartnerNo = partnerRequest.PartnerNo;
            partner.PartnerName = partnerRequest.PartnerName;
            partner.PartnerTypeId = partnerRequest.PartnerTypeId;
            partner.Email = partnerRequest.Email;
            partner.MobileNumber = partnerRequest.MobileNumber;
            partner.CreatedDate = DateTime.UtcNow;
            partner.CreatedBy = "System";
            return partner;
        }
        private static ErrorOr<Partner> Update(PartnerRequest partnerRequest)
        {
            List<Error> errors = Validate(partnerRequest);

            if (errors.Count > 0)
                return errors;

            Partner partner = new Partner();
            partner.PartnerNo = partnerRequest.PartnerNo;
            partner.PartnerName = partnerRequest.PartnerName;
            partner.PartnerTypeId = partnerRequest.PartnerTypeId;
            partner.Email = partnerRequest.Email;
            partner.MobileNumber = partnerRequest.MobileNumber;
            partner.LastModifiedDate = DateTime.UtcNow;
            partner.LastModifiedBy = "System";
            return partner;
        }
        private static List<Error> Validate(PartnerRequest partnerRequest)
        {
            List<Error> errors = new();
            if (partnerRequest.PartnerNo.Length is < MinPartnerNoLength or > MaxPartnerNoLength)
            {
                errors.Add(Errors.Partner.InvalidPartnerNo);
            }
            if (partnerRequest.PartnerName.Length is < MinPartnerNoLength or > MaxPartnerNoLength)
            {
                errors.Add(Errors.Partner.InvalidPartnerName);
            }
            if (string.IsNullOrEmpty(partnerRequest.Email))
            {
                errors.Add(Errors.Partner.InvalidEmail);
            }
            if (string.IsNullOrEmpty(partnerRequest.MobileNumber))
            {
                errors.Add(Errors.Partner.InvalidMobileNumber);
            }
            return errors;
        }
    }
}
