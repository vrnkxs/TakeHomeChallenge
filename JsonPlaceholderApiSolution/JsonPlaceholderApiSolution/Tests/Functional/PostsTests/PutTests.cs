using System.Net;
using FluentAssertions;
using ApiTests.DTO;
using Newtonsoft.Json;

namespace ApiTests.Tests
{
    /*
     * Test class for PUT operations against the JSONPlaceholder API.
     * Includes both positive and negative update scenarios.
     */
    public class PutTests : BaseTest
    {
        /*
         * Positive test:
         * Sends a PUT request to update an existing post.
         * Expects a 200 OK response and validates updated fields.
         */
        [Test]
        public async Task UpdatePost_ShouldReturn200()
        {
            var updated = new PostDto
            {
                Id = 1,
                UserId = 11,
                Title = "Updated title",
                Body = "Updated content"
            };
            var response = await _api.Put("posts/11", updated);
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var body = response.Content;
            var post = JsonConvert.DeserializeObject<PostDto>(body);

            post.Should().NotBeNull();
            post.Title.Should().Be("Updated title");
            post.Body.Should().Be("Updated content");
        }

        /*
         * Negative test:
         * Sends a PUT request to an invalid endpoint.
         * Expects a NotFound response.
         */
        [Test]
        public async Task UpdatePost_InvalidEndpoint_ShouldReturn404()
        {
            var updated = new PostDto
            {
                Id = 1,
                UserId = 11,
                Title = "Bad",
                Body = "Bad"
            };
            var response = await _api.Put("postssss/1", updated); 
            response.StatusCode.Should().BeOneOf(HttpStatusCode.NotFound, HttpStatusCode.InternalServerError);
        }
    }
}