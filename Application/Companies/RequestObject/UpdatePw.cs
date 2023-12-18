using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Companies.RequestObject
{
    public class UpdatePw
    {
        [Required]
        public string Password { get; set; }
    }
}
