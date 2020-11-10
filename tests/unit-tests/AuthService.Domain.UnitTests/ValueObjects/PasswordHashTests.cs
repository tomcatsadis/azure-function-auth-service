using AuthService.Domain.ValueObjects;
using Xunit;

namespace AuthService.Domain.UnitTests.ValueObjects
{
    public class PasswordHashTests
    {
        [Fact]
        public void DifferentPasswordShouldBeUnverified()
        {
            Password password = new Password("asd123");

            PasswordHash passwordHash = password.Hash();

            Password passwordToBeVerified = new Password("asd123456");

            Assert.False(passwordHash.Verify(passwordToBeVerified));
        }

        [Fact]
        public void SamePasswordShouldBeVerified()
        {
            Password password = new Password("asd123");

            PasswordHash passwordHash = password.Hash();

            Password passwordToBeVerified = new Password("asd123");

            Assert.True(passwordHash.Verify(passwordToBeVerified));
        }
    }
}
