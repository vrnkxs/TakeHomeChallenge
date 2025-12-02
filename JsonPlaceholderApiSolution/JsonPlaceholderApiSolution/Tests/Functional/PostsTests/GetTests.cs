using System.Net;
using FluentAssertions;
using Newtonsoft.Json;
using ApiTests.DTO;

namespace ApiTests.Tests
{
    /*
     * Test class for GET operations against the JSONPlaceholder API.
     * Covers positive and negative scenarios.
     */
    public class GetTests : BaseTest
    {
        /*
         * Positive test:
         * Sends a GET request to fetch an existing post.
         * Validates status code and that the response body is not null.
         */
        [Test]
        public async Task GetPost_ShouldReturn200()
        {
            var response = await _api.Get("posts/1");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var body = response.Content;
            var post = JsonConvert.DeserializeObject<PostDto>(body);

            post.Should().NotBeNull();
            post.Id.Should().Be(1);
            post.Title.Should().NotBeNullOrEmpty();
        }

        /*
         * Negative test:
         * Sends a GET request to an invalid endpoint.
         * Expects a 404 Not Found response.
         */
        [Test]
        public async Task GetPost_InvalidEndpoint_ShouldReturn404()
        {
            var response = await _api.Get("postsssss/1"); // wrong path
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}