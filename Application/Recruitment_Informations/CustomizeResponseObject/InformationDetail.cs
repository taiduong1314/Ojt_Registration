using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Recruitment_Informations.CustomizeResponseObject
{
    public class InformationDetail
    {
        public int id { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string MajorName { get; set; }
        public string CompanyWebsite { get; set; }
        public string Content { get; set; }
        public string Salary { get; set; }
        public string Position { get; set; }
        public DateTime? Deadline { get; set; }

    }
}
