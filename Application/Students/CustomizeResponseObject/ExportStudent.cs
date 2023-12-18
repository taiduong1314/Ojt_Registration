using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Students.CustomizeResponseObject
{
    public class ExportStudent
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public DateTime? Birthday { get; set; }
        public int Term { get; set; }
        public int Credit { get; set; }
        public double? Gpa { get; set; }
        public string StudentCode { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Gender { get; set; }
        public string WorkingStatus { get; set; }
    }
}
