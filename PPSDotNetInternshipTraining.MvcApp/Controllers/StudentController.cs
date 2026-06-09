using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPSDotNetInternshipTraining.EFCoreSample2.Database.AppDbContextModels;

public class StudentController : Controller
{
    private readonly AppDbContext _dbContext;
    public StudentController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IActionResult Index(int CurrentPage = 1)
    {
        const int PageSize = 3;
        if (CurrentPage < 1) CurrentPage = 1;

        int totalItems = _dbContext.TblStudents.Where(s=>!s.IsDelete).Count();
        int totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

        var students = _dbContext.TblStudents
            .AsNoTracking()
            .OrderByDescending(a => a.CreatedDateTime)
            .Where(s => !s.IsDelete)
            .Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize)
            .Select(s => new StudentListResponseModel
            {
                StudentId = s.StudentId,
                StudentName = s.StudentName,
                FatherName = s.FatherName,
                Address = s.Address,
                DateOfBirth = s.DateOfBirth,
                IsDelete = s.IsDelete,
                CreatedDateTime = s.CreatedDateTime,
                CreatedBy = s.CreatedBy,
                ModifiedDateTime = s.ModifiedDateTime,
                ModifiedBy = s.ModifiedBy
            })
            .ToList();

        ViewBag.TotalPages = totalPages;
        ViewBag.PageSize = PageSize;
        ViewBag.CurrentPage = CurrentPage;

        return View(students);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(StudentCreateRequestModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        _dbContext.TblStudents.Add(new TblStudent
        {
            StudentNo = model.studentNo,
            StudentName = model.studentName,
            FatherName = model.fatherName,
            Address = model.address,
            DateOfBirth = model.dateOfBirth,
            IsDelete = model.isDelete,
            CreatedDateTime = model.createdDateTime,
            CreatedBy = model.createdBy
        });
        _dbContext.SaveChanges();
        TempData["SuccessMessage"] = "Student created successfully!";
        return RedirectToAction("Index");
    }

    public IActionResult Edit(int id)
    {
        if (id == null || id <= 0)
        {
            return NotFound();
        }
        var student = _dbContext.TblStudents.FirstOrDefault(s => s.StudentId == id && !s.IsDelete);
        if (student == null)
        {
            return NotFound();
        }
        var responseModel = new StudentEditResponseModel
        {
            StudentId = student.StudentId,
            StudentNo = student.StudentNo,
            StudentName = student.StudentName,
            FatherName = student.FatherName,
            Address = student.Address,
            DateOfBirth = student.DateOfBirth.GetValueOrDefault(),
            CreatedDateTime = student.CreatedDateTime
        };
        return View(responseModel);
    }

    [HttpPost]
    public IActionResult Edit(StudentEditRequestModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        var student = _dbContext.TblStudents.FirstOrDefault(s => s.StudentId == model.StudentId && !s.IsDelete);
        if (student == null)
        {
            return NotFound();
        }
        student.StudentNo = model.StudentNo;
        student.StudentName = model.StudentName;
        student.FatherName = model.FatherName;
        student.Address = model.Address;
        student.DateOfBirth = model.DateOfBirth;
        student.ModifiedDateTime = DateTime.Now;
        student.ModifiedBy = "Admin";

        _dbContext.SaveChanges();
        TempData["SuccessMessage"] = "Student updated successfully!";
        return RedirectToAction("Index");

    }

    [HttpPost]
    public IActionResult Delete(int id)
    {
        var student = _dbContext.TblStudents.FirstOrDefault(s => s.StudentId == id && !s.IsDelete);
        if (student == null)
        {
            return NotFound();
        }
        student.IsDelete = true;
        student.ModifiedDateTime = DateTime.Now;
        student.ModifiedBy = "Admin";
        _dbContext.SaveChanges();
        TempData["SuccessMessage"] = "Student deleted successfully!";
        return RedirectToAction("Index");
    }
    
}