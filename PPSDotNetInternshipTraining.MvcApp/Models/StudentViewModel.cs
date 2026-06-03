
public class StudetntRequestModel
{
    public int studenId { get; set; }
    public string studentName { get; set; }
    public string fatherName { get; set; }
    public string address { get; set; }
    public DateTime dateOfBirth { get; set; }
    public bool isDelete { get; set; } = false;
    public DateTime createdDateTime { get; set; } = DateTime.Now;
    public string createdBy { get; set; } = "Admin";

}

public class StudentResponseModel
{
    public int StudentId { get; set; }
    public string StudentName { get; set; }
    public string FatherName { get; set; }
    public string Address { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public bool IsDelete { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? ModifiedDateTime { get; set; }
    public string? ModifiedBy { get; set; }
}



//  ,[StudentNo]
//       ,[StudentName]
//       ,[FatherName]
//       ,[Address]
//       ,[DateOfBirth]
//       ,[IsDelete]
//       ,[CreatedDateTime]
//       ,[CreatedBy]
//       ,[ModifiedDateTime]
//       ,[ModifiedBy]