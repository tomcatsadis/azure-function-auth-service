using AuthService.Domain.Entities;
using AuthService.Domain.Exceptions;
using AuthService.Domain.UnitTests.Util;
using AuthService.Domain.ValueObjects;
using FluentAssertions;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AuthService.Domain.UnitTests.Entities
{
    public class UserTests
    {
        [Theory]
        [MemberData(nameof(UserFactory.UsersDataUsingEmail), MemberType = typeof(UserFactory))]
        public void NewUserUsingEmailShouldBeLoaded(string email, string password, string firstName, string lastName)
        {
            var user = User.NewUserUsingEmail(
                email: email,
                password: password,
                firstName: firstName,
                lastName: lastName);

            Assert.Equal(email, user.Email);
            Assert.Equal(firstName, user.FirstName);
            Assert.Equal(lastName, user.LastName);

            user.PasswordHash.Verify(password);
        }

        [Theory]
        [MemberData(nameof(UserFactory.UsersDataUsingPhone), MemberType = typeof(UserFactory))]
        public void NewUserUsingPhoneShouldBeLoaded(string phone, string password, string firstName, string lastName)
        {
            var user = User.NewUserUsingPhone(
                phone: phone,
                password: password,
                firstName: firstName,
                lastName: lastName);

            Assert.Equal(phone, user.Phone);
            Assert.Equal(firstName, user.FirstName);
            Assert.Equal(lastName, user.LastName);

            user.PasswordHash.Verify(password);
        }

        [Theory]
        [MemberData(nameof(UserFactory.UsersDataUsingEmail), MemberType = typeof(UserFactory))]
        public void SamePasswordShouldBeVerified(string email, string password, string firstName, string lastName)
        {
            var user = User.NewUserUsingEmail(
                email: email,
                password: password,
                firstName: firstName,
                lastName: lastName);

            user.VerifyPassword(password);
        }

        [Theory]
        [MemberData(nameof(UserFactory.UsersDataUsingEmail), MemberType = typeof(UserFactory))]
        public void ChangePasswordShouldBeSucceed(string email, string password, string firstName, string lastName)
        {
            var user = User.NewUserUsingEmail(
                email: email,
                password: password,
                firstName: firstName,
                lastName: lastName);

            var newPassword = $"asd{password}abc";

            user.ChangePassword(
                oldPasswordConfirmation: password,
                newPassword: newPassword);

            user.VerifyPassword(newPassword);
        }

        [Theory]
        [MemberData(nameof(UserFactory.UsersDataUsingEmail), MemberType = typeof(UserFactory))]
        public void ChangePasswordUsingInvalidOldPasswordConfirmationShouldBeFailed(string email, string password, string firstName, string lastName)
        {
            var user = User.NewUserUsingEmail(
                email: email,
                password: password,
                firstName: firstName,
                lastName: lastName);

            var invalidOldPassword = $"asd{password}abc";

            Assert.Throws<ParameterException>(
                () => user.ChangePassword(
                    oldPasswordConfirmation: invalidOldPassword,
                    newPassword: "asd12345"));
        }

        [Theory]
        [MemberData(nameof(UserFactory.UsersDataUsingEmail), MemberType = typeof(UserFactory))]
        public void VerifiedEmailMethodShouldSetEmailToVerified(string email, string password, string firstName, string lastName)
        {
            var user = User.NewUserUsingEmail(
                email: email,
                password: password,
                firstName: firstName,
                lastName: lastName);

            user.VerifiedEmail();

            Assert.True(user.IsEmailVerified);
        }

        [Theory]
        [MemberData(nameof(UserFactory.UsersDataUsingPhone), MemberType = typeof(UserFactory))]
        public void VerifiedPhoneMethodShouldSetPhoneToVerified(string phone, string password, string firstName, string lastName)
        {
            var user = User.NewUserUsingPhone(
                phone: phone,
                password: password,
                firstName: firstName,
                lastName: lastName);

            user.VerifiedPhone();

            Assert.True(user.IsPhoneVerified);
        }

        [Theory]
        [MemberData(nameof(UserFactory.UsersDataUsingEmail), MemberType = typeof(UserFactory))]
        public void UpdateInfoShouldUpdateNecessaryProperty(string email, string password, string firstName, string lastName)
        {
            var user = User.NewUserUsingEmail(
                email: email,
                password: password,
                firstName: firstName,
                lastName: lastName);

            var updatedFirstName = $"Abc{firstName}qwe";
            var updatedLastName = $"Def{lastName}zxc";

            user.UpdateInfo(
                firstName: updatedFirstName,
                lastName: updatedLastName);

            Assert.Equal(updatedFirstName, user.FirstName);
            Assert.Equal(updatedLastName, user.LastName);
        }

        [Theory]
        [MemberData(nameof(UserFactory.UsersData), MemberType = typeof(UserFactory))]
        public void UserShouldBeLoaded(
            ObjectId id,
            PasswordHash passwordHash,
            string email,
            bool isEmailVerified,
            DateTime lastLoginUsingEmail,
            string phone,
            bool isPhoneVerified,
            DateTime lastLoginUsingPhone,
            DateTime signUpDate,
            string firstName,
            string lastName,
            List<Claim> claims)
        {
            var user = User.Load(
                id: id,
                passwordHash: passwordHash,
                email: email,
                isEmailVerified: isEmailVerified,
                lastLoginUsingEmail: lastLoginUsingEmail,
                phone: phone,
                isPhoneVerified: isPhoneVerified,
                lastLoginUsingPhone: lastLoginUsingPhone,
                signUpDate: signUpDate,
                firstName: firstName,
                lastName: lastName,
                claims: claims
            );

            Assert.Equal(id, user.Id);
            Assert.Equal(passwordHash, user.PasswordHash);
            Assert.Equal(email, user.Email);
            Assert.Equal(isEmailVerified, user.IsEmailVerified);
            Assert.Equal(lastLoginUsingEmail, user.LastLoginUsingEmail);
            Assert.Equal(phone, user.Phone);
            Assert.Equal(isPhoneVerified, user.IsPhoneVerified);
            Assert.Equal(lastLoginUsingPhone, user.LastLoginUsingPhone);
            Assert.Equal(signUpDate, user.SignUpDate);
            Assert.Equal(firstName, user.FirstName);
            Assert.Equal(lastName, user.LastName);
            user.Claims.Should().BeEquivalentTo(claims);
        }
    }
}
