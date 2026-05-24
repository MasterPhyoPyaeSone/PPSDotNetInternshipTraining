
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace PPSDotNetInternshipTraining.EFCoreSample;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(new SqlConnectionStringBuilder()
            {
                DataSource = "localhost",
                InitialCatalog = "PPSDotNetInternshipTraining",
                UserID = "sa",
                Password = "pps@Password123",
                TrustServerCertificate = true
            }.ConnectionString);
        }
    }

    public DbSet<Student> Students { get; set; }

    [Table("Tbl_Student")]
    public class Student
    {
        public int StudentId { get; set; }
        public string? StudentNo { get; set; }
        public string? StudentName { get; set; }
        public string? FatherName { get; set; }
        public string? Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsDelete { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
    }
}


