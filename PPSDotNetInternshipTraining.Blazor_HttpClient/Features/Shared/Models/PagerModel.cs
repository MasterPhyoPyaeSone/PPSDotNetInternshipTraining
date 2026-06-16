namespace PPSDotNetInternshipTraining.Blazor_HttpClient.Features.Shared.Models
{
    public class PagerModel
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Action { get; set; } // ဘယ် Method ကို လှမ်းခေါ်မလဲ (ဥပမာ - "Index")
    }

    public class PaginatedResponse<T>
    {
        public List<T> Data { get; set; } = new();
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}