
using static PPSDotNetInternshipTraining.EFCoreSample.AppDbContext;

namespace PPSDotNetInternshipTraining.EFCoreSample;

public class EFCoreSample
{
    private readonly AppDbContext _db;

    public EFCoreSample(AppDbContext db)
    {
        _db = db;
    }
    public void Read()
    {
        List<Student> lst = _db.Students.Where(x => x.IsDelete == false).ToList();
        foreach (Student item in lst)
        {
            System.Console.WriteLine($"StudentId: {item.StudentId}, StudentNo: {item.StudentNo}, StudentName: {item.StudentName},FatherName: {item.FatherName}");
        }

    }

    public void Edit()
    {
        Student? item = _db.Students.Where(x => x.StudentName == "Aung Aung").FirstOrDefault();
        if (item is null)
        {
            System.Console.WriteLine("Data not found");
            return;
        }
        item.StudentName = "Phyo Aung";
        item.ModifiedBy = "Admin";
        item.ModifiedDateTime = DateTime.Now;
        int result = _db.SaveChanges();
        System.Console.WriteLine(result > 0 ? "Data Edited successfully" : "Failed to edit data");
    }

    public void Update()
    {
        Student? item = _db.Students.Where(x => x.StudentName == "Phyo Aung").FirstOrDefault();
        if (item is null)
        {
            System.Console.WriteLine("Data not found");
            return;
        }
        item.StudentName = "Phyo Lay";
        item.ModifiedBy = "Admin";
        item.ModifiedDateTime = DateTime.Now;
        int result = _db.SaveChanges();
        System.Console.WriteLine(result > 0 ? "Data updated successfully" : "Failed to update data");
    }

    public void Create()
    {
        Student student = new Student
        {
            StudentNo = "S-0001",
            StudentName = "Aung Aung",
            FatherName = "U Aung Kyaw",
            Address = "Yangon",
            DateOfBirth = new DateTime(2000, 1, 1),
            IsDelete = false,
            CreatedBy = "Admin",
            CreatedDateTime = DateTime.Now
        };
        _db.Students.Add(student);
        int result = _db.SaveChanges();
        System.Console.WriteLine(result > 0 ? "Data Created successfully" : "Failed to create data");

    }

    public void Delete()
    {
        Student? item = _db.Students.Where(x => x.StudentId == 1004).FirstOrDefault();
        if (item is null)
        {
            System.Console.WriteLine("Data not found");
            return;
        }
        item.IsDelete = true;
        item.ModifiedBy = "Admin";
        item.ModifiedDateTime = DateTime.Now;
        int result = _db.SaveChanges();
        System.Console.WriteLine(result > 0 ? "Data deleted successfully" : "Failed to delete data");
    }

    

}