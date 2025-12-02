namespace ApiTests.DTO
{
    // Represents the JSON structure of a Post object used by the API.
    // This model is used for serialization and deserialization of
    // request and response bodies in the JSONPlaceholder posts endpoints.
    public class PostDto
    {
        public int UserId { get; set; }
        public int Id { get; set; }   
        public string Title { get; set; }
        public string Body { get; set; }
    }
}