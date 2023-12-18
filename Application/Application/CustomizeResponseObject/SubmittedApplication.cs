using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.CustomizeResponseObject
{
    public class SubmittedApplication
    {
        public string StudentCode { get; set; }
        public string StudentName { get; set; }
        public string CompanyName { get; set; }
        public string Status { get; set; }
        public string Topic { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
