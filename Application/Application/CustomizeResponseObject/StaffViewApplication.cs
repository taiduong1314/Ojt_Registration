using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.CustomizeResponseObject
{
    public class StaffViewApplication
    {
        public int Id { get; set; }
        public string StudentCode { get; set; }
        public string Fullname { get; set; }
        public double? GPA { get; set; }
        public string CompanyName { get; set; }
        public string Status { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
