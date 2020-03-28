using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace PUCMinasTCC.Util.Util
{
    public static class TokenHelper
    {
        public static string GenerateJwtToken(int idUsuario, TokenConfigurations tokenConfigurations, SigningConfigurations signingConfigurations)
        {
            ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(nameof(idUsuario), "Login"),
                    new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, idUsuario.ToString())
                    }
                );
            var dataCriacao = DateTime.Now;
            var dataExpiracao = dataCriacao.AddSeconds(tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();
            SecurityToken securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = tokenConfigurations.Issuer,
                Audience = tokenConfigurations.Audience,
                SigningCredentials = signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao,
                
            });

            return handler.WriteToken(securityToken);
        }


        public static bool ValidateToken(string authToken, TokenConfigurations tokenConfigurations, SigningConfigurations signinConfigurations)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters(tokenConfigurations, signinConfigurations);
            IdentityModelEventSource.ShowPII = true;
            SecurityToken validatedToken;

            try
            {
                IPrincipal principal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);

            }
            catch (Exception ex)
            {
                throw new Exception($"Token inválido. Erro: {ex.Message}");
            }
            return true;
        }

        private static TokenValidationParameters GetValidationParameters(TokenConfigurations tokenConfigurations, SigningConfigurations signinConfigurations)
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = true, // Because there is no expiration in the generated token
                ValidateAudience = false, // Because there is no audiance in the generated token
                ValidateIssuer = false,   // Because there is no issuer in the generated token
                ValidIssuer = tokenConfigurations.Issuer,
                ValidAudience = tokenConfigurations.Audience,
               IssuerSigningKey = signinConfigurations.Key // The same key as the one that generate the token
            };
        }
    }
}
