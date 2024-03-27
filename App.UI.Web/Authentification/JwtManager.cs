using App.ApplicationCore.Domain.Entities;
using App.ApplicationCore.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace App.UI.Web.Authentification
{
    public class JwtManager : IJwtManager


    {


        private readonly JwtOptions _jwtOptions; 
        private readonly IUserRepository _userRepository;

        public JwtManager(IOptions<JwtOptions> options)
        {
            _jwtOptions = options.Value;
        }

        public string GenerateAccessToken(User user)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtOptions.Issuer,
                Expires = DateTime.Now.AddDays(1),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = signingCredentials,
                Audience = _jwtOptions.Audience,
            };
            var token = new JwtSecurityTokenHandler().CreateToken(securityTokenDescriptor);
            string tokenValue = new JwtSecurityTokenHandler()
                .WriteToken(token);
            return tokenValue;
        }
        public async Task<string> RefreshAccessToken(string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtOptions.Issuer,
                ValidAudience = _jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_jwtOptions.SecretKey))

            };
            ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken
                (refreshToken, validationParameters, out SecurityToken validatedToken);

            if (validatedToken is not JwtSecurityToken jwtSecurityToken
                || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token first stage");
            }

            var userIdClaim = claimsPrincipal.FindFirst(JwtRegisteredClaimNames.NameId);
            if (userIdClaim == null)
            {
                throw new SecurityTokenException("Invlaid token");
            }

            var user = await _userRepository.GetByIdAsync(Guid.Parse(userIdClaim.Value));
            var newAccessToken = GenerateAccessToken(user);
            return newAccessToken;
        }

    }
}
