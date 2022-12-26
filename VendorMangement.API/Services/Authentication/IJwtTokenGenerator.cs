namespace VendorMangement.API.Services.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateJwtToken(Guid userId, string displayName, Guid partnerid, Guid roleid);
    }
}
