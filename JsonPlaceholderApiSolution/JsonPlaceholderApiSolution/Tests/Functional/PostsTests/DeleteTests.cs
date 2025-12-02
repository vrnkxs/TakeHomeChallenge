using System.Net;
using FluentAssertions;

namespace ApiTests.Tests
{
    /*
     * Test class for DELETE operations against the JSONPlaceholder API.
     * Includes both positive and negative test scenarios.
     */
    public class DeleteTests : BaseTest
    {
        /*
         * Positive test:
         * Sends a DELETE request to a valid endpoint.
         * Expects a 200 OK response.
         */
        [Test]
        public async Task DeletePost_ShouldReturn200()
        {
            var response = await _api.Delete($"posts/{_userId}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        /*
         * Negative test:
         * Sends a DELETE request to an invalid endpoint.
         * Expects a 404 Not Found response.
         */
        [Test]
        public async Task DeletePost_InvalidEndpoint_ShouldReturn404()
        {
            var response = await _api.Delete("postsssss/1");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}