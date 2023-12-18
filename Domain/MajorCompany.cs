using System;
using System.Collections.Generic;

namespace Domain
{
    public class MajorCompany
    {
        public MajorCompany()
        {
            Companies = new HashSet<Company>();
            Majors = new HashSet<Major>();
        }
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int MajorId { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Major> Majors { get; set; }
    }
}
