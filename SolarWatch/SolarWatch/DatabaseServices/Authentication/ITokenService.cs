using Microsoft.AspNetCore.Identity;

namespace SolarWatch.DatabaseServices.Authentication;

public interface ITokenService
{
    string CreateToken(IdentityUser user, string role);
}