using AuthService.Domain.Exceptions;
using AuthService.Domain.ValueObjects;
using Xunit;

namespace AuthService.Domain.UnitTests.ValueObjects
{
    public class NameTests
    {
        [Fact]
        public void ThrowsWhenCreateNullName()
        {
            Assert.Throws<ParameterException>(
                () => new Name(null));
        }

        [Fact]
        public void ThrowsWhenCreateEmptyName()
        {
            string empty = string.Empty;

            Assert.Throws<ParameterException>(
                () => new Name(empty));
        }

        [Fact]
        public void NameShouldBeCreated()
        {
            string validName = "Albert Brucelee";

            Assert.Equal(validName, new Name(validName));
        }

        [Fact]
        public void SpacesShouldBeCleaned()
        {
            string name = "   Albert    Brucelee     ";

            string cleanedName = "Albert Brucelee";

            Assert.Equal(cleanedName, new Name(name));
        }
    }
}
