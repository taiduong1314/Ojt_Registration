using System;
using System.Collections.Generic;

namespace Domain
{
    public partial class Company
    {
        public Company()
        {
            OjtReports = new HashSet<OjtReports>();
            RecruitmentInformations = new HashSet<RecruitmentInformation>();
            Students = new HashSet<Student>();
            CompanyAccounts = new HashSet<CompanyAccount>();
        }

        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string WebSite { get; set; }
        public DateTime LastInteractDate { get; set; }
        public bool IsSubCompany { get; set; }

        public virtual ICollection<CompanyAccount> CompanyAccounts { get; set; }
        public virtual ICollection<OjtReports> OjtReports { get; set; }
        public virtual ICollection<RecruitmentInformation> RecruitmentInformations { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        public virtual MajorCompany MajorCompany { get; set; }
    }
}
