using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPSDotNetInternshipTraining.WebApi.Database.AppDbContextModels;
using PPSDotNetInternshipTraining.WebApi.Models;
using static PPSDotNetInternshipTraining.WebApi.Database.AppDbContextModels.Book;

namespace PPSDotNetInternshipTraining.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BorrowingController : ControllerBase
{
    private readonly AppDbContext _db;

    public BorrowingController()
    {
        _db = new AppDbContext();
    }

    [HttpGet]
    public IActionResult GetBorrowings()
    {
        var borrowings = _db.Borrowings
            .Include(b => b.Book)   
            .Include(b => b.Member) 
            .Where(b => b.IsDeleted == false)
            .Select(b => new BorrowingResponseModel
            {
                BorrowingId = b.BorrowingId,
                BorrowDate = b.BorrowDate.ToString("dd-MMM-yyyy"),
                DueDate = b.DueDate.ToString("dd-MMM-yyyy"),
                ReturnDate = b.ReturnDate.HasValue ? b.ReturnDate.Value.ToString("dd-MMM-yyyy") : null,  
                BookTitle = b.Book.Title,
                MemberName = b.Member.FullName
            })
            .ToList();

        return Ok(borrowings);
    }

    [HttpGet("{id}")]
    public IActionResult GetBorrowingById(int id)
    {
        var borrowing = _db.Borrowings
            .Include(b => b.Book)
            .Include(b => b.Member)
            .Where(b => b.BorrowingId == id && b.IsDeleted == false)
            .Select(b => new BorrowingResponseModel
            {
                BorrowingId = b.BorrowingId,
                BorrowDate = b.BorrowDate.ToString("dd-MMM-yyyy"),
                DueDate = b.DueDate.ToString("dd-MMM-yyyy"),
                ReturnDate = b.ReturnDate.HasValue ? b.ReturnDate.Value.ToString("dd-MMM-yyyy") : null,  
                BookId = b.BookId,
                BookTitle = b.Book.Title,
                MemberId = b.MemberId,
                MemberName = b.Member.FullName
            })
            .FirstOrDefault();

        if (borrowing is null)
        {
            return NotFound(new ApiResponseModel { IsSuccess = false, Message = "Borrowing record not found." });
        }

        return Ok(borrowing);
    }

    [HttpPost]
    public IActionResult CreateBorrowing(BorrowingRequestModel request)
    {
        var book = _db.Books.FirstOrDefault(b => b.BookId == request.BookId && b.IsDeleted == false);
        var member = _db.Members.FirstOrDefault(m => m.MemberId == request.MemberId && m.IsDeleted == false);

        if (book is null || member is null)
        {
            return BadRequest(new ApiResponseModel 
            { 
                IsSuccess = false, 
                Message = "Invalid BookId or MemberId. Please check if they exist." 
            });
        }
        
        if (book.Status != BookStatus.Available) 
        {
            return BadRequest(new ApiResponseModel
            {
                IsSuccess = false,
                Message = "This book is currently borrowed or not available."
            });
        }

        var newBorrowing = new Borrowing
        {
            BorrowDate = request.BorrowDate,
            DueDate = request.DueDate,
            BookId = request.BookId,
            MemberId = request.MemberId
        };

        _db.Borrowings.Add(newBorrowing);
        book.Status = BookStatus.Borrowed;
        var result = _db.SaveChanges();

        return result > 0 
            ? Ok(new ApiResponseModel { IsSuccess = true, Message = "Borrowing record created successfully." })
            : BadRequest(new ApiResponseModel { IsSuccess = false, Message = "Failed to create borrowing record." });
    }

    [HttpPatch("{id}")]
    public IActionResult PatchBorrowing(int id, BorrowingPatchRequestModel request)
    {
        var borrowingFromDb = _db.Borrowings.FirstOrDefault(b => b.BorrowingId == id && b.IsDeleted == false);

        if (borrowingFromDb is null)
        {
            return NotFound(new ApiResponseModel { IsSuccess = false, Message = "Borrowing record not found." });
        }
        borrowingFromDb.ReturnDate = request.ReturnDate;
        var result = _db.SaveChanges();
        return Ok(new ApiResponseModel
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Book returned successfully." : "No changes were made."
        });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBorrowing(int id)
    {
        var borrowingFromDb = _db.Borrowings.FirstOrDefault(b => b.BorrowingId == id && b.IsDeleted == false);

        if (borrowingFromDb is null)
        {
            return NotFound(new ApiResponseModel { IsSuccess = false, Message = "Borrowing record not found." });
        }

        borrowingFromDb.IsDeleted = true;

        var result = _db.SaveChanges();

        return Ok(new ApiResponseModel
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Borrowing record deleted successfully." : "Failed to delete record."
        });
    }
}