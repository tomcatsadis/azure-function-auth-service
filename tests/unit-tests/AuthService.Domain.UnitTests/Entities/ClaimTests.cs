using AuthService.Domain.Entities;
using AuthService.Domain.UnitTests.Util;
using Xunit;

namespace AuthService.Domain.UnitTests.Entities
{
    public class ClaimTests
    {
        [Theory]
        [MemberData(nameof(ClaimFactory.Data), MemberType = typeof(ClaimFactory))]
        public void ClaimShouldBeLoaded(string type, string value)
        {
            var claim = new Claim(
                type: type,
                value: value);

            Assert.Equal(type, claim.Type);
            Assert.Equal(value, claim.Value);
        }
    }
}
