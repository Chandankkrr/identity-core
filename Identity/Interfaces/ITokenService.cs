using System.IdentityModel.Tokens.Jwt;

namespace Identity.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string userId);

        JwtSecurityToken DecodeToken(string token);
    }
}