using System;
using System.Collections.Generic;

#nullable disable

namespace Domain
{
    public partial class RecruimentApply
    {
        public int Id { get; set; }
        public string Cv { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public string Status { get; set; }
        public int? RecruimentInformationId { get; set; }
        public int? StudentId { get; set; }
        public string CoverLetter { get; set; }
        public DateTime UpdateDate { get; set; }
        public virtual RecruitmentInformation RecruimentInformation { get; set; }
        public virtual Student Student { get; set; }
    }
}
