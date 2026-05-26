using System;
using System.Collections.Generic;

namespace PPSDotNetInternshipTraining.WebApi.Database.AppDbContextModels;

public partial class Fine
{
    public int FineId { get; set; }

    public decimal Amount { get; set; }

    public bool IsPaid { get; set; }

    public bool IsDeleted { get; set; }

    public int BorrowingId { get; set; }

    public virtual Borrowing Borrowing { get; set; } = null!;
}
