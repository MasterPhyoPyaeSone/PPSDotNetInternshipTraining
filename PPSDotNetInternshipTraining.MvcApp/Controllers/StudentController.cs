using Microsoft.AspNetCore.Mvc;
using PPSDotNetInternshipTraining.EFCoreSample2.Database.AppDbContextModels;

public class StudentController : Controller
{
    private readonly AppDbContext _dbContext;
    public StudentController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

   public IActionResult Index(int? PageNumber)
{
    int PageSize = 3;
    int CurrentPage = PageNumber ?? 1;
    
    // ၁။ တကယ့် ပြသမယ့် Active Data Query ကို အရင်ဆောက်မယ်
    // var activeStudents = _dbContext.TblStudents.Where(s => s.IsDelete == false);

    // ၂။ ပြသမယ့် Active ဒေတာအရေအတွက်ကိုပဲ အခြေခံပြီး Total Page တွက်မယ်
    int totalItems = _dbContext.TblStudents.Count();
    int totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

    // ၃။ Skip နဲ့ Take သုံးပြီး ဆွဲထုတ်မယ်
    var students = _dbContext.TblStudents
        .OrderByDescending(a => a.CreatedDateTime)
        .Skip((CurrentPage - 1) * PageSize  )
        .Take(PageSize)
        .Select(s => new StudentResponseModel
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
    ViewBag.CurrentPage = CurrentPage;

    return View(students);
}
}