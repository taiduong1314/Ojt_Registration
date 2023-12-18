using System;
using System.Collections.Generic;

#nullable disable

namespace Domain
{
    public partial class Major
    {
        public Major()
        {
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string MajorName { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual MajorCompany MajorCompany { get; set; }
    }
}
