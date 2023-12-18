using System;
using System.Collections.Generic;

#nullable disable

namespace Domain
{
    public partial class CompanyAccount
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string Code { get; set; }
        public string Password{get; set; }

        public virtual Company Company { get; set; }
    }
}