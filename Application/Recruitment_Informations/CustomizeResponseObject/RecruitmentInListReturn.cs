using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Recruitment_Informations.CustomizeResponseObject
{
    public class RecruitmentInListReturn
    {
        public int Id { get; set; }
        public string Area { get; set; }
        public DateTime? Deadline { get; set; }
        public string Salary { get; set; }
        public string CompanyName { get; set; }
        public string MajorName { get; set; }
        public string Topic { get; set; }
    }
}
