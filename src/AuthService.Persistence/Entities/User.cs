using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace AuthService.Persistence.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }

        [BsonElement("password")]
        public string PasswordHash { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("isEmailVerified")]
        public bool IsEmailVerified { get; set; }

        [BsonElement("lastLoginUsingEmail")]
        public DateTime LastLoginUsingEmail { get; set; }

        [BsonElement("phone")]
        public string Phone { get; set; }

        [BsonElement("isPhoneVerified")]
        public bool IsPhoneVerified { get; set; }

        [BsonElement("lastLoginUsingPhone")]
        public DateTime LastLoginUsingPhone { get; set; }

        [BsonElement("signUpDate")]
        public DateTime SignUpDate { get; set; }

        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        public string LastName { get; set; }

        [BsonElement("claims")]
        public List<Claim> Claims { get; set; }

        public Domain.Entities.User MapToDomainEntity()
        {
            List<Domain.Entities.Claim> claimDomainEntities = null;

            if (Claims != null)
            {
                claimDomainEntities = new List<Domain.Entities.Claim>(Claims.Count);

                Claims.ForEach((claim) =>
                {
                    claimDomainEntities.Add(claim.MapToDomainEntity());
                });
            }

            return Domain.Entities.User.Load(
                id: Id,
                passwordHash: PasswordHash,
                email: Email,
                isEmailVerified: IsEmailVerified,
                lastLoginUsingEmail: LastLoginUsingEmail,
                phone: Phone,
                isPhoneVerified: IsPhoneVerified,
                lastLoginUsingPhone: LastLoginUsingPhone,
                signUpDate: SignUpDate,
                firstName: FirstName,
                lastName: LastName,
                claims: claimDomainEntities
            );
        }

        public static User Load(Domain.Entities.User user)
        {
            if (user == null) return null;

            List<Claim> claims = null;

            if (user.Claims != null)
            {
                claims = new List<Claim>(user.Claims.Count);

                user.Claims.ForEach((claim) =>
                {
                    claims.Add(Claim.Load(claim));
                });
            }

            return new User
            {
                Id = user.Id,
                PasswordHash = user.PasswordHash,
                Email = user.Email,
                IsEmailVerified = user.IsEmailVerified,
                LastLoginUsingEmail = user.LastLoginUsingEmail,
                Phone = user.Phone,
                IsPhoneVerified = user.IsPhoneVerified,
                LastLoginUsingPhone = user.LastLoginUsingPhone,
                SignUpDate = user.SignUpDate,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Claims = claims
            };
        }
    }
}
