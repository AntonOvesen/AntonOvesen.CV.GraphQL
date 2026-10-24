using AntonOvesen.CV.GraphQL.Core;
using AntonOvesen.CV.GraphQL.Infrastructure.DataLoaders;
using AntonOvesen.CV.GraphQL.IntegrationTests.Setup;
using FluentAssertions;
using GraphQL;
using Microsoft.Extensions.DependencyInjection;

namespace AntonOvesen.CV.GraphQL.IntegrationTests.DataLoaders
{
    public class UserDataLoaderTests(ApiWebApplicationFactory applicationFactory) : TestBase(applicationFactory)
    {
        [Fact]
        public async Task UserDataLoader_MultipleRequests_MatchesIndividuallyNoOverlap()
        {
            // Arrange
            var dataLoader = Scope.ServiceProvider.GetRequiredService<UserSearchDataLoader>();

            var userOne = new User
            {
                FirstName = "Test",
                LastName = "Testerson"
            };
            var userTwo = new User
            {
                FirstName = "John",
                LastName = "otherson"
            };
            Context.Users.AddRange([userOne, userTwo]);
            await Context.SaveChangesAsync(TestContext.Current.CancellationToken);

            List<List<User>> expected = [
                [userOne],
                [userTwo]
            ];

            // Act
            var loaderTask = dataLoader.LoadAsync(["test", "other"]);
            var result = await loaderTask.GetResultAsync(TestContext.Current.CancellationToken);

            // Assert
            result.Should().BeEquivalentTo(expected);
        }
    }
}