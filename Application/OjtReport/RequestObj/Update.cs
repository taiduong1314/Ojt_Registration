using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.OjtReport.RequestObj
{
    public class Update
    {
        public string WorkSortDescription { get; set; }
        public double? Mark { get; set; }
        public int? OnWorkDate { get; set; }
        public string Division { get; set; }
        public string LineManagerName { get; set; }
    }
}
