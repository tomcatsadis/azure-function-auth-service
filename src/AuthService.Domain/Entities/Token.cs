using AuthService.Domain.Settings;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using TomcatSadis.Security.AccessTokenGenerator;

namespace AuthService.Domain.Entities
{
    public class Token
    {
        public string AccessToken { get; private set;  }

        public string TokenType { get; } = "Bearer";

        public double ExpiresIn { get; private set; }

        public Token(string accessToken, double expiresIn)
        {
            AccessToken = accessToken;
            ExpiresIn = expiresIn;
        }

        public static Token GenerateToken(User user, IAccessTokenSettings accessTokenSettings)
        {

            var claims = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
                new System.Security.Claims.Claim(JwtRegisteredClaimNames.FamilyName, user.LastName)
            };

            if (user.Claims != null && user.Claims.Count > 0)
            {
                user.Claims.ForEach((claim) =>
                {
                    claims.Add(new System.Security.Claims.Claim(claim.Type, claim.Value));
                });
            }

            var accessToken = AccessTokenGenerator.GenerateAccessToken(
                userId: user.Id.ToString(),
                privateKey: accessTokenSettings.PrivateKey,
                issuer: accessTokenSettings.Issuer,
                audience: accessTokenSettings.Audience,
                notBefore: accessTokenSettings.NotBefore,
                expires: accessTokenSettings.Expires,
                additionClaims: claims);

            return new Token(
                accessToken: accessToken,
                expiresIn: accessTokenSettings.ExpiresIn);
        }
    }
}
