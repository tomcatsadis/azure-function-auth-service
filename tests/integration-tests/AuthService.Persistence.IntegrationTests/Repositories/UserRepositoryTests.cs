using AuthService.Domain.UnitTests.Util;
using AuthService.Persistence.IntegrationTests.Util;
using FluentAssertions;
using MongoDB.Bson;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AuthService.Persistence.IntegrationTests
{
    [Collection(nameof(PersistenceTestCollection))]
    public class UserRepositoryTests
    {
        private readonly PersistenceTestFixture _fixture;

        public UserRepositoryTests(PersistenceTestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task Get()
        {
            /* insert data directly */

            var expectedUser = UserFactory.GenerateUserUsingEmail();
            expectedUser = await _fixture.InjectUser(expectedUser);

            /* get data from service */

            var actualUser = await _fixture.UserRepository
                .Get(expectedUser.Id);

            /* check if the data are equals */

            actualUser.Should().NotBeNull();

            actualUser.Should().BeEquivalentTo(expectedUser, options =>
            {
                options.Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation)).WhenTypeIs<DateTime>();
                return options;
            });
        }

        [Fact]
        public async Task GetInexistentUser()
        {
            var id = ObjectId.GenerateNewId();

            /* delete data directly to ensure there is no data by id exist */
            await _fixture.DeleteUser(id);

            /* get the data using service */

            var actualUser = await _fixture.UserRepository.Get(ObjectId.GenerateNewId());

            /* check if the data is null (not found) */

            actualUser.Should().BeNull();
        }

        [Fact]
        public async Task Insert()
        {
            /* insert data */

            var expectedUser = UserFactory.GenerateUserUsingEmail();

            expectedUser = await _fixture.UserRepository.Insert(expectedUser);

            /* get data directly */

            var actualUser = _fixture.GetUser(expectedUser.Id);

            /* check if the data are equals */

            actualUser.Should().NotBeNull();

            actualUser.Should().BeEquivalentTo(expectedUser, options =>
            {
                options.Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation)).WhenTypeIs<DateTime>();
                return options;
            });
        }

        [Fact]
        public async Task Update()
        {
            /* insert data directly */

            var expectedUser = UserFactory.GenerateUserUsingEmail();
            expectedUser = await _fixture.InjectUser(expectedUser);

            /* update data */
            expectedUser.UpdateInfo(
                firstName: expectedUser.FirstName + "abc",
                lastName: expectedUser.LastName + "def");

            await _fixture.UserRepository.Update(expectedUser);

            /* get data directly */

            var actualUser = _fixture.GetUser(expectedUser.Id);

            /* check if the data are equals */

            actualUser.Should().NotBeNull();

            actualUser.Should().BeEquivalentTo(expectedUser, options =>
            {
                options.Using<DateTime>(ctx => ctx.Subject.Should().BeCloseTo(ctx.Expectation)).WhenTypeIs<DateTime>();
                return options;
            });
        }
    }

}
