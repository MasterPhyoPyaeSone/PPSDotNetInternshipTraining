using Microsoft.AspNetCore.Mvc;
using PPSDotNetInternshipTraining.WebApi.Database.AppDbContextModels;
using PPSDotNetInternshipTraining.WebApi.Models;

namespace PPSDotNetInternshipTraining.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MemberController : ControllerBase
{
    private readonly AppDbContext _db;

    public MemberController()
    {
        _db = new AppDbContext();
    }

    [HttpGet]
    public IActionResult GetMembers()
    {
        var members = _db.Members
            .Where(m => m.IsDeleted == false)
            .Select(m => new MemberResponseModel
            {
                MemberId = m.MemberId,
                FullName = m.FullName,
                Email = m.Email,
                PhoneNumber = m.PhoneNumber,
                JoinDate = m.JoinDate
            })
            .ToList();

        return Ok(members);
    }

    [HttpGet("{id}")]
    public IActionResult GetMemberById(int id)
    {
        var member = _db.Members
            .Where(m => m.MemberId == id && m.IsDeleted == false)
            .Select(m => new MemberResponseModel
            {
                MemberId = m.MemberId,
                FullName = m.FullName,
                Email = m.Email,
                PhoneNumber = m.PhoneNumber,
                JoinDate = m.JoinDate
            })
            .FirstOrDefault();

        if (member is null)
        {
            return NotFound(new ApiResponseModel { IsSuccess = false, Message = "Member not found." });
        }

        return Ok(member);
    }

    [HttpPost]
    public IActionResult CreateMember(MemberRequestModel request)
    {
        var newMember = new Member
        {
            FullName = request.FullName,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            JoinDate = request.JoinDate
        };

        _db.Members.Add(newMember);
        var result = _db.SaveChanges();

        return result > 0 
            ? Ok(new ApiResponseModel { IsSuccess = true, Message = "Member created successfully." })
            : BadRequest(new ApiResponseModel { IsSuccess = false, Message = "Failed to create member." });
    }

    [HttpPatch("{id}")]
    public IActionResult PatchMember (int id, MemberPatchModel request)
    {
        var memberFromDb = _db.Members.FirstOrDefault(m => m.MemberId == id && m.IsDeleted == false);

        if (memberFromDb is null)
        {
            return NotFound(new ApiResponseModel { IsSuccess = false, Message = "Member not found." });
        }

        int count = 0;

        if (!string.IsNullOrEmpty(request.FullName))
        {
            count++;
            memberFromDb.FullName = request.FullName;
        }

        if (!string.IsNullOrEmpty(request.Email))
        {
            count++;
            memberFromDb.Email = request.Email;
        }

        if (!string.IsNullOrEmpty(request.PhoneNumber))
        {
            count++;
            memberFromDb.PhoneNumber = request.PhoneNumber;
        }

        // if (request.JoinDate.HasValue && request.JoinDate.Value > DateTime.MinValue)
        // {
        //     count++;
        //     memberFromDb.JoinDate = request.JoinDate.Value;
        // }

        var result = _db.SaveChanges();

        return Ok(new ApiResponseModel
        {
            IsSuccess = result > 0,
            Message = result > 0 ? $"{count} field(s) updated successfully." : "No changes were made."
        });
    }

    [HttpPut("{id}")]
    public IActionResult UpdateMember(int id, MemberRequestModel request)
    {
        var memberFromDb = _db.Members.FirstOrDefault(m => m.MemberId == id && m.IsDeleted == false);

        if (memberFromDb is null)
        {
            return NotFound(new ApiResponseModel { IsSuccess = false, Message = "Member not found." });
        }

        memberFromDb.FullName = request.FullName;
        memberFromDb.Email = request.Email;
        memberFromDb.PhoneNumber = request.PhoneNumber;
        memberFromDb.JoinDate = request.JoinDate;

        var result = _db.SaveChanges();

        return Ok(new ApiResponseModel
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Member updated successfully." : "No changes were made.",
        });
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMember(int id)
    {
        var memberFromDb = _db.Members.FirstOrDefault(m => m.MemberId == id && m.IsDeleted == false);

        if (memberFromDb is null)
        {
            return NotFound(new ApiResponseModel { IsSuccess = false, Message = "Member not found." });
        }

        memberFromDb.IsDeleted = true;

        var result = _db.SaveChanges();

        return Ok(new ApiResponseModel
        {
            IsSuccess = result > 0,
            Message = result > 0 ? "Member deleted successfully." : "Failed to delete member."
        });
    }
}