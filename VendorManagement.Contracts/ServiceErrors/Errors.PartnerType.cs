using ErrorOr;

namespace VendorManagement.Contracts.ServiceErrors
{
    public static partial class Errors
    {
        public static class PartnerType
        {
            public const int MinDescriptionLength = 4;
            public const int MaxDescriptionLength = 150;

            public const int MinCodeLength = 4;
            public const int MaxCodeLength = 50;
            public static Error InvalidCode => Error.Validation(
                code: "PartnerType.InvalidCode",
                description: $"Partner Type Code must be at least {MinCodeLength} " +
                $"characters long and at most {MaxCodeLength} length");

            public static Error InvalidDescription => Error.Validation(
                code: "PartnerType.InvalidDescription",
                description: $"Partner Type Description must be at least {MinDescriptionLength} " +
                $"characters long and at most {MaxDescriptionLength} length");

            public static Error NotFound => Error.NotFound(
              code: "PartnerType.NotFound",
              description: "Partner Type not found");
        }
        public static class Partner
        {
            public const int MinPartnerNoLength = 4;
            public const int MaxPartnerNoLength = 150;

            public const int MinPartnerNameLength = 4;
            public const int MaxPartnerNameLength = 50;


            public static Error InvalidPartnerNo => Error.Validation(
                code: "Partner.PartnerNo",
                description: $"Partner No must be at least {MinPartnerNoLength} " +
                $"characters long and at most {MaxPartnerNoLength} length");

            public static Error InvalidPartnerName => Error.Validation(
                code: "Partner.PartnerName",
                description: $"Partner Name must be at least {MinPartnerNameLength} " +
                $"characters long and at most {MaxPartnerNameLength} length");

            public static Error InvalidEmail => Error.Validation(
               code: "Partner.InvalidEmail",
               description: $"Partner email should not be empty");

            public static Error InvalidMobileNumber => Error.Validation(
              code: "Partner.InvalidMobileNumber",
              description: $"Partner mobilenumber should not be empty");

            public static Error NotFound => Error.NotFound(
              code: "Partner.NotFound",
              description: "Partner not found");
        }
        public static class CommissionMethod
        {
            public const int MinDescriptionLength = 4;
            public const int MaxDescriptionLength = 150;

            public const int MinCodeLength = 4;
            public const int MaxCodeLength = 50;
            public static Error InvalidCode => Error.Validation(
              code: "CommissionMethod.InvalidCode",
              description: $"Commission Method Code must be at least {MinCodeLength} " +
              $"characters long and at most {MaxCodeLength} length");

            public static Error InvalidDescription => Error.Validation(
                code: "CommissionMethod.InvalidDescription",
                description: $"Commission Method must be at least {MinDescriptionLength} " +
                $"characters long and at most {MaxDescriptionLength} length");

            public static Error NotFound => Error.NotFound(
              code: "CommissionMethod.NotFound",
              description: "Commission Method not found");
        }
        public static class ContractStatus
        {
            public const int MinDescriptionLength = 4;
            public const int MaxDescriptionLength = 150;

            public const int MinCodeLength = 4;
            public const int MaxCodeLength = 50;
            public static Error InvalidCode => Error.Validation(
              code: "ContractStatus.InvalidCode",
              description: $"Contract Status Code must be at least {MinCodeLength} " +
              $"characters long and at most {MaxCodeLength} length");

            public static Error InvalidDescription => Error.Validation(
                code: "ContractStatus.InvalidDescription",
                description: $"Contract Status must be at least {MinDescriptionLength} " +
                $"characters long and at most {MaxDescriptionLength} length");

            public static Error NotFound => Error.NotFound(
              code: "ContractStatus.NotFound",
              description: "Contract Status not found");
        }
        public static class ContractType
        {
            public const int MinDescriptionLength = 4;
            public const int MaxDescriptionLength = 150;

            public const int MinCodeLength = 4;
            public const int MaxCodeLength = 50;
            public static Error InvalidCode => Error.Validation(
              code: "ContractType.InvalidCode",
              description: $"Contract Type Code must be at least {MinCodeLength} " +
              $"characters long and at most {MaxCodeLength} length");

            public static Error InvalidDescription => Error.Validation(
                code: "ContractType.InvalidDescription",
                description: $"Contract Type must be at least {MinDescriptionLength} " +
                $"characters long and at most {MaxDescriptionLength} length");

            public static Error NotFound => Error.NotFound(
              code: "ContractType.NotFound",
              description: "Contract Type not found");
        }
        public static class Login
        {
            public static Error EmptyUserName => Error.Validation(
                code: "User.EmptyUserName",
                description: "User name is required");

            public static Error EmptyPassword => Error.Validation(
                code: "User.EmptyPassword",
                description: "Password is required");

            public static Error InvalidCredentials => Error.Validation(
             code: "User.InvalidCredentials",
             description: "Invalid Credentials");
        }
        public static class Register
        {
            public static Error EmptyFirstName => Error.Validation(
                code: "User.EmptyFirstName",
                description: "First name is required");

            public static Error InvalidLastName => Error.Validation(
               code: "User.InvalidLastName",
               description: "Last name is required");

            public static Error InvalidEmail => Error.Validation(
               code: "User.InvalidEmail",
               description: "Email is required");

            public static Error InvalidMobileNumber => Error.Validation(
              code: "User.InvalidMobileNumber",
              description: "Mobile Number is required");

            public static Error InvalidRole => Error.Validation(
              code: "User.InvalidRole",
              description: "Role is required");

        }
        public static class Role
        {
            public const int MinDescriptionLength = 4;
            public const int MaxDescriptionLength = 150;

            public const int MinCodeLength = 4;
            public const int MaxCodeLength = 50;
            public static Error InvalidName => Error.Validation(
                code: "Role.InvalidName",
                description: $"Role Name Code must be at least {MinCodeLength} " +
                $"characters long and at most {MaxCodeLength} length");

            public static Error InvalidDescription => Error.Validation(
                code: "Role.InvalidDescription",
                description: $"Role Name Description must be at least {MinDescriptionLength} " +
                $"characters long and at most {MaxDescriptionLength} length");

            public static Error NotFound => Error.NotFound(
              code: "Role.NotFound",
              description: "Role not found");
        }
    }
}
