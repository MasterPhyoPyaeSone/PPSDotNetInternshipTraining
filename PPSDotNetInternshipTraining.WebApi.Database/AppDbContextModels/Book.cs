using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PPSDotNetInternshipTraining.WebApi.Database.AppDbContextModels;

public partial class Book
{
      public enum BookStatus
    {
        Available,
        Borrowed,
        Lost,
        Damaged
    }
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string Isbn { get; set; } = null!;

    public int PublishedYear { get; set; }

    // public int Status { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public BookStatus Status { get; set; } = BookStatus.Available;

    public bool IsDeleted { get; set; }

    public int AuthorId { get; set; }

    public int CategoryId { get; set; }

    [JsonIgnore]
    public virtual Author Author { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
    [JsonIgnore]
    public virtual Category Category { get; set; } = null!;
}
