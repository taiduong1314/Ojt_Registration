using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Students.CustomizeRequestObject
{
    public class AddNewStudent
    {
        public string Phone { get; set; }
        public DateTime? Birthday { get; set; }
        public int? Term { get; set; }
        public int? Credit { get; set; }
        public double? Gpa { get; set; }
        public string MajorName { get; set; }
        public string StudentCode { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Gender { get; set; }
    }
}
