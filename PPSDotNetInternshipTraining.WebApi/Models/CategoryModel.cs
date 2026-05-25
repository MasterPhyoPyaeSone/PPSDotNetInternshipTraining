namespace PPSDotNetInternshipTraining.WebApi.Models;

public class CategoryRequestModel
{
    public string CategoryName { get; set; } = null!;
}

public class CategoryResponseModel
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;
}
