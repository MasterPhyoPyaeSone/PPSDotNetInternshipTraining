using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PPSDotNetInternshipTraining.Blazor_HttpClient.Features.Author
{

    public class AuthorRequestModel
    {
        [Required(ErrorMessage = "Author Name is required.")]
        public string Name { get; set; } = string.Empty;

        public string? Bio { get; set; }
    }
    public class AuthorResponseModel
    {
        [JsonPropertyName("authorId")]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Bio { get; set; }
    } 

    
}