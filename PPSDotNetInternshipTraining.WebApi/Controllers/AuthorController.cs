using Microsoft.AspNetCore.Mvc;
using PPSDotNetInternshipTraining.WebApi.Database.AppDbContextModels;
using PPSDotNetInternshipTraining.WebApi.Models;

namespace PPSDotNetInternshipTraining.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController : ControllerBase
{
     private readonly AppDbContext _db;
    public AuthorController()
    {
        _db = new AppDbContext();
    }

    [HttpGet]
    public IActionResult GetAuthors()
    {
        var authors = _db.Authors
            .Where(a => a.IsDeleted == false)
            .Select(a => new AuthorResponseModel
            {
                AuthorId = a.AuthorId,
                Name = a.Name,
                Bio = a.Bio
            })
            .ToList(); 

        return Ok(authors);
    }

    [HttpGet("{id}")]
    public IActionResult GetAuthorById(int id)
    {
        var author = _db.Authors
            .Where(a => a.AuthorId == id && a.IsDeleted == false)
            .Select(a => new AuthorResponseModel
            {
                AuthorId = a.AuthorId,
                Name = a.Name,
                Bio = a.Bio
            })
            .FirstOrDefault(); 

        if (author is null)
        {
            return NotFound(new ApiResponseModel { IsSuccess = false, Message = "Author not found." });
        }

        return Ok(author);
    }

    [HttpPost]
    public IActionResult CreateAuthor(AuthorRequestModel request)
    {
        var newAuthor = new Author
        {
            Name = request.Name,
            Bio = request.Bio
        };

        _db.Authors.Add(newAuthor);
        
        var result = _db.SaveChanges(); 

        return result > 0 
            ? Ok(new ApiResponseModel { IsSuccess = true, Message = "Author created successfully." })
            : BadRequest(new ApiResponseModel { IsSuccess = false, Message = "Failed to create author." });
    }

    [HttpPut("{id}")]
    public IActionResult UpdateAuthor(int id, AuthorRequestModel request)
    {
        var authorFromDb = _db.Authors.FirstOrDefault(a => a.AuthorId == id && a.IsDeleted == false);

        if (authorFromDb is null)
        {
            return NotFound(new ApiResponseModel { IsSuccess = false, Message = "Author not found." });
        }

        authorFromDb.Name = request.Name;
        authorFromDb.Bio = request.Bio;

        var result = _db.SaveChanges();

        return Ok(new ApiResponseModel
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Author updated successfully." : "No changes were made.",
            // Data = new AuthorResponseModel
            // {
            //     AuthorId = authorFromDb.AuthorId,
            //     Name = authorFromDb.Name,
            //     Bio = authorFromDb.Bio
            // }
        });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAuthor(int id)
    {
        var authorFromDb = _db.Authors.FirstOrDefault(a => a.AuthorId == id && a.IsDeleted == false);

        if (authorFromDb is null)
        {
            return NotFound(new ApiResponseModel { IsSuccess = false, Message = "Author not found." });
        }

        authorFromDb.IsDeleted = true;

        var result = _db.SaveChanges();

        return Ok(new ApiResponseModel
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Author deleted successfully." : "Failed to delete author."
        });
    }
}