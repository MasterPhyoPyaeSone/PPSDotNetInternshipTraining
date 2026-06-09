
public class StudentRequestModel
{
    public int studenId { get; set; }
    public string studentNo { get; set; }
    public string studentName { get; set; }
    public string fatherName { get; set; }
    public string address { get; set; }
    public DateTime dateOfBirth { get; set; }
    public bool isDelete { get; set; } = false;
    public DateTime createdDateTime { get; set; } = DateTime.Now;
    public string createdBy { get; set; } = "Admin";

}

public class StudentListResponseModel
{
    public int StudentId { get; set; }
    public string studentNo { get; set; }

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

public class StudentCreateRequestModel
{

    public string studentNo { get; set; }
    public string studentName { get; set; }
    public string fatherName { get; set; }
    public string address { get; set; }
    public DateTime dateOfBirth { get; set; }
    public bool isDelete { get; set; } = false;
    public DateTime createdDateTime { get; set; } = DateTime.Now;
    public string createdBy { get; set; } = "Admin";

}

public class StudentEditResponseModel
{
    public int StudentId { get; set; }
    public string StudentNo { get; set; }
    public string StudentName { get; set; }
    public string FatherName { get; set; }
    public string Address { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime CreatedDateTime { get; set; }
}

public class StudentEditRequestModel
{
    public int StudentId { get; set; } 
    public string StudentNo { get; set; }
    public string StudentName { get; set; }
    public string FatherName { get; set; }
    public string Address { get; set; }
    public DateTime DateOfBirth { get; set; }
}