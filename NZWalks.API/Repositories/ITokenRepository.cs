using Microsoft.AspNetCore.Identity;

namespace NZWalks.API.Repositories
{
    public interface ITokenRepository
    {
        string GetToken(IdentityUser user, List<string> roles);
    }
}
