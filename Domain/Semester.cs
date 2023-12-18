using System;
using System.Collections.Generic;

#nullable disable

namespace Domain
{
    public partial class Semester
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        
    }
}