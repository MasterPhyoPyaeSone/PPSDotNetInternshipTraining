using System;

namespace PPSDotNetInternshipTraining.MvcApp.Ajax.Models
{
    public class StudetntRequestModel
    {
        public int studentId { get; set; } 
        public string studentNo { get; set; }
        public string studentName { get; set; }
        public string fatherName { get; set; }
        public string address { get; set; }
        public DateTime dateOfBirth { get; set; }
        public bool isDelete { get; set; } = false;
        public DateTime createdDateTime { get; set; } = DateTime.Now;
        public string createdBy { get; set; } = "Admin";
    }
}