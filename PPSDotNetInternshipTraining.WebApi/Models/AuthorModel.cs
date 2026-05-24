namespace PPSDotNetInternshipTraining.WebApi.Models;

public class AuthorRequestModel
{
    public string Name { get; set; } = null!;
    public string? Bio { get; set; } 
}

public class AuthorResponseModel
{
    public int AuthorId { get; set; }
    public string Name { get; set; } = null!;
    public string? Bio { get; set; }
}

public class ApiResponseModel
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = string.Empty;
    // public object? Data { get; set; }
}