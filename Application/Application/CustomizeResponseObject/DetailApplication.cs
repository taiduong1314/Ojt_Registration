using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.CustomizeResponseObject
{
    public class DetailApplication
    {
        public int Id { get; set; }
        public string StudentCode { get; set; }
        public string Fullname { get; set; }
        public string Position { get; set; }
        public string Cv { get; set; }
        public double? Gpa { get; set; }
        public string Email { get; set; }
    }
}
