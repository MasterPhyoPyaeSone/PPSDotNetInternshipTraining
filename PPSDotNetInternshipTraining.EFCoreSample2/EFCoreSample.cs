using Microsoft.SqlServer.Server;
using PPSDotNetInternshipTraining.EFCoreSample2.Database.AppDbContextModels;

namespace PPSDotNetInternshipTraining.EFCoreSample2;

public class EFCoreSample2
{
    private readonly AppDbContext _db;
    public EFCoreSample2()
    {
        _db = new AppDbContext();
    }

    public void Read()
    {
        var Students = _db.TblStudents.Where(s => s.IsDelete == false).ToList();
        foreach (var student in Students)
        {
            Console.WriteLine($"Id: {student.StudentId}, Name: {student.StudentName}, Father Name: {student.FatherName}, DOB: {student.DateOfBirth}");
        }
    }

    public void Create()
    {
        TblStudent student = new TblStudent
        {
            StudentNo = "S-0001",
            StudentName = "Aung Aung",
            FatherName = "U Aung Kyaw",
            Address = "Yangon",
            DateOfBirth = new DateOnly(2000, 1, 1),
            IsDelete = false,
            CreatedBy = "Admin",
            CreatedDateTime = DateTime.Now
        };
        _db.TblStudents.Add(student);
        int result = _db.SaveChanges();
        Console.WriteLine(result > 0 ? "Data created successfully" : "Failed to create data");
    }

    public void Edit()
    {
        var student = _db.TblStudents.FirstOrDefault(s => s.StudentName == "Aung Aung");
        if (student == null)
        {
            Console.WriteLine("Data not found");
            return;
        }
        student.StudentName = "Hla Hla";
        student.ModifiedBy = "Admin";
        student.ModifiedDateTime = DateTime.Now;
        int result = _db.SaveChanges();
        Console.WriteLine(result > 0 ? "Data edited successfully" : "Failed to edit data");
    }

    public void Update()
    {
        var student = _db.TblStudents.FirstOrDefault(s => s.StudentName == "Hla Hla");
        if (student == null)
        {
            Console.WriteLine("Data not found");
            return;
        }
        student.StudentName = "Phyo Lay";
        student.ModifiedBy = "Admin";
        student.ModifiedDateTime = DateTime.Now;
        int result = _db.SaveChanges();
        Console.WriteLine(result > 0 ? "Data updated successfully" : "Failed to update data");
    }

    public void Delete()
    {
        var student = _db.TblStudents.FirstOrDefault(s => s.StudentName == "Phyo Lay" && s.StudentId == 2002);
        if (student == null)
        {
            Console.WriteLine("Data not found");
            return;
        }
        student.IsDelete = true;
        student.ModifiedBy = "Admin";
        student.ModifiedDateTime = DateTime.Now;
        int result = _db.SaveChanges();
        Console.WriteLine(result > 0 ? "Data deleted successfully" : "Failed to delete data");
    }
}