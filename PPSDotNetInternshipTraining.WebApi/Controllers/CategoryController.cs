using Microsoft.AspNetCore.Mvc;
using PPSDotNetInternshipTraining.WebApi.Database.AppDbContextModels;
using PPSDotNetInternshipTraining.WebApi.Models;

namespace PPSDotNetInternshipTraining.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
   private readonly AppDbContext _db;
    public CategoryController()
    {
        _db = new AppDbContext();
    }

    [HttpGet]
    public IActionResult GetCategories()
    {
        var categories = _db.Categories
            .Where(c => c.IsDeleted == false)
            .Select(c => new CategoryResponseModel
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            })
            .ToList();

        return Ok(categories);
    }

    [HttpGet("{id}")]
    public IActionResult GetCategoryById(int id)
    {
        var category = _db.Categories
            .Where(c => c.CategoryId == id && c.IsDeleted == false)
            .Select(c => new CategoryResponseModel
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            })
            .FirstOrDefault();

        if (category is null)
        {
            return NotFound(new ApiResponseModel { IsSuccess = false, Message = "Category not found." });
        }

        return Ok(category);
    }

    [HttpPost]
    public IActionResult CreateCategory(CategoryRequestModel request)
    {
        var newCategory = new Category
        {
            CategoryName = request.CategoryName
        };

        _db.Categories.Add(newCategory);
        var result = _db.SaveChanges();

        return result > 0 
            ? Ok(new ApiResponseModel { IsSuccess = true, Message = "Category created successfully." })
            : BadRequest(new ApiResponseModel { IsSuccess = false, Message = "Failed to create category." });
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCategory(int id, CategoryRequestModel request)
    {
        var categoryFromDb = _db.Categories.FirstOrDefault(c => c.CategoryId == id && c.IsDeleted == false);

        if (categoryFromDb is null)
        {
            return NotFound(new ApiResponseModel { IsSuccess = false, Message = "Category not found." });
        }

        categoryFromDb.CategoryName = request.CategoryName;

        var result = _db.SaveChanges();

        return Ok(new ApiResponseModel
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Category updated successfully." : "No changes were made.",
           
        });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteCategory(int id)
    {
        var categoryFromDb = _db.Categories.FirstOrDefault(c => c.CategoryId == id && c.IsDeleted == false);

        if (categoryFromDb is null)
        {
            return NotFound(new ApiResponseModel { IsSuccess = false, Message = "Category not found." });
        }

        categoryFromDb.IsDeleted = true;

        var result = _db.SaveChanges();

        return Ok(new ApiResponseModel
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Category deleted successfully." : "Failed to delete category."
        });
    }
}