using AuthService.Domain.Entities;
using FluentAssertions;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Xunit;

namespace AuthService.Persistence.IntegrationTests.Entitites
{
    public class UserTests
    {
        [Fact]
        public void ObjectMappingTest()
        {
            /* insert data */

            var expectedClaims = new List<Claim>
            {
                new Claim("testType", "testValue")
            };

            var expectedUser = User.Load(
                id: ObjectId.GenerateNewId(),
                passwordHash: "asd123Hash",
                email: "albert@gmail.com",
                isEmailVerified: true,
                lastLoginUsingEmail: DateTime.UtcNow.AddDays(-1),
                phone: "+6281234567891",
                isPhoneVerified: true,
                lastLoginUsingPhone: DateTime.UtcNow.AddDays(-3),
                signUpDate: DateTime.UtcNow.AddDays(-10),
                firstName: "Albert",
                lastName: "Brucelee",
                claims: expectedClaims
            );

            var userEntity = Persistence.Entities.User.Load(expectedUser);

            var actualUser = userEntity.MapToDomainEntity();

            /* check if the data are equals */

            actualUser.Should().BeEquivalentTo(expectedUser);
        }
    }
}
