using System.Security.Claims;

namespace GigaConsulting.Infra.CrossCutting.Identity.Services
{
    public interface IJwtFactory
    {
        Task<JwtToken> GenerateJwtToken(ClaimsIdentity claimsIdentity);
    }
}
