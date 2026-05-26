using System;
using System.Collections.Generic;

namespace PPSDotNetInternshipTraining.WebApi.Database.AppDbContextModels;

public partial class Book
{
    public int BookId { get; set; }

    public string Title { get; set; } = null!;

    public string Isbn { get; set; } = null!;

    public int PublishedYear { get; set; }

    public int Status { get; set; }

    public bool IsDeleted { get; set; }

    public int AuthorId { get; set; }

    public int CategoryId { get; set; }

    public virtual Author Author { get; set; } = null!;

    public virtual ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();

    public virtual Category Category { get; set; } = null!;
}
