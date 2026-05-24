using System;
using System.Collections.Generic;

namespace PPSDotNetInternshipTraining.WebApi.Database.AppDbContextModels;

public partial class Borrowing
{
    public int BorrowingId { get; set; }

    public DateTime BorrowDate { get; set; }

    public DateTime DueDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public bool IsDeleted { get; set; }

    public int BookId { get; set; }

    public int MemberId { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Member Member { get; set; } = null!;
}
