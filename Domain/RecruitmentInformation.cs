using System;
using System.Collections.Generic;

#nullable disable

namespace Domain
{
    public partial class RecruitmentInformation
    {
        public RecruitmentInformation()
        {
            RecruimentApplies = new HashSet<RecruimentApply>();
        }

        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime? Deadline { get; set; }
        public string Salary { get; set; }
        public int? CompanyId { get; set; }
        public int MajorId { get; set; }
        public string Topic { get; set; }
        public string Position { get; set; }
        public string Area { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Company Company { get; set; }
        public virtual ICollection<RecruimentApply> RecruimentApplies { get; set; }
    }
}
