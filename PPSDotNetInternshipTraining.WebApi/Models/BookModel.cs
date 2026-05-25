using static PPSDotNetInternshipTraining.WebApi.Database.AppDbContextModels.Book;

namespace PPSDotNetInternshipTraining.WebApi.Models;

public class BookCreateRequestModel
{
    public enum BookStatus
    {
        Available,
        Borrowed,
        Lost,
        Damaged
    }
    // public int BookId { get; set; }
    public string Title { get; set; } = null!;
    public string ISBN { get; set; } =null!;
    public int PublishedYear { get; set; }
    public BookStatus Status { get; set; } = BookStatus.Available;
    // public bool IsDeleted { get; set; } = false;
    public int AuthorId { get; set; }
    public int CategoryId { get; set; }
}

public class BookCreateResponseModel
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = null!;
}

public class BookPatchRequestModel
{
    public string? Title { get; set; }
    public string? ISBN { get; set; }
    public int? PublishedYear { get; set; }
    public int? AuthorId { get; set; }
    public int? CategoryId { get; set; }
    public BookStatus? Status { get; set; }
}

public class BookUpdateRequestModel
{
    public string Title { get; set; } = null!;
    public string ISBN { get; set; } = null!;
    public int PublishedYear { get; set; }
    public int AuthorId { get; set; }
    public int CategoryId { get; set; }
    public BookStatus Status { get; set; }
}

public class BookUpdateResponseModel
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; } = null!;
}