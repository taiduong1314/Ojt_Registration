using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Semester.CsResponse
{
    public class StudentInSemester
    {
        public string Phone { get; set; }
        public int? Term { get; set; }
        public double? Gpa { get; set; }
        public string CompanyName { get; set; }
        public string MajorName { get; set; }
        public string StudentCode { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string WorkingStatus { get; set; }
    }
}
