using System;
using System.Collections.Generic;

#nullable disable

namespace Domain
{
    public partial class FptStaff
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Image { get; set; }
        public string Code { get; set; }
    }
}
