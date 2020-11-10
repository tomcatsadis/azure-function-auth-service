using AuthService.Domain.Exceptions;
using AuthService.Domain.ValueObjects;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace AuthService.Domain.Entities
{
    public class User
    {
        public ObjectId Id { get; private set; }

        public PasswordHash PasswordHash { get; private set; }

        public string Email { get; private set; }

        public bool IsEmailVerified { get; private set; } = false;

        public DateTime LastLoginUsingEmail { get; set; }

        public string Phone { get; private set; }

        public bool IsPhoneVerified { get; private set; } = false;

        public DateTime LastLoginUsingPhone { get; set; }

        public DateTime SignUpDate { get; private set; } = DateTime.UtcNow;

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }

        public List<Claim> Claims { get; set; }

        public void ChangePassword(Password oldPasswordConfirmation, Password newPassword)
        {
            if (!VerifyPassword(oldPasswordConfirmation))
            {
                throw new ParameterException("Invalid old password");
            }

            PasswordHash = newPassword.Hash();
        }

        public bool VerifyPassword(Password password)
        {
            return PasswordHash.Verify(password);
        }

        public void UpdateLastLoginUsingEmail()
        {
            LastLoginUsingEmail = DateTime.UtcNow;
        }

        public void UpdateLastLoginUsingPhone()
        {
            LastLoginUsingPhone = DateTime.UtcNow;
        }

        public void VerifiedEmail()
        {
            IsEmailVerified = true;
        }

        public void VerifiedPhone()
        {
            IsPhoneVerified = true;
        }

        public void UpdateInfo(Name firstName, Name lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public static User Load(
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
            return new User
            {
                Id = id,
                PasswordHash = passwordHash,
                Email = email,
                IsEmailVerified = isEmailVerified,
                LastLoginUsingEmail = lastLoginUsingEmail,
                Phone = phone,
                IsPhoneVerified = isPhoneVerified,
                LastLoginUsingPhone = lastLoginUsingPhone,
                SignUpDate = signUpDate,
                FirstName = firstName,
                LastName = lastName,
                Claims = claims
            };
        }

        public static User NewUserUsingPhone(Phone phone, Password password, Name firstName, Name lastName)
        {
            return new User
            {
                Phone = phone,
                PasswordHash = password.Hash(),
                FirstName = firstName,
                LastName = lastName
            };
        }

        public static User NewUserUsingEmail(Email email, Password password, Name firstName, Name lastName)
        {
            return new User
            {
                Email = email,
                PasswordHash = password.Hash(),
                FirstName = firstName,
                LastName = lastName
            };
        }
    }
}