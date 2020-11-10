using AuthService.Domain.Entities;
using AuthService.Domain.Settings;
using AuthService.Domain.UnitTests.Util;
using System;
using Xunit;

namespace AuthService.Domain.UnitTests.Entities
{
    public class TokenTests
    {
        [Theory]
        [MemberData(nameof(TokenFactory.Data), MemberType = typeof(TokenFactory))]
        public void TokenShouldBeCreatedAndValid(
            IAccessTokenSettings accessTokenSettings,
            User user,
            string expectedAccessToken
            )
        {
            var token = Token.GenerateToken(user, accessTokenSettings);

            Assert.Equal(accessTokenSettings.ExpiresIn, token.ExpiresIn);
            Assert.Equal(expectedAccessToken, token.AccessToken);
        }
    }
}
