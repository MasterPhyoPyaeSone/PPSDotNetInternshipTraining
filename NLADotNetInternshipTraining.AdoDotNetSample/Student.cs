namespace NLADotNetInternshipTraining.AdoDotNetSample;

public class Student
{
    // Maps to [StudentId] [int] IDENTITY(1,1)
    public int StudentId { get; set; }

    // Maps to [StudentNo] [nvarchar](50)
    public string StudentNo { get; set; } = string.Empty;

    // Maps to [StudentName] [nvarchar](50)
    public string StudentName { get; set; } = string.Empty;

    // Maps to [FatherName] [nvarchar](50)
    public string FatherName { get; set; } = string.Empty;

    // Maps to [Address] [nvarchar](max)
    public string Address { get; set; } = string.Empty;

    // Maps to [DateOfBirth] [datetime]
    public DateTime DateOfBirth { get; set; }

    // Maps to [IsDelete] [bit]
    public bool IsDelete { get; set; }

    // Maps to [CreatedDateTime] [datetime]
    public DateTime CreatedDateTime { get; set; }

    // Maps to [CreatedBy] [varbinary](50)
    // SQL varbinary maps to a byte array in C#
    public string CreatedBy { get; set; } = string.Empty;

    // Maps to [ModifiedDateTime] [datetime] NULL
    // The '?' allows this property to accept null values
    public DateTime? ModifiedDateTime { get; set; }

    // Maps to [ModifiedBy] [varchar](50) NULL
    public string? ModifiedBy { get; set; }
}
