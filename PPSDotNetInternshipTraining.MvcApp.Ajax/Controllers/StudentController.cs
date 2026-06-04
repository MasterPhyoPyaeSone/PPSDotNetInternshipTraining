using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPSDotNetInternshipTraining.EFCoreSample2.Database.AppDbContextModels;
using PPSDotNetInternshipTraining.MvcApp.Ajax.Models;

namespace PPSDotNetInternshipTraining.MvcApp.Ajax.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        // Constructor ဖြင့် AppDbContext ကို ချိတ်ဆက်ခြင်း (Dependency Injection)
        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // 1. READ (Database မှ Data များကို ယူခြင်း)
        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            // Database (TblStudents) ထဲက Data များကို UI တွင်ပြမည့် Model သို့ ပြောင်းလဲချိတ်ဆက်ပေးခြင်း
            var data = await _context.TblStudents
                                     .Where(s => s.IsDelete == false)
                                     .Select(s => new StudetntRequestModel
                                     {
                                         studentId = s.StudentId, // Database ထဲက ID
                                         studentNo = s.StudentNo, // Database ထဲက StudentNo (STU-001)
                                         studentName = s.StudentName,
                                         fatherName = s.FatherName,
                                         address = s.Address,
                                         dateOfBirth = s.DateOfBirth.Value, // DateOfBirth ကို DateTime အဖြစ် ပြောင်းလဲခြင်း (null ဖြစ်နိုင်သောအတွက် Value ကိုသုံးသည်)
                                     })
                                     .ToListAsync();

            return Json(data);
        }


        [HttpPost]
        public async Task<JsonResult> Create(StudetntRequestModel model)
        {
            try
            {
                // StudentNo အသစ်တစ်ခု ဖန်တီးခြင်း (ဥပမာ: STU-202606042339)
                // တကယ်လို့ သင်က ಬೇರೆ ပုံစံနဲ့ Generate လုပ်ချင်ရင် ဒီနေရာမှာ ပြင်ပါ
                string generatedStudentNo = "STU-" + DateTime.Now.ToString("yyyyMMddHHmmss");

                var newStudent = new TblStudent
                {
                    StudentNo = generatedStudentNo, // Error တက်နေသော Column သို့ Data ဖြည့်ခြင်း
                    StudentName = model.studentName,
                    FatherName = model.fatherName,
                    Address = model.address,
                    DateOfBirth = model.dateOfBirth,
                    IsDelete = false, // အသစ်ဖြစ်၍ ဖျက်ထားခြင်း မရှိပါ
                    CreatedDateTime = DateTime.Now,
                    CreatedBy = "Admin"
                };

                await _context.TblStudents.AddAsync(newStudent);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Successfully Created!" });
            }
            catch (Exception ex)
            {
                string actualError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Json(new { success = false, message = "Database Error: " + actualError });
            }
        }

        [HttpPost]
        public async Task<JsonResult> Update(StudetntRequestModel model)
        {
            try
            {
                // studentId ဖြင့် Database တွင် ရှာဖွေခြင်း
                var existingData = await _context.TblStudents.FindAsync(model.studentId);

                if (existingData != null)
                {
                    // Data အဟောင်းများကို အသစ်ဖြင့် အစားထိုးခြင်း (StudentNo ကို ပြင်လေ့မရှိသဖြင့် မထည့်ပါ)
                    existingData.StudentName = model.studentName;
                    existingData.FatherName = model.fatherName;
                    existingData.Address = model.address;
                    existingData.DateOfBirth = model.dateOfBirth;

                    // UpdatedDateTime, UpdatedBy စသည်ဖြင့် သင့် Table တွင်ရှိပါက ထပ်ထည့်နိုင်ပါသည်

                    await _context.SaveChangesAsync();

                    return Json(new { success = true, message = "Successfully Updated!" });
                }
                return Json(new { success = false, message = "Student Data not found for update!" });
            }
            catch (Exception ex)
            {
                string actualError = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                return Json(new { success = false, message = "Database Error: " + actualError });
            }
        }

        // 3. GET BY ID (ပြင်ဆင်ရန် Data တစ်ကြောင်းတည်း ယူခြင်း)
        [HttpGet]
        public async Task<JsonResult> GetById(int id)
        {
            var data = await _context.TblStudents.FindAsync(id);
            return Json(data);
        }

        // 4. DELETE (Database တွင် Soft Delete လုပ်ခြင်း)
        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var data = await _context.TblStudents.FindAsync(id);
            if (data != null)
            {
                data.IsDelete = true; // အမှန်တကယ်မဖျက်ဘဲ isDelete ကိုသာ true ပေးလိုက်သည်
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Successfully Deleted!" });
            }
            return Json(new { success = false, message = "Delete Failed!" });
        }
    }
}