using System.Net;
using FluentAssertions;
using ApiTests.DTO;
using Newtonsoft.Json;

namespace ApiTests.Tests
{
    /*
     * Test class for POST operations against the JSONPlaceholder API.
     * Includes positive and negative test scenarios for creating posts.
     */
    public class PostTests : BaseTest
    {
        /*
         * Positive test:
         * Creates a new post and expects a 201 Created response.
         * Validates that the returned post contains the correct fields.
         */
        [Test]
        public async Task CreatePost_ShouldReturn201()
        {
            var newPost = new PostDto
            {
                UserId = _userId,
                Title = "My test post",
                Body = "This is created by API test"
            };

            var response = await _api.Post("posts", newPost);
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var body = response.Content;
            var created = JsonConvert.DeserializeObject<PostDto>(body);

            created.Should().NotBeNull();
            created.Title.Should().Be("My test post");
            created.Body.Should().Be("This is created by API test");
        }

        /*
         * Negative test:
         * Sends a POST request to an invalid endpoint.
         * Expects a 404 Not Found response.
         */
        [Test]
        public async Task CreatePost_InvalidEndpoint_ShouldReturn404()
        {
            var body = new
            {
                title = "test",
                body = "invalid",
                userId = 1
            };

            var response = await _api.Post("postsss", body);
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}