using AuthService.Domain.Exceptions;
using AuthService.Domain.ValueObjects;
using Xunit;

namespace AuthService.Domain.UnitTests.ValueObjects
{
    public class EmailTests
    {
        [Fact]
        public void ThrowsWhenCreateNullEmail()
        {
            Assert.Throws<ParameterException>(
                () => new Email(null));
        }

        [Fact]
        public void ThrowsWhenCreateEmptyEmail()
        {
            string empty = string.Empty;

            Assert.Throws<ParameterException>(
                () => new Email(empty));
        }

        [Fact]
        public void ThrowsWhenCreateEmailContainWhiteSpaces()
        {
            string email = "albe rt@ gma il.com";

            Assert.Throws<ParameterException>(
                () => new Email(email));
        }

        [Fact]
        public void ThrowsWhenCreateInvalidEmail()
        {
            string email = "albertgmailcom";

            Assert.Throws<ParameterException>(
                () => new Email(email));
        }

        [Fact]
        public void EmailShouldBeCreated()
        {
            string validEmail = "albert@gmail.com";

            Assert.Equal(validEmail, new Email(validEmail));
        }

        [Fact]
        public void EmailShouldBeTrimed()
        {
            string email = "   albert@gmail.com     ";

            string trimedEmail = "albert@gmail.com";

            Assert.Equal(trimedEmail, new Email(email));
        }
    }
}
