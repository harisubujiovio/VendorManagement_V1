using ErrorOr;

namespace VendorManagement.Contracts.ServiceErrors
{
    public static partial class Errors
    {
        public static class PartnerType
        {
            public const int MinDescriptionLength = 3;
            public const int MaxDescriptionLength = 150;
            public static Error InvalidDescription => Error.Validation(
                code: "PartnerType.InvalidDescription",
                description: $"Partner Type must be at least {MinDescriptionLength} " +
                $"characters long and at most {MaxDescriptionLength} length");

            public static Error NotFound => Error.NotFound(
              code: "PartnerType.NotFound",
              description: "Partner Type not found");
        }
        public static class CommissionMethod
        {
            public const int MinDescriptionLength = 3;
            public const int MaxDescriptionLength = 150;
            public static Error InvalidDescription => Error.Validation(
                code: "CommissionMethod.InvalidDescription",
                description: $"Commission Method must be at least {MinDescriptionLength} " +
                $"characters long and at most {MaxDescriptionLength} length");

            public static Error NotFound => Error.NotFound(
              code: "CommissionMethod.NotFound",
              description: "Commission Method not found");
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

            public static Error EmptyLastName => Error.Validation(
               code: "User.EmptyLastName",
               description: "Last name is required");

            public static Error EmptyEmail => Error.Validation(
               code: "User.EmptyEmail",
               description: "Email is required");

            public static Error EmptyMobileNumber => Error.Validation(
              code: "User.EmptyMobileNumber",
              description: "Mobile Number is required");
        }
    }
}
