namespace PPSDotNetInternshipTraining.MVCToAPI.Models
{
    public class PagerModel
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public string Action { get; set; } // ဘယ် Method ကို လှမ်းခေါ်မလဲ (ဥပမာ - "Index")
    }
}