using System;
using System.Collections.Generic;

namespace PPSDotNetInternshipTraining.WebApi.Database.AppDbContextModels;

public partial class Member
{
    public int MemberId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public DateTime JoinDate { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
}
