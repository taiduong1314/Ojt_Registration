using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OjtReport.RequestObj
{
    public class Evaluate
    {
        [Required]
        public string WorkSortDescription { get; set; }
        [Required]
        public string StudentCode { get; set; }
        [Required]
        public string CompanyCode { get; set; }
        [Required]
        public double? Mark { get; set; }
        [Required]
        public int? OnWorkDate { get; set; }
        [Required]
        public string Division { get; set; }
        [Required]
        public string LineManagerName { get; set; }
    }
}
