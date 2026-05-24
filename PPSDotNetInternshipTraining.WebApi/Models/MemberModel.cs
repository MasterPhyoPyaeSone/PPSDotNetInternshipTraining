namespace PPSDotNetInternshipTraining.WebApi.Models;

public class MemberRequestModel
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public DateTime JoinDate { get; set; } 
}


public class MemberResponseModel
{
    public int MemberId { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public DateTime JoinDate { get; set; }
}