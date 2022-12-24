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
    public class Contract : BaseEntity
    {
        public string ContractNo { get; set; }

        public Guid ContractTypeId { get; set; }

        public DateTime ContractDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? RenewalDate { get; set; }

        public Guid CommissionMethodId { get; set; }

        public Guid ContractStatusId { get; set; }

        public Guid PartnerId { get; set; } 

        [ForeignKey("ContractTypeId")]
        public ContractType ContractType { get; set; }

        [ForeignKey("CommissionMethodId")]
        public CommissionMethod CommissionMethod { get; set; }

        [ForeignKey("ContractStatusId")]
        public ContractStatus ContractStatus { get; set; }

        //[ForeignKey("PartnerId")]
        //public Partner Partner { get; set; }
        public static ErrorOr<Contract> From(ContractRequest contractRequest)
        {
            return Create(contractRequest);
        }
        public static ErrorOr<Contract> From(Guid Id, ContractRequest contractRequest)
        {
            return Update(contractRequest);
        }
        private static ErrorOr<Contract> Create(ContractRequest contractRequest)
        {
            List<Error> errors = Validate(contractRequest);

            if (errors.Count > 0)
                return errors;

            Contract contract = new Contract();
            contract.ContractNo = contractRequest.ContractNo;
            contract.ContractDate = contractRequest.ContractDate;
            contract.ContractTypeId = new Guid(contractRequest.ContractTypeId);
            contract.ContractStatusId = new Guid(contractRequest.ContractStatusId);
            contract.CommissionMethodId = new Guid(contractRequest.CommissionMethodId);
            contract.PartnerId = new Guid(contractRequest.PartnerId);
            contract.StartDate = contractRequest.StartDate;
            contract.EndDate = contractRequest.EndDate;
            contract.RenewalDate = contractRequest.RenewalDate;
            contract.CreatedDate = DateTime.UtcNow;
            contract.CreatedBy = "System";
            return contract;
        }
        private static ErrorOr<Contract> Update(ContractRequest contractRequest)
        {
            List<Error> errors = Validate(contractRequest);

            if (errors.Count > 0)
                return errors;

            Contract contract = new Contract();
            contract.ContractNo = contractRequest.ContractNo;
            contract.ContractDate = contractRequest.ContractDate;
            contract.ContractTypeId = new Guid(contractRequest.ContractTypeId);
            contract.ContractStatusId = new Guid(contractRequest.ContractStatusId);
            contract.CommissionMethodId = new Guid(contractRequest.CommissionMethodId);
            contract.PartnerId = new Guid(contractRequest.PartnerId);
            contract.StartDate = contractRequest.StartDate;
            contract.EndDate = contractRequest.EndDate;
            contract.RenewalDate = contractRequest.RenewalDate;
            contract.LastModifiedDate = DateTime.UtcNow;
            contract.LastModifiedBy = "System";
            return contract;
        }
        private static List<Error> Validate(ContractRequest contractRequest)
        {
            List<Error> errors = new();
            if (string.IsNullOrEmpty(contractRequest.ContractNo))
            {
                errors.Add(Errors.Contract.InvalidContractNo);
            }
            if (string.IsNullOrEmpty(contractRequest.PartnerId))
            {
                errors.Add(Errors.Contract.InvalidPartnerId);
            }
            if (string.IsNullOrEmpty(contractRequest.ContractStatusId))
            {
                errors.Add(Errors.Contract.InvalidContractStatusId);
            }
            if (string.IsNullOrEmpty(contractRequest.ContractTypeId))
            {
                errors.Add(Errors.Contract.InvalidContractTypeId);
            }
            if (string.IsNullOrEmpty(contractRequest.CommissionMethodId))
            {
                errors.Add(Errors.Contract.InvalidCommissionMethodId);
            }
            return errors;
        }
    }
}
