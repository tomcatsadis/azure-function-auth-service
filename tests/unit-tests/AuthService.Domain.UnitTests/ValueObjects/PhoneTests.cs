using AuthService.Domain.Exceptions;
using AuthService.Domain.ValueObjects;
using Xunit;

namespace AuthService.Domain.UnitTests.ValueObjects
{
    public class PhoneTests
    {
        [Fact]
        public void ThrowsWhenCreateNullPhone()
        {
            Assert.Throws<ParameterException>(
                () => new Phone(null));
        }

        [Fact]
        public void ThrowsWhenCreateEmptyPhone()
        {
            string empty = string.Empty;

            Assert.Throws<ParameterException>(
                () => new Phone(empty));
        }

        [Fact]
        public void ThrowsWhenCreatePhoneWithoutCountryCode()
        {
            string phone = "81234567891";

            Assert.Throws<ParameterException>(
                () => new Phone(phone));
        }

        [Fact]
        public void ThrowsWhenCreateTooShortPhone()
        {
            string phone = "+628";

            Assert.Throws<ParameterException>(
                () => new Phone(phone));
        }

        [Fact]
        public void PhoneShouldBeCreated()
        {
            string validPhone = "+6281234567891";

            Assert.Equal(validPhone, new Phone(validPhone));
        }

        [Fact]
        public void SpacesShouldBeCleaned()
        {
            string phone = " +628 12345 6789 1 ";

            string cleanedPhone = "+6281234567891";

            Assert.Equal(cleanedPhone, new Phone(phone));
        }
    }
}
