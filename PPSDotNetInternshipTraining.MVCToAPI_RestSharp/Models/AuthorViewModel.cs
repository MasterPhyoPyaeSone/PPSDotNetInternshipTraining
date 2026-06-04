// Models/AuthorViewModel.cs
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PPSDotNetInternshipTraining.MVCToAPI_RestSharp.Models;

public class AuthorViewModel
{
    public int AuthorId { get; set; }  // that is must be same to Json format as "authorId": 4002, 
    public string? Name { get; set; }
    public string? Bio { get; set; }
}

public class AuthorCreateModel
{
    [Required(ErrorMessage = "Author Name is required. (နာမည် ထည့်သွင်းရန် လိုအပ်ပါသည်)")]
    [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
    public string Name { get; set; } = null!;

    [MaxLength(500, ErrorMessage = "Bio cannot exceed 500 characters.")]
    public string Bio { get; set; } = null!;
}
public class AuthorEditModel
{
    public int AuthorId { get; set; } 
    public string? Name { get; set; }
    public string? Bio { get; set; }
}