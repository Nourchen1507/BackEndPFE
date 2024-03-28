using App.ApplicationCore.Domain.Entities;
using App.ApplicationCore.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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

            // Generate RSA key pair
            using (var rsa = RSA.Create())
            {
                var privateKey = rsa.ExportRSAPrivateKey();
                var publicKey = rsa.ExportRSAPublicKey();

                var rsaSecurityKey = new RsaSecurityKey(rsa);

                var signingCredentials = new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256);

                var securityTokenDescriptor = new SecurityTokenDescriptor
                {
                    Issuer = _jwtOptions.Issuer,
                    Expires = DateTime.Now.AddDays(1),
                    Subject = new ClaimsIdentity(claims),
                    SigningCredentials = signingCredentials,
                    Audience = _jwtOptions.Audience,
                };
                var token = new JwtSecurityTokenHandler().CreateToken(securityTokenDescriptor);
                string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
                return tokenValue;
            }
        }

        public async Task<string> RefreshAccessToken(string refreshToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Chargez la clé publique RSA
            using (var rsa = RSA.Create())
            {
                byte[] publicKeyBytes = MyKeyPackage.GetPublicKeyBytes(); // Méthode hypothétique pour récupérer la clé publique
                rsa.ImportSubjectPublicKeyInfo(publicKeyBytes, out _);

                var rsaSecurityKey = new RsaSecurityKey(rsa);

                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _jwtOptions.Issuer,
                    ValidAudience = _jwtOptions.Audience,
                    IssuerSigningKey = rsaSecurityKey
                };

                JwtSecurityToken jwtSecurityToken;
                ClaimsPrincipal claimsPrincipal;
                try
                {
                    // Valide le token et récupère les claims
                    claimsPrincipal = tokenHandler.ValidateToken(refreshToken, validationParameters, out var validatedToken);
                    jwtSecurityToken = validatedToken as JwtSecurityToken;
                }
                catch (SecurityTokenException ex)
                {
                    throw new SecurityTokenException("Invalid token: " + ex.Message);
                }

                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.RsaSha256, StringComparison.InvariantCultureIgnoreCase))
                {
                    throw new SecurityTokenException("Invalid token algorithm");
                }

                var userIdClaim = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    throw new SecurityTokenException("Invalid token");
                }

                var user = await _userRepository.GetByIdAsync(Guid.Parse(userIdClaim.Value));
                if (user == null)
                {
                    throw new SecurityTokenException("User not found");
                }

                // Générez un nouveau token d'accès en utilisant l'utilisateur récupéré
                var newAccessToken = GenerateAccessToken(user);
                return newAccessToken;
            }
        }

    }
}