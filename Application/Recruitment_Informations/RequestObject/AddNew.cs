using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Recruitment_Informations.RequestObject
{
    public class AddNew
    {
        public string Content { get; set; }
        public DateTime? Deadline { get; set; }
        public string Salary { get; set; }
        public string MajorName { get; set; }
        public string Topic { get; set; }
        public string Area { get; set; }
    }
}
