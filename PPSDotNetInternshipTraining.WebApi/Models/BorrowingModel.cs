using System.Text.Json.Serialization;

namespace PPSDotNetInternshipTraining.WebApi.Models;


public class BorrowingRequestModel
{
    public DateTime BorrowDate { get; set; }
    public DateTime DueDate { get; set; }
    // public DateTime? ReturnDate { get; set; }
    public int BookId { get; set; }
    public int MemberId { get; set; }
}


public class BorrowingResponseModel
{
    public int BorrowingId { get; set; }
    public String BorrowDate { get; set; } = null!;
    public String DueDate { get; set; } = null!;
    public string? ReturnDate { get; set; }
    [JsonIgnore]
    public int BookId { get; set; }
    public string BookTitle { get; set; } = null!;
    [JsonIgnore]
    public int MemberId { get; set; }
    public string MemberName { get; set; } = null!;
}

public class BorrowingPatchRequestModel
{
    public DateTime ReturnDate { get; set; }
}