using ErrorOr;

namespace VendorMangement.API.ServiceErrors
{
    public static class Errors
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
    }
}
