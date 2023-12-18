using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Students.CustomizeResponseObject
{
    public class StudentDetailReturn
    {
        public string Major { get; set; }
        public int? Term { get; set; }
        public string StuCode { get; set; }
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public double? Gpa { get; set; }
    }
}
