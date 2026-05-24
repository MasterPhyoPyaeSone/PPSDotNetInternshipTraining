
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPSDotNetInternshipTraining.WebApi.Database.AppDbContextModels;
using PPSDotNetInternshipTraining.WebApi.Models;


namespace PPSDotNetInternshipTraining.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    private readonly AppDbContext _db;
    public BookController()
    {
        _db = new AppDbContext();
    }
    [HttpGet()]
    public IActionResult GetBook()
    {
        var booksFromDb = _db.Books
            .Include(b => b.Author)
            .Include(b => b.Category)
            .Where(b => b.IsDeleted == false)
            .ToList();

        var books = booksFromDb.Select(b => new
        {
            b.BookId,
            b.Title,
            b.Isbn,
            b.PublishedYear,
            Status = b.Status.ToString(),
            AuthorName = b.Author.Name,
            CategoryName = b.Category.CategoryName
        });

        return Ok(books);
    }
    [HttpGet("{id}")]
    public IActionResult GetBookById(int id)
    {
        var bookFromDb = _db.Books
            .Include(b => b.Author)
            .Include(b => b.Category)
            .FirstOrDefault(b => b.BookId == id && b.IsDeleted == false);

        if (bookFromDb == null)
        {
            return NotFound(new { Message = "Book not found" });
        }

        var book = new
        {
            bookFromDb.BookId,
            bookFromDb.Title,
            bookFromDb.Isbn,
            bookFromDb.PublishedYear,
            Status = bookFromDb.Status.ToString(),
            AuthorName = bookFromDb.Author?.Name,
            CategoryName = bookFromDb.Category?.CategoryName
        };

        return Ok(book);
    }
    [HttpPost]
    public IActionResult CreateBook(BookCreateRequestModel bookModel)
    {
        bool isIsbnExist = _db.Books.Any(b => b.Isbn == bookModel.ISBN);
        if (isIsbnExist)
        {
            return BadRequest(new BookCreateResponseModel
            {
                IsSuccess = false,
                Message = "A book with this ISBN already exists."
            });
        }

        var newBook = new Book
        {
            Title = bookModel.Title,
            Isbn = bookModel.ISBN,
            PublishedYear = bookModel.PublishedYear,
            AuthorId = bookModel.AuthorId,
            CategoryId = bookModel.CategoryId
        };

        _db.Books.Add(newBook);
        int result = _db.SaveChanges();

        return result > 0 ? Ok(new BookCreateResponseModel
        {
            IsSuccess = true,
            Message = "Book created successfully"
        }) : BadRequest(new BookCreateResponseModel
        {
            IsSuccess = false,
            Message = "Failed to create book"
        });
    }

    [HttpPatch("{id}")]
    public IActionResult PatchBook(int id, BookPatchRequestModel requestModel)
    {
        var item = _db.Books.FirstOrDefault(x => x.BookId == id && x.IsDeleted == false);

        if (item is null)
        {
            return NotFound(new BookUpdateResponseModel
            {
                IsSuccess = false,
                Message = "Book not found"
            });
        }

        int count = 0;

        if (!string.IsNullOrEmpty(requestModel.Title))
        {
            count++;
            item.Title = requestModel.Title;
        }

        if (!string.IsNullOrEmpty(requestModel.ISBN))
        {
            count++;
            item.Isbn = requestModel.ISBN;
        }

        if (requestModel.PublishedYear.HasValue)
        {
            count++;
            item.PublishedYear = requestModel.PublishedYear.Value;
        }

        if (requestModel.AuthorId.HasValue)
        {
            count++;
            item.AuthorId = requestModel.AuthorId.Value;
        }

        if (requestModel.CategoryId.HasValue)
        {
            count++;
            item.CategoryId = requestModel.CategoryId.Value;
        }

        if (requestModel.Status.HasValue)
        {
            count++;
            item.Status = requestModel.Status.Value;
        }

        if (count == 0)
        {
            return BadRequest(new BookUpdateResponseModel
            {
                IsSuccess = false,
                Message = "Invalid data. No changes provided."
            });
        }

        var result = _db.SaveChanges();

        return Ok(new BookUpdateResponseModel
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Book updated successfully" : "Failed to update book",
        });
    }

    [HttpPut("{id}")]
    public IActionResult PutBook(int id, BookUpdateRequestModel requestModel)
    {
        var item = _db.Books.FirstOrDefault(x => x.BookId == id && x.IsDeleted == false);

        if (item is null)
        {
            return NotFound(new BookUpdateResponseModel
            {
                IsSuccess = false,
                Message = "Book not found"
            });
        }

        bool isAuthorExist = _db.Authors.Any(a => a.AuthorId == requestModel.AuthorId);
        if (!isAuthorExist)
        {
            return BadRequest(new BookUpdateResponseModel
            {
                IsSuccess = false,
                Message = "The provided AuthorId does not exist."
            });
        }

        bool isCategoryExist = _db.Categories.Any(c => c.CategoryId == requestModel.CategoryId);
        if (!isCategoryExist)
        {
            return BadRequest(new BookUpdateResponseModel
            {
                IsSuccess = false,
                Message = "The provided CategoryId does not exist."
            });
        }

        item.Title = requestModel.Title;
        item.Isbn = requestModel.ISBN;
        item.PublishedYear = requestModel.PublishedYear;
        item.AuthorId = requestModel.AuthorId;
        item.CategoryId = requestModel.CategoryId;
        item.Status = requestModel.Status;

        var result = _db.SaveChanges();
        return Ok(new BookUpdateResponseModel
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Book updated successfully" : "Failed to update book",
            // Data = new 
            // {
            //     BookId = item.BookId,
            //     Title = item.Title,
            //     ISBN = item.Isbn,
            //     PublishedYear = item.PublishedYear,
            //     AuthorId = item.AuthorId,
            //     CategoryId = item.CategoryId,
            //     Status = item.Status.ToString()
            // }
        });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(int id)
    {
        var item = _db.Books.FirstOrDefault(x => x.BookId == id && x.IsDeleted == false);

        if (item is null)
        {
            return NotFound(new BookUpdateResponseModel
            {
                IsSuccess = false,
                Message = "Book not found"
            });
        }

        item.IsDeleted = true;
        var result = _db.SaveChanges();

        return Ok(new BookUpdateResponseModel
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Book deleted successfully" : "Failed to delete book"
        });
    }
}