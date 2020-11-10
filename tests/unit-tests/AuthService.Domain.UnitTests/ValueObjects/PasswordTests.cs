using AuthService.Domain.Exceptions;
using AuthService.Domain.ValueObjects;
using Xunit;

namespace AuthService.Domain.UnitTests.ValueObjects
{
    public class PasswordTests
    {
        [Fact]
        public void ThrowsWhenCreateNullPassword()
        {
            Assert.Throws<ParameterException>(
                () => new Password(null));
        }

        [Fact]
        public void ThrowsWhenCreateEmptyPassword()
        {
            string empty = string.Empty;

            Assert.Throws<ParameterException>(
                () => new Password(empty));
        }

        [Fact]
        public void ThrowsWhenCreateTooShortPassword()
        {
            string password = "asd";

            Assert.Throws<ParameterException>(
                () => new Password(password));
        }

        [Fact]
        public void PasswordShouldBeCreated()
        {
            string validPassword = "123456";

            Assert.Equal(validPassword, new Password(validPassword));
        }

        [Fact]
        public void HashMethodShouldReturnPasswordHash()
        {
            Password password = new Password("asd123");

            PasswordHash passwordHash = password.Hash();

            Assert.NotNull(passwordHash);
        }
    }
}
