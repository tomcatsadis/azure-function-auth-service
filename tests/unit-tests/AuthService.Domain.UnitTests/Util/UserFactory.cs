using AuthService.Domain.Entities;
using AuthService.Domain.ValueObjects;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AuthService.Domain.UnitTests.Util
{
    public static class UserFactory
    {
        public const string DefaultPassword = "abcde123";
        private static readonly Random random = new Random();

        public static IEnumerable<object[]> UsersDataUsingEmail()
        {
            return new List<object[]>
            {
                new object[] { "albert123@gmail.com", DefaultPassword, "Albert", "Brucelee" },
                new object[] { "bruce123@gmail.com", DefaultPassword, "Bruce", "John" },
                new object[] { "lee123@gmail.com", DefaultPassword, "Lee", "Lele" },
                new object[] { "tomjerry123@gmail.com", DefaultPassword, "Tom", "Jerry" },
                new object[] { "bejo@gmail.com", DefaultPassword, "Bejo", "Udin" }
            };
        }

        public static IEnumerable<object[]> UsersDataUsingPhone()
        {
            return new List<object[]>
            {
                new object[] { "+628111111111", DefaultPassword, "Albert", "Brucelee" },
                new object[] { "+628222222222", DefaultPassword, "Bruce", "John" },
                new object[] { "+628333333334", DefaultPassword, "Lee", "Lele" },
                new object[] { "+628444444444", DefaultPassword, "Tom", "Jerry" },
                new object[] { "+628555555555", DefaultPassword, "Bejo", "Udin" }
            };
        }

        public static IEnumerable<object[]> UsersData()
        {
            return new List<object[]>
            {
                new object[] { 
                    new ObjectId("5f7b787a7d6f589acfc51a15"),
                    PasswordHash.Hash(DefaultPassword),
                    "albert123@gmail.com",
                    false,
                    DateTime.Parse("2020-12-01T01:01:01.0Z"),
                    "+628111111111",
                    false,
                    DateTime.Parse("2020-12-02T01:01:01.0Z"),
                    DateTime.Parse("2020-11-01T01:01:01.0Z"),
                    "Albert", 
                    "Brucelee",
                    new List<Claim> { new Claim("Role", "Customer") } },
                new object[] {
                    new ObjectId("5f7b787a7d6f589acfc51a16"),
                    PasswordHash.Hash(DefaultPassword),
                    "bruce123@gmail.com",
                    false,
                    DateTime.Parse("2020-12-01T01:01:01.0Z"),
                    "+628222222222",
                    false,
                    DateTime.Parse("2020-12-02T01:01:01.0Z"),
                    DateTime.Parse("2020-11-01T01:01:01.0Z"),
                    "Bruce",
                    "John",
                    new List<Claim> { new Claim("Role", "Customer") } },
                new object[] {
                    new ObjectId("5f7b787a7d6f589acfc51a17"),
                    PasswordHash.Hash(DefaultPassword),
                    "lee123@gmail.com",
                    false,
                    DateTime.Parse("2020-12-01T01:01:01.0Z"),
                    "+628333333333",
                    false,
                    DateTime.Parse("2020-12-02T01:01:01.0Z"),
                    DateTime.Parse("2020-11-01T01:01:01.0Z"),
                    "Lee",
                    "Lele",
                    new List<Claim> { new Claim("Role", "Customer") } },
            };
        }

        /*public static List<User> UsersUsingEmail()
        {
            return new List<User>
            {
                User.NewUserUsingEmail(
                    email: $"albert123@gmail.com",
                    password: new Password(DefaultPassword),
                    firstName: "Albert",
                    lastName: "Brucelee"),
                User.NewUserUsingEmail(
                    email: $"bruce123@gmail.com",
                    password: new Password(DefaultPassword),
                    firstName: "Bruce",
                    lastName: "Brucelastname"),
                User.NewUserUsingEmail(
                    email: $"lee123@gmail.com",
                    password: new Password(DefaultPassword),
                    firstName: "Lee",
                    lastName: "Leelastname"),
                User.NewUserUsingEmail(
                    email: $"tomjerry123@gmail.com",
                    password: new Password(DefaultPassword),
                    firstName: "Tom",
                    lastName: "Jerry"),
                User.NewUserUsingEmail(
                    email: $"bejo@gmail.com",
                    password: new Password(DefaultPassword),
                    firstName: "Bejo",
                    lastName: "Udin"),
            };
        }

        public static List<User> UsersUsingPhone()
        {
            return new List<User>
            {
                User.NewUserUsingPhone(
                    phone: $"+628111111111",
                    password: new Password(DefaultPassword),
                    firstName: "Steve",
                    lastName: "Stephen"),
                User.NewUserUsingPhone(
                    phone: $"+62822222222",
                    password: new Password(DefaultPassword),
                    firstName: "John",
                    lastName: "Jontor"),
                User.NewUserUsingPhone(
                    phone: $"+62833333333",
                    password: new Password(DefaultPassword),
                    firstName: "Tejo",
                    lastName: "Tehijo"),
                User.NewUserUsingPhone(
                    phone: $"+62844444444",
                    password: new Password(DefaultPassword),
                    firstName: "Surti",
                    lastName: "Surtinem"),
                User.NewUserUsingPhone(
                    phone: $"+62855555555",
                    password: new Password(DefaultPassword),
                    firstName: "Riki",
                    lastName: "Maru")
            };
        }*/

        public static User GenerateUserUsingEmail(string password = DefaultPassword)
        {
            return User.NewUserUsingEmail(
                email: $"{GetRandomStringWithNumber(8)}@gmail.com",
                password: new Password(password),
                firstName: GetRandomString(8),
                lastName: GetRandomString(8)
            );
        }

        public static User GenerateUserUsingPhone(string password = DefaultPassword)
        {
            return User.NewUserUsingPhone(
                phone: $"+628{GetRandomNumberString(10)}",
                password: new Password(password),
                firstName: GetRandomString(8),
                lastName: GetRandomString(8)
            );
        }

        public static string GetRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetRandomStringWithNumber(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetRandomNumberString(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
